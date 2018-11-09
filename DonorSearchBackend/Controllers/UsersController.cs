using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DonorSearchBackend.Helpers;
using GraphQL.Client;
using GraphQL.Common.Request;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("{vkId}")]
        public async Task<ActionResult> Get(int vkId)
        {
            return Content(await UserApi.GetUserByVKId(vkId));
        }

        // POST /api/users/{vk id}
        [HttpPost("getUsersByVkId")]
        public void Post([FromBody] string value, int value2)
        {

        }

    }
}
