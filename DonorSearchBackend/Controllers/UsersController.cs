using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DonorSearchBackend.DAL;
using DonorSearchBackend.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DonorSearchBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Get user by vk id
        /// </summary>
        /// <param name="vkId">vk Id</param>
        /// <returns>info about user in JSON</returns>
        //GET /api/users/{vk_id}
        [EnableCors("AllowAll")]
        [HttpGet("{vkId}")]
        public string Get(int vkId)
        {
            //From Api
            //return Content(await UserApi.GetUserByVKId(vkId));
            DAL.User user = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                user = db.Users.Where(u=>u.vk_id == vkId).First();
            }
            //TODO: if null?
            return JsonConvert.SerializeObject(user);
        }

        /// <summary>
        /// Change user
        /// </summary>
        /// <param name="userJson"></param>
        /// <param name="vkId"></param>
        /// <returns></returns>
        // POST /api/users
        [EnableCors("AllowAll")]
        [HttpPost]
        public void Post([FromBody] JObject userJson)
        {
            //TODO error -not right format
            DAL.User user = userJson.ToObject<DAL.User>();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.AddOrUpdate(user);
                db.SaveChanges();
            }

            //change in APi
            //mutation updateProfile($first_name: String, $second_name: String, $last_name: String, $maiden_name: String, $bdate: Int, $gender: Int, $city_id: Int, $about_self:String, $blood_type_id:Int, $kell: Int, $blood_class_ids:[Int],$bone_marrow: Boolean, $cant_to_be_donor: Boolean, $donor_pause_to: Int ) 
            //{
            //    updateProfile(first_name: $first_name, second_name: $second_name, last_name:$last_name, maiden_name:$maiden_name, bdate:$bdate, gender:$gender, city_id:$city_id, about_self:$about_self, blood_type_id:$blood_type_id, kell:$kell, blood_class_ids:$blood_class_ids, bone_marrow:$bone_marrow, cant_to_be_donor:$cant_to_be_donor, donor_pause_to:$donor_pause_to)
            //{ id first_name last_name second_name}
            //}
        }
        

    }
}
