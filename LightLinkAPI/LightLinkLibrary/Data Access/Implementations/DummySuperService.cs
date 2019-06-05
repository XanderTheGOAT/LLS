using LightLinkModels;
using LightLinkModels.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightLinkLibrary.Data_Access.Implementations
{
    public class DummySuperService : IComputerService, IProfileService, IUserService, ILoginAuthenticator
    {
        private ICollection<Computer> computers;
        private ICollection<Profile> profiles;
        private ICollection<User> users;

        public DummySuperService()
        {
            computers = new HashSet<Computer>(new ComputerEqualityComparer());
            profiles = new List<Profile>();
            users = new HashSet<User>();
            
        }

        public void AddComputer(Computer dto)
        {
            computers.Add(dto);
        }

        public void AddProfileToUser(string username, Profile dto)
        {
            var user = users.FirstOrDefault(u => u.UserName.Equals(username));
            if (user != null)
            {
                user.Profiles.Add(dto);
                profiles.Add(dto);
            }
        }

        public void AddUser(User dto)
        {
            users.Add(dto);
        }

        public void DeleteUser(string username)
        {
            
            users.Remove(new User { UserName = username });
        }

        public IEnumerable<Computer> GetAllComputers()
        {
            return computers;
        }

        public IEnumerable<Computer> GetAllUnAssignedComputers()
        {
            return computers.Where(c => c.UserName == "");
        }

        public IEnumerable<User> GetAllUsers()
        {
            return users;
        }

        public Computer GetComputerForUser(string username, string computerName)
        {
            return GetComputersForUser(username).FirstOrDefault(c => c.Name == computerName);
        }

        public IEnumerable<Computer> GetComputersForUser(string username)
        {
            return computers.Where(c => c.UserName == username);
        }

        public IEnumerable<Profile> GetProfilesForUser(string username)
        {
            return GetUserById(username)?.Profiles;
        }

        public User GetUserById(string id)
        {
            return users.FirstOrDefault(u => u.UserName == id);
        }

        public void RemoveComputer(string name)
        {
            computers.Remove(new Computer { Name = name });
        }

        public void RemoveProfileFromUser(string username, string name)
        {
            GetUserById(username).Profiles?.Remove(new Profile { Name = name });
        }

        public void UpdateComputer(string computerName, Computer dto)
        {
            var remove = GetAllComputers().FirstOrDefault(c => c.Name == computerName);
            computers.Remove(remove);
            computers.Add(dto);
        }

        public void UpdateProfileOnUser(string username, string name, Profile dto)
        {
            var remove = GetProfilesForUser(username).FirstOrDefault(p => p.Name == name);
            var user = GetUserById(username);
            user.Profiles.Remove(remove);
            user.Profiles.Add(dto);
        }

        public void UpdateUser(string id, User dto)
        {
            var remove = users.FirstOrDefault(u => u.UserName == id);
            users.Remove(remove);
            users.Add(dto);
        }

        public bool Exists(string username)
        {
            return users.Any(u => u.UserName == username);
        }

        public bool Exists(string username, string name)
        {
            return GetUserById(username).Profiles.Any(p => p.Name == name);
        }

        public bool Exist(string computername)
        {
            return computers.Any(c => c.Name == computername);
        }

        public Profile GetActiveForUser(string username)
        {
            return users.FirstOrDefault(c => c.UserName == username)
                .Profiles
                .SingleOrDefault(p => p.IsActive)  ?? 
                users.FirstOrDefault(c => c.UserName == username)
                .Profiles
                .FirstOrDefault();
        }

        public User Authenticate(UserLogin logInfo)
        {
            return users.FirstOrDefault(u => u.UserName == logInfo.Username && u.Password == logInfo.Password);
        }

        public void SetActiveForUser(string username, Profile dto)
        {
            throw new NotImplementedException();
        }
    }
}
