using LightLinkModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightLinkLibrary.Data_Access.Implementations
{
    public class MongoSuperService : IComputerService, IProfileService, IUserService, ILoginAuthenticator
    {
        private readonly MongoClient client;
        private static readonly string mongoConnectionTemplate;
        static MongoSuperService()
        {
            mongoConnectionTemplate = "mongodb://{0}:27017";
        }
        public MongoSuperService(string host = "localhost")
        {
            client = new MongoClient(String.Format(mongoConnectionTemplate, host));
        }
        public void AddComputer(Computer dto)
        {
            var db = client.GetDatabase("LightLinkProfiles");

            var collection = db.GetCollection<Computer>("Computer");

            if (!GetAllComputers().Contains(dto))
            {
                collection.InsertOne(dto);
            }
        }

        public void AddProfileToUser(string username, Profile dto)
        {
            var db = client.GetDatabase("LightLinkProfiles");

            var collection = db.GetCollection<User>("User");

            var filter = new FilterDefinitionBuilder<User>();

            var builder = new UpdateDefinitionBuilder<User>();
            var user = collection.Find((u) => u.UserName == username).Single();
            user.Profiles.Add(dto);
            var filterBuilder = new FilterDefinitionBuilder<User>();
            collection.ReplaceOne(filterBuilder.Where((u) => u.UserName == username), user);
        }

        public void AddUser(User dto)
        {
            var db = client.GetDatabase("LightLinkProfiles");

            var collection = db.GetCollection<User>("User");

            if (!GetAllUsers().Contains(dto))
            {
                collection.InsertOne(dto);
            }
        }

        public void DeleteUser(string id)
        {
            var db = client.GetDatabase("LightLinkProfiles");

            var collection = db.GetCollection<User>("User");

            var filter = new FilterDefinitionBuilder<User>();

            collection.DeleteOne(filter.Where(u => u.UserName == id));
        }

        public bool Exist(string computername)
        {
            return GetAllComputers().SingleOrDefault(c => c.Name == computername) != null;
        }

        public bool Exists(string username)
        {
            return GetAllUsers().SingleOrDefault(u => u.UserName == username) != null;
        }

        public bool Exists(string username, string name)
        {
            var profileList = GetProfilesForUser(username);
            return profileList.Count() >= 1;
        }

        public IEnumerable<Computer> GetAllComputers()
        {
            var db = client.GetDatabase("LightLinkProfiles");

            var collection = db.GetCollection<Computer>("Computer");

            var results = new List<Computer>();

            var filter = new FilterDefinitionBuilder<Computer>();

            using (var cursor = collection.FindSync(FilterDefinition<Computer>.Empty))
            {
                while (cursor.MoveNext())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        results.Add(document);
                    }
                }
            }

            return results;
        }

        public IEnumerable<Computer> GetAllUnAssignedComputers()
        {
            return GetAllComputers().Where(c => string.IsNullOrEmpty(c.UserName));
        }

        public IEnumerable<User> GetAllUsers()
        {
            var db = client.GetDatabase("LightLinkProfiles");

            var collection = db.GetCollection<User>("User");

            var results = new List<User>();

            var filter = new FilterDefinitionBuilder<User>();

            using (var cursor = collection.FindSync(FilterDefinition<User>.Empty))
            {
                while (cursor.MoveNext())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        results.Add(document);
                    }
                }
            }

            return results;
        }

        public Computer GetComputerForUser(string username, string computerName)
        {
            return GetAllComputers().SingleOrDefault(c => c.UserName == username && c.Name == computerName);
        }

        public IEnumerable<Computer> GetComputersForUser(string username)
        {
            return GetAllComputers().Where(c => c.UserName == username);
        }

        public IEnumerable<Profile> GetProfilesForUser(string username)
        {
            IEnumerable<Profile> profiles = GetAllUsers().SingleOrDefault(u => u.UserName == username).Profiles;
            return profiles;
        }

        public User GetUserById(string id)
        {
            return GetAllUsers().SingleOrDefault(u => u.UserName == id);
        }

        public void RemoveComputer(string computerName)
        {
            var db = client.GetDatabase("LightLinkProfiles");

            var collection = db.GetCollection<Computer>("Computer");

            var filter = new FilterDefinitionBuilder<Computer>();

            collection.DeleteOne(filter.Where(c => c.Name == computerName));
        }

        public void RemoveProfileFromUser(string username, string name)
        {
            var users = GetAllUsers();

            var user = users.SingleOrDefault(u => u.UserName == username);

            Profile p = user.Profiles.SingleOrDefault(p => p.Name == name);

            user.Profiles.Remove(p);
        }

        public void UpdateComputer(string computerName, Computer dto)
        {
            var db = client.GetDatabase("LightLinkProfiles");
            var collection = db.GetCollection<Computer>("Computer");
            var filter = new FilterDefinitionBuilder<Computer>();
            dto.Id = collection.Find((c) => c.Name == dto.Name).First().Id;

            collection.ReplaceOne(filter.Where(c => c.Name == computerName), dto);
        }

        public void UpdateProfileOnUser(string username, string name, Profile dto)
        {
            var db = client.GetDatabase("LightLinkProfiles");

            var collection = db.GetCollection<User>("User");

            var users = GetAllUsers();

            var requestedUser = users.SingleOrDefault(u => u.UserName == username);

            Profile p = requestedUser.Profiles.SingleOrDefault(p => p.Name == name);

            requestedUser.Profiles.Remove(p);
            requestedUser.Profiles.Add(dto);

            var filter = new FilterDefinitionBuilder<User>();

            collection.ReplaceOne(filter.Where(u => u.UserName == username), requestedUser);
        }

        public void UpdateUser(string id, User dto)
        {
            var db = client.GetDatabase("LightLinkProfiles");

            var collection = db.GetCollection<User>("User");

            var filter = new FilterDefinitionBuilder<User>();

            collection.ReplaceOne(filter.Where(u => u.UserName == id), dto);
        }

        public User Authenticate(UserLogin logInfo)
        {
            return GetAllUsers().SingleOrDefault(u => u.Password == logInfo.Password && u.UserName == logInfo.Username);
        }

        public Profile GetActiveForUser(string username)
        {
            return GetProfilesForUser(username).SingleOrDefault(p => p.IsActive);
        }

        public void SetActiveForUser(string username, Profile dto)
        {
            var db = client.GetDatabase("LightLinkProfiles");
            var collection = db.GetCollection<User>("User");
            var user = collection.Find((u) => u.UserName == username).FirstOrDefault();
            var filterDefinition = new FilterDefinitionBuilder<User>();
            foreach (var profile in user.Profiles)
            {
                profile.IsActive = profile.Name == dto.Name;
            }
            collection.ReplaceOne(filterDefinition.Where(u => u.UserName == username), user);
        }
    }
}
