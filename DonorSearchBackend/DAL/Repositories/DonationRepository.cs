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

        //TODO: error? - return result
        public static void AddDonation(Donation donation)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Donations.Add(donation);
                db.SaveChanges();
            }
        }
        public static void UpdateDonation(Donation donation)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var originalDonation= db.Donations.FirstOrDefault(d => d.id == donation.id && d.vk_id == donation.vk_id);
                db.Entry(originalDonation).CurrentValues.SetValues(donation);
                db.SaveChanges();
            }
        }
        //TODO: error? - return result
        public static void DeleteDonation(int donationId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Donation donation = db.Donations.Where(d => d.id == donationId).FirstOrDefault();
                if (donation != null)
                {
                    db.Donations.Remove(donation);
                    db.SaveChanges();
                }
                else
                {
                    //TODO donation doesn't exist
                }
            }
        }
    }
}
