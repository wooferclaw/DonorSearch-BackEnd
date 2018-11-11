﻿using System.Collections.Generic;
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
        /// Get donation by vkId
        /// </summary>
        /// <param name="vkId">vk Id</param>
        /// <returns>List of donations in JSON</returns>
        ///<remarks>
        /// Example of donation JSON: {"id":1,"blood_class_ids":4,"ds_Id":null,"succeed":null,"recomendation_timestamp":"0001-01-01T00:00:00","vk_id":1,"donation_timestamp":"2018-11-10T17:25:05.1751202+03:00","station_id":1,"status_id":1}
        /// Blood_class_ids possible values:
        ///None = 0,
        ///WholeBlood = 1,
        ///Plasma = 2,
        ///Eritocites = 4,
        ///Granulocites = 8,
        ///Liekocites = 16,
        ///Trombocites = 32
        ///status_id possible values:
        ///Appointment = 1,//запись + противопоказания
        ///Recomendations = 2,//рекомендации
        ///Donation = 3,//результат
        ///CheckAfterDonation = 4//проверка крови
        ///</remarks>
        //GET /api/donations/{vkId}
    [EnableCors("AllowAll")]
        [HttpGet("{vkId}")]
        public string Get(int vkId)
        {
            string result;

           List<DAL.Donation> donations = DonationRepository.GetDonationByVkId(vkId);
            if(donations == null)
            {
                result = ResultHelper.Error(ExceptionEnum.DBException);
            }
            else
            {
                result = JsonConvert.SerializeObject(donations);
            }
            return result;
        }

        //Create or update donation
        // POST /api/donations/{vk id}
        /// <summary>
        /// Creating donation for user, if "id" is not 0 - update
        /// </summary>
        /// <param name="donationJson">donation in JSON</param>
        /// <returns></returns>
        /// <remarks>
        /// Example of donation JSON for creation: {"blood_class_ids":4,"ds_Id":null,"succeed":null,"recomendation_timestamp":"0001-01-01T00:00:00","vk_id":1,"donation_timestamp":"2018-11-10T17:25:05.1751202+03:00","station_id":1,"status_id":1}
        ///  Example of donation JSON for updation: {"blood_class_ids":4,"ds_Id":null,"succeed":null,"recomendation_timestamp":"0001-01-01T00:00:00","vk_id":1,"donation_timestamp":"2018-11-10T17:25:05.1751202+03:00","station_id":1,"status_id":1}
        /// Blood_class_ids possible values:
        ///None = 0,
        ///WholeBlood = 1,
        ///Plasma = 2,
        ///Eritocites = 4,
        ///Granulocites = 8,
        ///Liekocites = 16,
        ///Trombocites = 32
        ///status_id possible values:
        ///Appointment = 1,//запись + противопоказания
        ///Recomendations = 2,//рекомендации
        ///Donation = 3,//результат
        ///CheckAfterDonation = 4//проверка крови
        ///</remarks>
        [EnableCors("AllowAll")]
        [HttpPost]
        public string Post([FromBody] JObject donationJson)
        {
            string result;
            DAL.Donation donation=donationJson.ToObject<DAL.Donation>();
            //считаем, что формат неправильный
            if (donation.vk_id == 0 || donation.station_id == 0 /*|| donation.status_id == 0*/ )
            {
                return ResultHelper.Error(ExceptionEnum.NotRightJSONFormat);
            }
            #region  Проверка заполненности обязательных полей
            if (donation.vk_id == 0)
            {
                return ResultHelper.Error(ExceptionEnum.EmptyNonRequiredParameter, "vk_id");
            }
            if (donation.station_id == 0)
            {
                return ResultHelper.Error(ExceptionEnum.EmptyNonRequiredParameter, "station_id");
            }
            //if (donation.status_id == 0)
            //{
            //    return ResultHelper.Error(ExceptionEnum.EmptyNonRequiredParameter, "status_id");
            //}
            #endregion
            if (donation.id == 0)
            {
                DonationRepository.AddDonation(donation);
                
            }
            else
            {
                DonationRepository.UpdateDonation(donation);
            }
            result = JsonConvert.SerializeObject(donation.id);
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
            DonationRepository.DeleteDonation(donationId);

            return result;
        }


    }
}