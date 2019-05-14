using LightLinkLibrary.Data_Access;
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
        public IProfileService ProfileService { get; private set; }
        public IComputerService ComputerService { get; private set; }

        public UserController(IUserService userService, IProfileService profileService, IComputerService computerService)
        {
            UserService = userService;
            ProfileService = profileService;
            ComputerService = computerService;
        }
    }
}
