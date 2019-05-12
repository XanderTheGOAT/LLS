using System;
using System.Collections.Generic;

namespace LightLinkModels.Comparers
{
    internal class UserEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            if (x is null) throw new ArgumentNullException(nameof(x), "x cannot be null.");
            if (y is null) throw new ArgumentNullException(nameof(y), "y cannot be null.");
            return x.UserName.Equals(y.UserName);
        }

        public int GetHashCode(User obj)
        {
            if (obj is null) throw new ArgumentNullException(nameof(obj), "object cannot be null.");
            return obj.UserName.GetHashCode();
        }
    }
}
