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
    public class UserController: ControllerBase
    {
        public IUserService UserService { get; private set; }

        public UserController(IUserService userService)
        {
            UserService = userService;
        }
        // GET api/User
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return UserService.GetAllUsers();
        }

        // GET api/values/5
        [HttpGet("{username}")]
        public User Get(string username)
        {
            return UserService.GetUserById(username);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] User value)
        {
            UserService.AddUser(value);
        }

        // PUT api/values/5
        [HttpPut("{username}")]
        public void Put(string username, [FromBody] User value)
        {
            UserService.UpdateUser(username, value);
        }

        // DELETE api/values/5
        [HttpDelete("{username}")]
        public void Delete(string username)
        {
            UserService.DeleteUser(username);
        }
    }
}
