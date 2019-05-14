using System;
using System.Collections.Generic;
using System.Text;

namespace LightLinkModels.Extension_Methods
{
    public static class ICollectionExtensions
    {
        public static bool StructureEquals<T>(this ICollection<T> source, ICollection<T> other)
        {
            bool isEqual = source == other;
            if (!isEqual)
            {
                if (source is null) throw new ArgumentNullException(nameof(source), "source cannot be null.");
                if (other is null) throw new ArgumentNullException(nameof(other), "other cannot be null.");
                if (source.Count != other.Count) return false;
                foreach (var item in source)
                {
                    if (!other.Contains(item))
                    {
                        return false;
                    }
                }
                isEqual = true;
            }
            return isEqual;
        }
    }
}
