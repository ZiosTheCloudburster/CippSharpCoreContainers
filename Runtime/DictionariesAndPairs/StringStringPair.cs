using System;

namespace CippSharp.Core.Containers
{
    [Serializable]
    public class StringStringPair : SerializedKeyValuePair<string, string>, IEquatable<StringStringPair>
    {
        public static readonly StringStringPair Empty = new StringStringPair();
        
        public StringStringPair()
        {
            
        }

        public StringStringPair(string key, string value) : this()
        {
            this.key = key;
            this.value = value;
        }

        #region Equality Memebers

        public bool Equals(StringStringPair other)
        {
            return string.Equals(key, other.key) && string.Equals(value, other.value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is StringStringPair other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((key != null ? key.GetHashCode() : 0) * 397) ^ (value != null ? value.GetHashCode() : 0);
            }
        }

        public static bool operator ==(StringStringPair left, StringStringPair right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(StringStringPair left, StringStringPair right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}
