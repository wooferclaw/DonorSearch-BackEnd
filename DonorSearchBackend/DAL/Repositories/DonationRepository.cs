using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorSearchBackend.DAL.Repositories
{
    public class DonationRepository
    {
        public static List<Donation> GetDonationByVkId(int vkId)
        {
            List<Donation> donationsList = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                donationsList = db.Donations.Where(u => u.vk_id == vkId).ToList();
            }
            return donationsList;
        }

        //TODO: error?
        public static void AddOrUpdateDonation(Donation donation)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Donations.AddOrUpdate(donation);
                db.SaveChanges();
            }
        }
    }
}
