using LightLinkModels.Extension_Methods;
using System;
using System.Collections.Generic;

namespace LightLinkModels
{
    public class User: IEquatable<User>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public ICollection<Profile> Profiles { get; set; }

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
