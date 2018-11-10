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
    public class StationsController : Controller
    {
        /// <summary>
        /// Get all blood stations by user vk id (and his current city id)
        /// </summary>
        /// <param name="vkId">vkontakte vk id</param>
        /// <returns></returns>
        
        //GET /api/stations/{vk id}
        [HttpGet("getStationsByVkId")]
        public async Task<ActionResult> getStationsByVkId(int vkId)
        {
              return Content(await BloodStation.GetBloodStationsByVkIdTask(vkId));
        }

        private ActionResult<string> Get(int vkId)
        {
            return getStationsByVkId(vkId).Result;
        }
    }
}