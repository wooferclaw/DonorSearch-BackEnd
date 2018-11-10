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
    public class DonationsController : Controller
    {
        //GET /api/donations/{vkId}
        [HttpGet("{vkId}")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST /api/donations/{vkId}/{donationId}?{type}
        [HttpPost("{vkId}/{donationId}")]
        public void Post([FromBody] string value)
        {

        }

        // POST /api/donations/{vk id}
        [HttpPost("{vkId}")]
        public void Post([FromBody] int value1, int value2)
        {

        }
    }
}