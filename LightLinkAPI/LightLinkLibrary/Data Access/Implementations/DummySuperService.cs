using LightLinkModels;
using LightLinkModels.Comparers;
using System.Collections.Generic;
using System.Linq;

namespace LightLinkLibrary.Data_Access.Implementations
{
    public class DummySuperService : IComputerService, IProfileService, IUserService
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

        public User DeleteUser(User dto)
        {
            users.Remove(dto);
            return dto;
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

        public Profile GetProfileByName(string username, string name)
        {
            return GetProfilesForUser(username)?.FirstOrDefault(p => p.Name == name);
        }

        public IEnumerable<Profile> GetProfilesForUser(string username)
        {
            return GetUserById(username)?.Profiles;
        }

        public User GetUserById(string id)
        {
            return users.FirstOrDefault(u => u.UserName == id);
        }

        public void RemoveComputer(Computer dto)
        {
            computers.Remove(dto);
        }

        public Profile RemoveProfileFromUser(string username, Profile dto)
        {
            GetUserById(username)?.Profiles?.Remove(dto);
            return dto;
        }

        public void UpdateComputer(Computer dto, string computerName)
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
    }
}
