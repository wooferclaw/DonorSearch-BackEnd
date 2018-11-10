using System.Threading.Tasks;
using DonorSearchBackend.Helpers.DSApi;
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
        [HttpGet("{vkId}")]
        public async Task<ActionResult> Get(int vkId)
        {
              return Content(await DSBloodStation.GetBloodStationsByVkIdTask(vkId));
        }
    }
}