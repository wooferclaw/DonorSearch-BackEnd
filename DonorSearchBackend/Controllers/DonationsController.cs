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
        //GET /api/donations/{vk id}
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST /api/donations/{vk id}?{donation id}?{type}
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // POST /api/donations/{vk id}
        [HttpPost]
        public void Post([FromBody] int value)
        {

        }
    }
}