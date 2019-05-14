using LightLinkModels.Extension_Methods;
using System;
using System.Collections.Generic;

namespace LightLinkModels
{
    public class Profile: IEquatable<Profile>
    {
        public string Name;
        public Dictionary<string, dynamic> Configurations;
        public override bool Equals(object obj)
        {
            bool isEqual = false;
            if (obj != null && obj is Profile p)
            {
                isEqual = Equals(p);
            }
            return isEqual;
        }

        public bool Equals(Profile other)
        {
            bool isEqual = this == other;
            if (!isEqual)
            {
                isEqual = this.Name == other.Name;
            }
            return isEqual;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}