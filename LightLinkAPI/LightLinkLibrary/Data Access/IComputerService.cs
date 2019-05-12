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
        void RemoveComputer(Computer dto);
        void UpdateComputer(Computer dto, string computerName);
    }
}
