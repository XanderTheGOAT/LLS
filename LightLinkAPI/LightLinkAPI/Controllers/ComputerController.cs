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
        public ComputerController(IComputerService computerService)
        {
            ComputerService = computerService;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Computer> Get()
        {
            return ComputerService.GetAllComputers();
        }

        [HttpGet("unassigned")]
        public IEnumerable<Computer> GetUnassigned()
        {
            return ComputerService.GetAllUnAssignedComputers();
        }

        // GET api/<controller>/5
        [HttpGet("{username}")]
        public IEnumerable<Computer> Get(string username)
        {
            return ComputerService.GetComputersForUser(username);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Computer value)
        {
            ComputerService.AddComputer(value);
        }

        // PUT api/<controller>/5
        [HttpPut("{computername}")]
        public void Put(string computername, [FromBody]Computer value)
        {
            ComputerService.UpdateComputer(computername, value);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{computername}")]
        public void Delete(string computername)
        {
            ComputerService.RemoveComputer(computername);
        }
    }
}
