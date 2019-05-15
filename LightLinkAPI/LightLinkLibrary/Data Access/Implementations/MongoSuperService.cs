using System;
using System.Collections.Generic;
using System.Text;
using LightLinkModels;
using MongoDB.Driver;

namespace LightLinkLibrary.Data_Access.Implementations
{
    public class MongoSuperService : IComputerService, IProfileService, IUserService
    {
        public void AddComputer(Computer dto)
        {
            throw new NotImplementedException();
        }

        public void AddProfileToUser(string username, Profile dto)
        {
            throw new NotImplementedException();
        }

        public void AddUser(User dto)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string id)
        {
            throw new NotImplementedException();
        }

        public bool Exist(string computername)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string username)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string username, string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Computer> GetAllComputers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Computer> GetAllUnAssignedComputers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Computer GetComputerForUser(string username, string computerName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Computer> GetComputersForUser(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Profile> GetProfilesForUser(string username)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public void RemoveComputer(string computerName)
        {
            throw new NotImplementedException();
        }

        public void RemoveProfileFromUser(string username, string name)
        {
            throw new NotImplementedException();
        }

        public void UpdateComputer(string computerName, Computer dto)
        {
            throw new NotImplementedException();
        }

        public void UpdateProfileOnUser(string username, string name, Profile dto)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(string id, User dto)
        {
            throw new NotImplementedException();
        }
    }
}
