using LightLinkLibrary.Data_Access;
using LightLinkModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LightLinkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController: ControllerBase
    {
        public IUserService UserService { get; private set; }
        public IProfileService ProfileService { get; private set; }
        public IComputerService ComputerService { get; private set; }

        public ProfileController(IUserService userService, IProfileService profileService)
        {
            UserService = userService;
            ProfileService = profileService;
        }

        // GET api/Profile/username/
        [HttpGet("{username}")]
        public IEnumerable<Profile> Get(string username)
        {
            return ProfileService.GetProfilesForUser(username);
        }

        // POST api/values
        [HttpPost("{username}")]
        public void Post(string username, [FromBody] Profile value)
        {
            ProfileService.AddProfileToUser(username, value);
        }

        // PUT api/values/username
        [HttpPut("{username}/{name}")]
        public void Put(string username, string name, [FromBody] Profile value)
        {
            ProfileService.UpdateProfileOnUser(username, name, value);
        }

        // DELETE api/values/5
        [HttpDelete("{username}/{name}")]
        public void Delete(string username, string name)
        {
            ProfileService.RemoveProfileFromUser(username, name);
        }
    }
}
