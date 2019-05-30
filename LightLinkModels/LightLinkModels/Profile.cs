using LightLinkModels.Extension_Methods;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace LightLinkModels
{
    public class Profile: IEquatable<Profile>
    {
        private string name;

        public ObjectId Id { get; set; }
        public string Name { get => name ?? ""; set => name = value; }
        public IDictionary<string, dynamic> Configurations { get; set; }

        public Profile()
        {
            Configurations = new Dictionary<string, dynamic>();
        }

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

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder("Name: ").Append(Name);
            foreach (var pair in Configurations)
            {                
                builder.Append("Device: ").Append(pair.Key).Append(" \\ Color: ").Append(pair.Value);
            }
            return builder.ToString();
        }
    }
}