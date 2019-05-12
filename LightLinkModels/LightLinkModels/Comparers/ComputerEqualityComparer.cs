using System;
using System.Collections.Generic;

namespace LightLinkModels.Comparers
{
    public class ComputerEqualityComparer : IEqualityComparer<Computer>
    {
        public bool Equals(Computer x, Computer y)
        {
            if (x is null)
            {
                throw new ArgumentNullException(nameof(x), "x cannot be null.");
            }

            if (y is null)
            {
                throw new ArgumentNullException(nameof(y), "y cannot be null.");
            }

            return x.Name.Equals(y.Name);
        }

        public int GetHashCode(Computer obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj), "object cannot be null.");
            }

            return obj.Name.GetHashCode();
        }
    }
}
