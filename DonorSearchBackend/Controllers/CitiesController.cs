using System;
using System.Threading.Tasks;
using DonorSearchBackend.DAL.Repositories;
using DonorSearchBackend.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DonorSearchBackend.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]  
    [ApiController]
    public class CitiesController : Controller
    {
        //GET /api/Cities?lat=<lat>&long=<long>
        /// <summary>
        /// Get correct city name list from DonorSearch database
        /// </summary>
        /// <param name="lat">Latitude</param>
        /// <param name="lon">Longitude</param>
        /// <returns>City list from DonorSearch database by coordinates</returns>
        [EnableCors("AllowAll")]
        [HttpGet]
        public async Task<ActionResult> Get(double lat, double lon)
        {
            return Content(await DSCity.GetCityByCoordinatesTask(lat, lon));
        }

        /// <summary>
        /// Get cities by search pattern
        /// </summary>
        /// <param name="pattern">search pattern</param>
        /// <returns></returns>
        //GET /api/Cities/searchbyPattern/{pattern}
        [EnableCors("AllowAll")]
        [HttpGet("searchByPattern/{pattern}")]
        public async Task<ActionResult> Get(string pattern)
        {
             return Content(await DSCity.GetCityByTitleTask(pattern));
        }


        /// <summary>
        /// Get coordinates for user by vk id (if no city or geoposition were specified)
        /// </summary>
        /// <param name="vkId"></param>
        /// <returns></returns>
        [EnableCors("AllowAll")]
        [HttpGet("searchByVkId/{vkId}")]
        public async Task<string> GetAsync(int vkId)
        {
            //From Api
            //return Content(await UserApi.GetUserByVKId(vkId));
            string result;
            DAL.User user = UserRepository.GetUserByVkId(vkId);

            var coordinates = await OsmLocation.GetCoordinatesByCityTitleTask(user);

            if (user == null)
            {
                result = ResultHelper.Error(ExceptionEnum.DBException);
            }
            else
            {
                result = coordinates;
            }
            return result;
        }
    }
}