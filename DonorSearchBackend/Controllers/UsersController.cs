using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DonorSearchBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //GET /api/users/{vk id}? login = &lt; login&gt;&amp;password=&lt;password&gt;
        [HttpGet("getUserByVkId")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST /api/users/{vk id}
        [HttpPost("getUsersByVkId")]
        public void Post([FromBody] string value, int value2)
        {

        }

    }
}
