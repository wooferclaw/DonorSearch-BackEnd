using DonorSearchBackend.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorSearchBackend.DAL.Repositories
{
    public static class UserRepository
    {
        public static User GetUserByVkId(int vkId)
        {
            User user = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                user = db.Users.FirstOrDefault(u => u.vk_id == vkId);
            }
            return user;
        }

        //TODO: error?
        public static async Task<User> AddOrUpdateUser(User user, bool isInit)
        {
            
            using (ApplicationContext db = new ApplicationContext())
            {
                //если заполнено только название города, то надо вытащить id из DS api
                if (user.city_id == null && !string.IsNullOrEmpty(user.city_title))
                {
                    DSCity currentCity = new DSCity();
                    var cities= await DSCity.GetCitiesByPattern(user.city_title);
                    //если несколько городов - берём первый
                    if (cities.Count > 0)
                    {
                        currentCity = cities[0];
                    }
                    //если не найдены города - Москва
                    else
                    {
                        var Moscow = await DSCity.GetCitiesByPattern("Москва");
                        currentCity = Moscow[0];
                    }
                    int id;
                    if (int.TryParse(currentCity.id, out id))
                    {
                        user.city_id = id;
                        user.city_title = currentCity.title;
                        user.region_title = currentCity.region!=null?currentCity.region.title : null;
                    }
                    else
                    {
                        //TODO:error
                    }
                }

                //if user founded in DB - update
                if (db.Users.Any(u => u.vk_id == user.vk_id))
                {
                    var originalUser = db.Users.FirstOrDefault(u => u.vk_id == user.vk_id);
                    if (!isInit)
                    {
                        
                        db.Entry(originalUser).CurrentValues.SetValues(user);
                    }
                    else
                    {
                        user = originalUser;
                    }
                }
                else
                {
                    db.Users.Add(user);
                }
                db.SaveChanges();

                return user;
            }
        }
    }
}
