using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DonorSearchBackend.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DonorSearchBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : Controller
    {
        //GET /api/Cities/getDonorSearchCityByCoordinates
        /// <summary>
        /// Get correct city name list from DonorSearch database
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns>City list from DonorSearch database by coordinates</returns>

        [HttpGet("getDonorSearchCityByCoordinates")]
        public async Task<ActionResult> getDonorSearchCityByCoordinates(double lat, double lon)
        {
            return Content(await City.GetCityByCoordinatesTask(lat, lon));
        }

        private ActionResult<string> Get(double lat, double lon)
        {
            return getDonorSearchCityByCoordinates(lat, lon).Result;
        }


    }
}