using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightLinkModels
{
    public class Computer: IEquatable<Computer>
    {
        private string name;
        private string userName;
        private string[] connectedDevices;
        
        public string Name { get => name ?? ""; set => name = value; }
        public string UserName { get => userName ?? ""; set => userName = value; }
        public string[] ConnectedDevices { get => connectedDevices.ToArray(); set => connectedDevices = value; }

        public override bool Equals(object obj)
        {
            
            bool isEqual = false;
            if (obj != null && obj is Computer c)
            {
                isEqual = Equals(c);
            }
            return isEqual;
        }

        public bool Equals(Computer other)
        {
            bool isEqual = this == other;
            if (!isEqual && other != null)
            {
                isEqual = Name == other.Name;
            }
            return isEqual;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
