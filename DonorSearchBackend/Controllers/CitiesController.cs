﻿
using System.Threading.Tasks;
using DonorSearchBackend.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public async Task<ActionResult> Get(double lat, double lon)
        {
            return Content(await City.GetCityByCoordinatesTask(lat, lon));
        }

        //GET /api/Cities?pattern=<pattern>
        [HttpGet]
        public async Task<ActionResult> Get(string pattern)
        {
             return Content(await City.GetCityByTitleTask(pattern));
        }
    }
}