using System;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Before the Vector2Int you had to use this.
    /// </summary>
    [Serializable]
    public class IndexMap : PairContainer<int, int>, IEquatable<IndexMap>
    {
        /// <summary>
        /// Index Map Zero
        /// </summary>
        public static readonly IndexMap Zero = new IndexMap(0, 0);
        
        public int x;
        public int y;
        
        #region Constructor

        public IndexMap ()
        {
            
        }
        
        public IndexMap(int x = default, int y = default) : this ()
        {
            this.x = x;
            this.y = y;
        }
        
        #endregion

        #region Equality Members

        public bool Equals(IndexMap other)
        {
            return x == other.x && y == other.y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is IndexMap other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x * 397) ^ y;
            }
        }

        #endregion
        
        #region Operators

#if UNITY_2017_2_OR_NEWER
        public static implicit operator Vector2Int(IndexMap map)
        {
            return new Vector2Int(map.x, map.y);
        }

        public static implicit operator IndexMap(Vector2Int v2int)
        {
            return new IndexMap(v2int.x, v2int.y);
        }
#endif
        #endregion
    }
}
