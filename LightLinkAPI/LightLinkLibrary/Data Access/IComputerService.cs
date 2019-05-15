using LightLinkModels;
using System.Collections.Generic;

namespace LightLinkLibrary.Data_Access
{
    public interface IComputerService
    {
        Computer GetComputerForUser(string username, string computerName);
        IEnumerable<Computer> GetComputersForUser(string username);
        IEnumerable<Computer> GetAllUnAssignedComputers();
        IEnumerable<Computer> GetAllComputers();
        void AddComputer(Computer dto);
        void RemoveComputer(string computerName);
        void UpdateComputer(string computerName, Computer dto);
    }
}
