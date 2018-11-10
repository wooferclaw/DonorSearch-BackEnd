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
        public static void AddOrUpdateUser(User user)
        {
            
            using (ApplicationContext db = new ApplicationContext())
            {
                //if user founded in DB - update
                if (db.Users.Any(u => u.vk_id == user.vk_id))
                {
                    var originalUser = db.Users.FirstOrDefault(u => u.vk_id == user.vk_id);
                    db.Entry(originalUser).CurrentValues.SetValues(user);
                }
                else
                {
                    db.Users.Add(user);
                }
                db.SaveChanges();
            }
        }
    }
}
