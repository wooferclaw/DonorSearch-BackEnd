using DonorSearchBackend.DAL;
using DonorSearchBackend.DAL.Repositories;
using DonorSearchBackend.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

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
            string result;
            DAL.User user = UserRepository.GetUserByVkId(vkId);
            if (user == null)
            {
                result = ResultHelper.Error(ExceptionEnum.DBException);
            }
            else
            {
                result = JsonConvert.SerializeObject(user);
            }
            return result;
        }

        /// <summary>
        /// Create or update user
        /// </summary>
        /// <param name="userJson">changed user JSON</param>
        /// <param name="type">init or change</param>
        /// <returns></returns>
        // POST /api/users?type=<init/change>
        [EnableCors("AllowAll")]
        [HttpPost]
        public async Task<string> Post([FromBody] JObject userJson, string type)
        {
            string result;
            //check right format for JSON - TODO validation failed
            //string userSchemaJson = JsonConvert.SerializeObject(new DAL.User());
            //JSchema schema = JSchema.Parse(userSchemaJson);
            //if (!userJson.IsValid(schema))
            //{
            //    return JsonConvert.SerializeObject(new Result(false, ExceptionEnum.NotRightJSONFormat.ToString()));
            //}
            DAL.User user = userJson.ToObject<DAL.User>();
            //считаем, что формат неправильный
            if (user.vk_id == 0 || string.IsNullOrEmpty(user.first_name) || string.IsNullOrEmpty(user.last_name))
            {
                return ResultHelper.Error(ExceptionEnum.NotRightJSONFormat); 
            }
            #region  Проверка заполненности обязательных полей
            if (user.vk_id == 0)
            {
                return ResultHelper.Error(ExceptionEnum.EmptyNonRequiredParameter, "vk_id");
            }
            if (string.IsNullOrEmpty(user.first_name))
            {
                return ResultHelper.Error(ExceptionEnum.EmptyNonRequiredParameter, "first_name");
            }
            if (string.IsNullOrEmpty(user.last_name))
            {
                return ResultHelper.Error(ExceptionEnum.EmptyNonRequiredParameter, "last_name");
            }
            if (string.IsNullOrEmpty(user.city_title))
            {
                return ResultHelper.Error(ExceptionEnum.EmptyNonRequiredParameter, "city_title");
            }
            #endregion
            User resultUser =await UserRepository.AddOrUpdateUser(user, type == "init");
            result = JsonConvert.SerializeObject(resultUser);

            //update in APi - not work
            //mutation updateProfile($first_name: String, $second_name: String, $last_name: String, $maiden_name: String, $bdate: Int, $gender: Int, $city_id: Int, $about_self:String, $blood_type_id:Int, $kell: Int, $blood_class_ids:[Int],$bone_marrow: Boolean, $cant_to_be_donor: Boolean, $donor_pause_to: Int ) 
            //{
            //    updateProfile(first_name: $first_name, second_name: $second_name, last_name:$last_name, maiden_name:$maiden_name, bdate:$bdate, gender:$gender, city_id:$city_id, about_self:$about_self, blood_type_id:$blood_type_id, kell:$kell, blood_class_ids:$blood_class_ids, bone_marrow:$bone_marrow, cant_to_be_donor:$cant_to_be_donor, donor_pause_to:$donor_pause_to)
            //{ id first_name last_name second_name}
            //}
            return result;
        }
        

    }
}
