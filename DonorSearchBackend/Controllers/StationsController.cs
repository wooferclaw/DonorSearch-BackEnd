using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DonorSearchBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationsController : Controller
    {
        //GET /api/stations/{vk id}
        //[HttpGet("getStationsByVkId")]
        //public async Task<ActionResult> getDonorSearchCiByCoordinates(double lat, double lon)
        //{
        //    //Content(await ...City.GetCityByCoordinatesTask(lat, lon));
        //   // return 
        //}

        //private ActionResult<string> Get(double lat, double lon)
        //{
        //    return getDonorSearchCityBoordinates(lat, lon).Result;
        //}
    }
}