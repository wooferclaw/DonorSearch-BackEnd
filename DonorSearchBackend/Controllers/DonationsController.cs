using System;
using System.Collections.Generic;
using System.Linq;
using DonorSearchBackend.DAL.Repositories;
using DonorSearchBackend.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DonorSearchBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationsController : Controller
    {
        /// <summary>
        /// Get successful donations and donation for timeline by vkId
        /// </summary>
        /// <param name="vkId">vk Id</param>
        /// <param name="type">successful or timeline</param>
        /// <returns>List of donations in JSON</returns>
        ///<remarks>
        /// Example of donation JSON:{
        ///  "id": 0,
        ///  "ds_Id": null,
        ///  "vk_id": 0,
        ///  "appointment_date_from": "0001-01-01T00:00:00",
        ///  "appointment_date_to": "0001-01-01T00:00:00",
        ///  "donation_date": null,
        ///  "donation_success": null,
        ///  "blood_class_ids": 0,
        ///  "img": null,
        ///  "station_id": null,
        ///  "recomendation_timestamp": null,
        ///  "finished": false,
        ///  "confirm_visit": {
        ///  "id": 0,
        ///  "date_from": null,
        ///  "date_to": null,
        ///  "visit_date": null,
        ///  "success": null
        ///}
        ///}
        /// Blood_class_ids possible values:
        ///None = 0,
        ///WholeBlood = 1,
        ///Plasma = 2,
        ///Eritocites = 4,
        ///Granulocites = 8,
        ///Liekocites = 16,
        ///Trombocites = 32
        ///</remarks>
        //GET /api/donations/{vkId}?type=<successful/timeline>
    [EnableCors("AllowAll")]
        [HttpGet("{vkId}")]
        public string Get(int vkId, string type)
        {
            string result = ResultHelper.Error(ExceptionEnum.WrongRequest);

           List<DAL.Donation> donations = DonationRepository.GetDonationByVkId(vkId);
            if(donations == null)
            {
                return ResultHelper.Error(ExceptionEnum.DBException);
            }
            switch (type)
            {
                //получить все успешные донации
                case "successful":
                    List<DAL.Donation> successDonations = donations.Where(d=>d.donation_success==true).ToList();
                    result = JsonConvert.SerializeObject(successDonations);
                    break;
                //текущий timeline, в котором пользователь находится
                case "timeline":
                    //если есть таймлайн в процессе (не finished) - выдаём его
                    DAL.Donation timelineDonation = donations.Where(d=>d.finished == false).ToList().LastOrDefault();
                    //если в процессе таймлайна нет - создаём его
                    if (timelineDonation == null)
                    {
                        //вычислить дату после которой донор может записаться на донацию
                        DateTime appointmentFrom = DateTime.Now;
                        DAL.User user = UserRepository.GetUserByVkId(vkId);
                        if (user!= null && user.donor_pause_to.HasValue && user.donor_pause_to.Value>DateTime.Now)
                        {
                            appointmentFrom = UserRepository.GetUserByVkId(vkId).donor_pause_to.Value;
                        }
                        DAL.Donation newAppointment = new DAL.Donation() { vk_id = vkId, appointment_date_from = appointmentFrom, appointment_date_to = appointmentFrom.AddDays(7)};
                        timelineDonation = newAppointment;
                        DonationRepository.AddDonation(timelineDonation);
                    }

                    result = JsonConvert.SerializeObject(timelineDonation);
                    
                    break;
            }
            
            return result;
        }

        //Create or update donation
        // POST /api/donations/{vk id}
        /// <summary>
        /// Creating donation for user, if "id" is not 0 - update
        /// </summary>
        /// <param name="donationJson">donation in JSON</param>
        /// <returns>new donation in JSON fornmat</returns>
        /// <remarks>
        /// Blood_class_ids possible values:
        ///None = 0,
        ///WholeBlood = 1,
        ///Plasma = 2,
        ///Eritocites = 4,
        ///Granulocites = 8,
        ///Liekocites = 16,
        ///Trombocites = 32
        ///</remarks>
        [EnableCors("AllowAll")]
        [HttpPost]
        public string Post([FromBody] JObject donationJson)
        {
            string result;
            DAL.Donation donation=donationJson.ToObject<DAL.Donation>();
            #region  Проверка заполненности обязательных полей
            if (donation.vk_id == 0)
            {
                return ResultHelper.Error(ExceptionEnum.EmptyNonRequiredParameter, "vk_id");
            }
            #endregion
                DonationRepository.UpdateDonation(donation);
            //Когда пользователь посетит центр и сделает донацию или сдаст кровь из пальца (повторно)
            if (donation.confirm_visit != null && donation.confirm_visit.success != null && donation.confirm_visit.without_donation != null)
            {
                //вернуть новую запись с заполненными полями(appointment_date_from, appointment_date_to, previous_donation_date)
                //вычислить дату после которой донор может записаться на донацию
                DateTime appointmentFrom = UserRepository.GetUserByVkId(donation.vk_id).donor_pause_to.HasValue ? UserRepository.GetUserByVkId(donation.vk_id).donor_pause_to.Value : DateTime.Now;
                //предыдущую дату выдачи проставляем только если была успешная сдача в этой
                DateTime? previous_success_date = null;
                if (donation.donation_date.HasValue && donation.donation_success == true)
                {
                    previous_success_date = donation.donation_date.Value;
                }
                DAL.Donation newAppointment = new DAL.Donation() { vk_id = donation.vk_id, appointment_date_from = appointmentFrom, appointment_date_to = appointmentFrom.AddDays(7), previous_donation_date = previous_success_date };
                //Если success = true + without_donation = false из старого объекта скопируется центр
                if (donation.confirm_visit.success == true && donation.confirm_visit.without_donation == false)
                {
                    newAppointment.station_id = donation.station_id;
                    newAppointment.station_address = donation.station_address;
                    newAppointment.station_title = donation.station_title;
                }
                donation = newAppointment;
                DonationRepository.AddDonation(donation);
            }
            
            result = JsonConvert.SerializeObject(donation);
            return result;
        }

        /// <summary>
        /// Delete donation by vkId and donationId
        /// </summary>
        /// <param name="vkId"></param>
        /// <param name="donationId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        //delete donation
        // POST /api/donations/delete/{vkId}/{donationId}
        [EnableCors("AllowAll")]
        [HttpPost("delete/{vkId}/{donationId}")]
        public string Post(int vkId, int donationId, string type)
        {
            string result = ResultHelper.Success();
            if (vkId == 0)
            {
                return ResultHelper.Error(ExceptionEnum.EmptyNonRequiredParameter, "vkId");
            }
            if (donationId == 0)
            {
                return ResultHelper.Error(ExceptionEnum.EmptyNonRequiredParameter, "donationId");
            }


            DAL.Donation newDonation = DonationRepository.DeleteAndAddNewDonation(donationId);
            result = JsonConvert.SerializeObject(newDonation);

            return result;
        }


    }
}