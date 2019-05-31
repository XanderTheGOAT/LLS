using LightLinkLibrary.Data_Access;
using LightLinkModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightLinkAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        public IUserService UserService { get; private set; }
        public ILoginAuthenticator AuthenticatorService { get; private set; }

        public UserController(IUserService userService)
        {
            UserService = userService;
        }
        // GET api/User
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(UserService.GetAllUsers());
        }

        // GET api/values/5
        [HttpGet("{username}")]
        public ActionResult Get(string username)
        {
            var user = UserService.GetUserById(username);
            if (user is null) return NotFound();
            return Ok(user);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] User value)
        {
            if (value is null)
            {
                return BadRequest();
            }
            UserService.AddUser(value);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{username}")]
        public ActionResult Put(string username, [FromBody] User value)
        {
            if (!UserService.Exists(username)) return BadRequest(new { error="no user exists by that name." });
            UserService.UpdateUser(username, value);
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{username}")]
        public ActionResult Delete(string username)
        {
            if (!UserService.Exists(username)) return BadRequest(new { error = "no user exists by that name." });
            UserService.DeleteUser(username);
            return Ok();
        }
        
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserLogin logInfo)
        {
            if (logInfo is null) return BadRequest(new { error = "No login given" });
            bool isValid = AuthenticatorService.Authenticate(logInfo);
            return Ok(isValid);
        }
    }
}
