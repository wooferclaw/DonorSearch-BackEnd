﻿using System;
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
                user = db.Users.Where(u => u.vk_id == vkId).FirstOrDefault();
            }
            return user;
        }

        //TODO: error?
        public static void AddOrUpdateUser(User user)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.AddOrUpdate(user);
                db.SaveChanges();
            }
        }
    }
}
