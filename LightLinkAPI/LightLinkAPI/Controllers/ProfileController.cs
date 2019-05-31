using LightLinkLibrary.Data_Access;
using LightLinkModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LightLinkAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        public IUserService UserService { get; private set; }
        public IProfileService ProfileService { get; private set; }
        public IComputerService ComputerService { get; private set; }

        public ProfileController(IUserService userService, IProfileService profileService)
        {
            UserService = userService;
            ProfileService = profileService;
        }

        // GET api/Profile/gxldcptrick/
        [HttpGet("{username}")]
        public ActionResult Get(string username)
        {
            if (!UserService.Exists(username))
            {
                return NotFound();
            }

            return Ok(ProfileService.GetProfilesForUser(username));
        }
        // GET api/Profile/active/gxldcptrick
        [HttpGet("active/{username}")]
        public ActionResult GetActive(string username)
        {
            var result = ProfileService.GetActiveForUser(username);
            if (result is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        // POST api/values
        [HttpPost("{username}")]
        public ActionResult Post(string username, [FromBody] Profile value)
        {
            if (!UserService.Exists(username))
            {
                return BadRequest(new { error = "user by that username was not found." });
            }

            ProfileService.AddProfileToUser(username, value);
            return Ok();
        }

        // PUT api/values/username
        [HttpPut("{username}/{name}")]
        public ActionResult Put(string username, string name, [FromBody] Profile value)
        {
            if (!UserService.Exists(username))
            {
                return BadRequest(new { error = "user by that username was not found." });
            }

            if (!ProfileService.Exists(username, name))
            {
                return BadRequest(new { error = "profile not found on user." });
            }

            ProfileService.UpdateProfileOnUser(username, name, value);
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{username}/{name}")]
        public ActionResult Delete(string username, string name)
        {
            if (!UserService.Exists(username))
            {
                return BadRequest(new { error = "user by that username was not found." });
            }

            if (!ProfileService.Exists(username, name))
            {
                return BadRequest(new { error = "profile not found on user." });
            }

            ProfileService.RemoveProfileFromUser(username, name);
            return Ok();
        }
    }
}
