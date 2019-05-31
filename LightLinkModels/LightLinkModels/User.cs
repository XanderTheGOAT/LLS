using LightLinkModels.Extension_Methods;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LightLinkModels
{
    public class User : IEquatable<User>
    {
        private string userName;
        private string password;
        [JsonIgnore]
        public ObjectId Id { get; set; }
        public string UserName { get => userName ?? ""; set => userName = value; }
        public string Password { get => password ?? ""; set => password = value; }
        public ICollection<Profile> Profiles { get; set; }

        public User()
        {
            Profiles = new List<Profile>();

        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj != null && obj is User u)
            {
                isEqual = Equals(u);
            }
            return isEqual;
        }

        public bool Equals(User other)
        {
            bool isEqual = this == other;
            if (!isEqual && other != null)
            {
                isEqual = other.UserName == UserName;
            }
            return isEqual;
        }

        public override int GetHashCode()
        {
            return UserName.GetHashCode();
        }
    }
}
