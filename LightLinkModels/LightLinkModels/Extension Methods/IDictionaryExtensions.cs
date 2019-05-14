using System;
using System.Collections.Generic;
using System.Text;

namespace LightLinkModels.Extension_Methods
{
    public static class IDictionaryExtensions
    {
        public static bool StructualEquals<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> other)
        {
            bool isEqual = source == other;
            if (!isEqual)
            {
                if (source is null) throw new ArgumentNullException(nameof(source), "source Dictionary cannot be null.");
                if (other is null) throw new ArgumentNullException(nameof(other), "other Dictionary cannot be null.");
                foreach (var key in other.Keys)
                {
                    if (!source.ContainsKey(key))
                    {
                        return false;
                    }
                    else if (!source[key].Equals(other[key]))
                    {
                        return false;
                    }
                }
                isEqual = true;
            }
            return isEqual;
        }

        public static int StructualHashCode<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source), "source cannot be null.");
            int hashedcode = 0;
            foreach (var item in source)
            {
                hashedcode ^= item.Key.GetHashCode() ^ item.Value.GetHashCode();
            }
            return hashedcode;
        }
    }
}
