using Microsoft.EntityFrameworkCore;
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
                donationsList = db.Donations.Include(d=>d.confirm_visit).Where(u => u.vk_id == vkId).ToList();
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

        public static async void UpdateDonation(Donation donation)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (donation.confirm_visit == null)
                {
                    donation.confirm_visit = new ValidationVisit();
                }
                var originalDonation= db.Donations.Include(d=>d.confirm_visit).FirstOrDefault(d => d.id == donation.id && d.vk_id == donation.vk_id);
                //если заполнена дата донации+она изменилась, то определим период для повторного визита 
                //и дату когда будут посылаться уведомления с рекомендацией
                if ((donation.donation_date.HasValue && originalDonation.donation_date == null) ||
                    (donation.donation_date.HasValue && originalDonation.donation_date.HasValue && (donation.donation_date.Value.ToString() != originalDonation.donation_date.Value.ToString())))
                    { 
                        CalculateDates(donation);
                    }
                
                //когда ставится, что донор сдал кровь - обновляем противопоказания
                if (donation.donation_success.HasValue)
                {
                   User user = UserRepository.GetUserByVkId(donation.vk_id);
                    //TODO: 60 дней только для цельной крови
                    user.donor_pause_to = donation.donation_date.Value.AddDays(60);
                    await UserRepository.AddOrUpdateUser(user,false);
                }
                //Когда пользователь посетит центр и сделает донацию или сдаст кровь из пальца (повторно)
                if (donation.confirm_visit != null && donation.confirm_visit.success != null && donation.confirm_visit.without_donation != null)
                {
                    //finished переходит в true 
                    donation.finished = true;
                }
                db.Entry(originalDonation).CurrentValues.SetValues(donation);
                db.Entry(originalDonation.confirm_visit).CurrentValues.SetValues(donation.confirm_visit);
                db.SaveChanges();
            }
        }
        //TODO: error? - return result
        public static Donation DeleteAndAddNewDonation(int donationId)
        {
            DAL.Donation newAppointment = new Donation();
            using (ApplicationContext db = new ApplicationContext())
            {
                Donation donation = db.Donations.Include(d => d.confirm_visit).Where(d => d.id == donationId).FirstOrDefault();
                if (donation != null)
                {
                    //вернуть новую донацию
                    DateTime appointmentFrom = UserRepository.GetUserByVkId(donation.vk_id).donor_pause_to.HasValue ? UserRepository.GetUserByVkId(donation.vk_id).donor_pause_to.Value : DateTime.Now;
                    //предыдущую дату выдачи проставляем только если была успешная сдача в этой
                    DateTime? previous_success_date = null;
                    if (donation.donation_date.HasValue && donation.donation_success == true)
                    {
                        previous_success_date = donation.donation_date.Value;
                    }
                    newAppointment = new DAL.Donation() { vk_id = donation.vk_id, appointment_date_from = appointmentFrom, appointment_date_to = appointmentFrom.AddDays(7), previous_donation_date = previous_success_date };
                    
                    db.Donations.Remove(donation);
                    db.SaveChanges();
                    AddDonation(newAppointment);
                }
                else
                {
                    //TODO donation doesn't exist
                }

            }
            return newAppointment;
        }
    }
}
