using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightLinkLibrary.Data_Access;
using LightLinkModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LightLinkAPI.Controllers
{
    [Route("api/[controller]")]
    public class ComputerController : Controller
    {
        public IComputerService ComputerService { get; private set; }
        public IUserService UserService { get; private set; }

        public ComputerController(IComputerService computerService, IUserService userService)
        {
            ComputerService = computerService;
            UserService = userService;
        }
        // GET: api/<controller>
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(ComputerService.GetAllComputers());
        }
        //GET api/<controller>/unassigned
        [HttpGet("unassigned")]
        public ActionResult GetUnassigned()
        {
            return Ok(ComputerService.GetAllUnAssignedComputers());
        }

        // GET api/<controller>/gxldcptrick
        [HttpGet("{username}")]
        public ActionResult Get(string username)
        {
            if (!UserService.Exists(username)) return BadRequest("user with the given username does not exist.");
            return Ok(ComputerService.GetComputersForUser(username));
        }

        // POST api/<controller>
        [HttpPost]
        public ActionResult Post([FromBody]Computer value)
        {
            if (value is null) return BadRequest(new { error = "not able to create computer object." });
            ComputerService.AddComputer(value);
            return Ok();
        }

        // PUT api/<controller>/DESKTOPKVG-3232
        [HttpPut("{computername}")]
        public ActionResult Put(string computername, [FromBody]Computer value)
        {
            if (!ComputerService.Exist(computername)) return BadRequest(new { error = "computer with the given name does not exist." });
            ComputerService.UpdateComputer(computername, value);
            return Ok();
        }

        // DELETE api/<controller>/DESKTOPKVG-3232
        [HttpDelete("{computername}")]
        public ActionResult Delete(string computername)
        {
            if (!ComputerService.Exist(computername)) return BadRequest(new { error = "computer with given name does not exist." });
            ComputerService.RemoveComputer(computername);
            return Ok();
        }
    }
}
