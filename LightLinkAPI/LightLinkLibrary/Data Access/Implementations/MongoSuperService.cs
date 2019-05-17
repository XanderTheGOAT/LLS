using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using LightLinkModels;
using MongoDB.Driver;

namespace LightLinkLibrary.Data_Access.Implementations
{
    public class MongoSuperService : IComputerService, IProfileService, IUserService
    {
        MongoClient client = new MongoClient("mongodb://localhost:27017");

        public void AddComputer(Computer dto)
        {
            IMongoDatabase db = client.GetDatabase("LightLinkProfiles");

            IMongoCollection<Computer> collection = db.GetCollection<Computer>("Computer");

            if (!GetAllComputers().Contains(dto))
            {
                collection.InsertOne(dto);
            }
        }

        public void AddProfileToUser(string username, Profile dto)
        {
            IMongoDatabase db = client.GetDatabase("LightLinkProfiles");

            IMongoCollection<User> collection = db.GetCollection<User>("User");

            FilterDefinitionBuilder<User> filter = new FilterDefinitionBuilder<User>();

            UpdateDefinitionBuilder<User> builder = new UpdateDefinitionBuilder<User>();

            if (!GetProfilesForUser(username).Contains(dto))
            {
                collection.UpdateOne(filter.Where(u => u.UserName == username), builder.AddToSet("Profiles", dto));
            }
        }

        public void AddUser(User dto)
        {
            IMongoDatabase db = client.GetDatabase("LightLinkProfiles");

            IMongoCollection<User> collection = db.GetCollection<User>("User");

            if (!GetAllUsers().Contains(dto))
            {
                collection.InsertOne(dto);
            }
        }

        public void DeleteUser(string id)
        {
            IMongoDatabase db = client.GetDatabase("LightLinkProfiles");

            IMongoCollection<User> collection = db.GetCollection<User>("User");

            FilterDefinitionBuilder<User> filter = new FilterDefinitionBuilder<User>();

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
            IEnumerable<Profile> profileList = GetProfilesForUser(username);
            return profileList.Count() >= 1;
        }

        public IEnumerable<Computer> GetAllComputers()
        {
            IMongoDatabase db = client.GetDatabase("LightLinkProfiles");

            IMongoCollection<Computer> collection = db.GetCollection<Computer>("Computer");

            List<Computer> results = new List<Computer>();

            FilterDefinitionBuilder<Computer> filter = new FilterDefinitionBuilder<Computer>();

            using (IAsyncCursor<Computer> cursor = collection.FindSync(FilterDefinition<Computer>.Empty))
            {
                while (cursor.MoveNext())
                {
                    IEnumerable<Computer> batch = cursor.Current;
                    foreach (Computer document in batch)
                    {
                        results.Add(document);
                    }
                }
            }

            return results;
        }

        public IEnumerable<Computer> GetAllUnAssignedComputers()
        {
            return GetAllComputers().Where(c => String.IsNullOrEmpty(c.UserName));
        }

        public IEnumerable<User> GetAllUsers()
        {
            IMongoDatabase db = client.GetDatabase("LightLinkProfiles");

            IMongoCollection<User> collection = db.GetCollection<User>("User");

            List<User> results = new List<User>();

            FilterDefinitionBuilder<User> filter = new FilterDefinitionBuilder<User>();

            using (IAsyncCursor<User> cursor = collection.FindSync(FilterDefinition<User>.Empty))
            {
                while (cursor.MoveNext())
                {
                    IEnumerable<User> batch = cursor.Current;
                    foreach (User document in batch)
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
            IMongoDatabase db = client.GetDatabase("LightLinkProfiles");

            IMongoCollection<Computer> collection = db.GetCollection<Computer>("Computer");

            FilterDefinitionBuilder<Computer> filter = new FilterDefinitionBuilder<Computer>();

            collection.DeleteOne(filter.Where(c => c.Name == computerName));
        }

        public void RemoveProfileFromUser(string username, string name)
        {
            IEnumerable<User> users = GetAllUsers();

            User user = users.SingleOrDefault(u => u.UserName == username);

            Profile p = user.Profiles.SingleOrDefault(p => p.Name == name);

            user.Profiles.Remove(p);
        }

        public void UpdateComputer(string computerName, Computer dto)
        {
            IMongoDatabase db = client.GetDatabase("LightLinkProfiles");

            IMongoCollection<Computer> collection = db.GetCollection<Computer>("Computer");

            FilterDefinitionBuilder<Computer> filter = new FilterDefinitionBuilder<Computer>();

            collection.ReplaceOne(filter.Where(c => c.Name == computerName), dto);
        }

        public void UpdateProfileOnUser(string username, string name, Profile dto)
        {
            IMongoDatabase db = client.GetDatabase("LightLinkProfiles");

            IMongoCollection<User> collection = db.GetCollection<User>("User");

            IEnumerable<User> users = GetAllUsers();

            User requestedUser = users.SingleOrDefault(u => u.UserName == username);

            Profile p = requestedUser.Profiles.SingleOrDefault(p => p.Name == name);

            requestedUser.Profiles.Remove(p);
            requestedUser.Profiles.Add(dto);

            FilterDefinitionBuilder<User> filter = new FilterDefinitionBuilder<User>();

            collection.ReplaceOne(filter.Where(u => u.UserName == username), requestedUser);
        }

        public void UpdateUser(string id, User dto)
        {
            IMongoDatabase db = client.GetDatabase("LightLinkProfiles");

            IMongoCollection<User> collection = db.GetCollection<User>("User");

            FilterDefinitionBuilder<User> filter = new FilterDefinitionBuilder<User>();

            collection.ReplaceOne(filter.Where(u => u.UserName == id), dto);
        }
    }
}
