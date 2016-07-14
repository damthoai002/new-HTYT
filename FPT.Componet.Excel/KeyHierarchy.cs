using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPT.Component.ExcelPlus
{
    /// <summary>
    /// Key hierarchy
    /// </summary>
    public class KeyHierarchy<T> : IEquatable<KeyHierarchy<T>>, IComparable<KeyHierarchy<T>>
        where T : IEquatable<T>, IComparable<T>
    {
        public List<T> Keys { get; set; }

        public KeyHierarchy()
        {
            Keys = new List<T>();
        }

        public KeyHierarchy(List<T> keys)
        {
            Keys = new List<T>(keys.ToArray());
        }

        public KeyHierarchy(T[] keys)
        {
            Keys = new List<T>(keys);
        }

        public override string ToString()
        {
            string result = string.Empty;
            for (int i = 0; i < Keys.Count; i++)
            {
                result += "[" + Keys[i].ToString() + "]";
            }
            return result;
        }

        public override int GetHashCode()
        {
            string result = string.Empty;
            for (int i = 0; i < Keys.Count; i++)
            {
                result += "[" + Keys[i].GetHashCode().ToString() + "]";
            }

            return result.GetHashCode();
        }

        #region IEquatable<KeyHierarchy> Members

        public bool Equals(KeyHierarchy<T> other)
        {
            bool result = true;
            int count = Keys.Count;
            if (count == other.Keys.Count)
            {
                for (int i = 0; i < count; i++)
                {
                    if (!Keys[i].Equals(other.Keys[i]))
                    {
                        result = false;
                        break;
                    }
                }
            }
            else
            {
                result = false;
            }
            return result;
        }

        #endregion

        #region IComparable<KeyHierarchy> Members

        public int CompareTo(KeyHierarchy<T> other)
        {
            int result = 0;
            int count = Keys.Count;
            if (count == other.Keys.Count)
            {
                for (int i = 0; i < Keys.Count; i++)
                {
                    result = Keys[i].CompareTo(other.Keys[i]);
                    if (result != 0)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        #endregion

        #region IEqualityComparer<KeyHierarchy> Members



        #endregion
    }

    public class KeyHierarchyComparer<T> : IEqualityComparer<KeyHierarchy<T>>
         where T : IEquatable<T>, IComparable<T>
    {
        public const int MAX_VALUE_PER_KEY = 1000;

        #region IEqualityComparer<KeyHierarchy> Members
        public bool Equals(KeyHierarchy<T> x, KeyHierarchy<T> y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(KeyHierarchy<T> obj)
        {
            string result = string.Empty;
            for (int i = 0; i < obj.Keys.Count; i++)
            {
                result += "[" + obj.Keys[i].GetHashCode().ToString() + "]";
            }

            return result.GetHashCode();
        }
        #endregion IEqualityComparer<KeyHierarchy> Members
    }
}
