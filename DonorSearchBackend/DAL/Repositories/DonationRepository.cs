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
                donation.confirm_visit = new ValidationVisit();
                //если заполнена дата донации, то определим период для повторного визита 
                //и дату когда будут посылаться уведомления с рекомендацией
                if (donation.donation_date.HasValue)
                {
                    CalculateDates(donation);
                }
                db.Donations.Add(donation);
                db.SaveChanges();
            }
        }

        private static void CalculateDates(Donation donation)
        {
            donation.confirm_visit.date_from = donation.donation_date.Value.AddMonths(3);
            donation.confirm_visit.date_to = donation.donation_date.Value.AddMonths(6);
            //напоминать за 3,2,1 день (в зависимости от того, сколько дней осталось)
            double days = (donation.donation_date.Value.Subtract(DateTime.Now)).TotalDays;
            if (days >= 3)
            {
                donation.recomendation_timestamp = donation.donation_date.Value.AddDays(-3);
            }
            else if (days < 3 && days >= 2)
            {
                donation.recomendation_timestamp = donation.donation_date.Value.AddDays(-2);
            }
            else if (days < 2)
            {
                donation.recomendation_timestamp = donation.donation_date.Value.AddDays(-1);
            }
        }

        public static void UpdateDonation(Donation donation)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var originalDonation= db.Donations.FirstOrDefault(d => d.id == donation.id && d.vk_id == donation.vk_id);
                //если заполнена дата донации+она изменилась, то определим период для повторного визита 
                //и дату когда будут посылаться уведомления с рекомендацией
                if(donation.donation_date.HasValue && donation.donation_date != originalDonation.donation_date)
                {
                    CalculateDates(donation);
                }
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
