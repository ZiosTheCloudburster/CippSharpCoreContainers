using System;
using System.Collections.Generic;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Before the Vector2Int you had to use this.
    /// </summary>
    [Serializable]
    public struct IndexMap : IEquatable<IndexMap>, IContainerPair<int, int>, ISimplePair<int, int>
    {
        /// <summary>
        /// Index Map Zero
        /// </summary>
        public static readonly IndexMap Zero = new IndexMap(0, 0);
        
        public int x;
        public int Key => x;
        public int y;
        public int Value => y;
        
        
        #region Items
        
        /// <summary>
        /// Items array
        /// </summary>
        private object[] items;

        /// <summary>
        /// Items array property.
        /// 
        /// Usage: get is 'readonly'.
        ///
        /// Example:
        /// { 
        ///    object[] pairContainerItems = myPairContainer.Items;
        ///     //DoStuffs;
        ///    myPairContainer.Items = pairContainerItems;
        /// } 
        /// </summary>
        private object[] Items
        {
            get
            {
                if (ArrayUtils.IsNullOrEmpty(items))
                {
                    items = new object[2];
                }
                else if (items.Length != 2)
                {
                    Array.Resize(ref items, 2);
                }
                
                items[0] = x;
                items[1] = y;
                return items;
            }
            set
            {
                this.x = CastUtils.ToOrDefault<int>(value[0]);
                this.y = CastUtils.ToOrDefault<int>(value[1]);
            }
        }
        
        #endregion

        #region Constructor
        
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

        #region IContainerPair Implementation

        public Type ContainerType => typeof(IContainerPair<int, int>);
        
        public object GetValueRaw()
        {
            return Items;
        }
        
        public int GetKey()
        {
            return x;
        }

        public int GetValue()
        {
            return y;
        }

        public void Access(GenericAccessDelegate access)
        {
            object o = Items;
            access.Invoke(ref o);
            Items = (object[])o;
        }

        public void Access(AccessDelegate<int, int> access)
        {
            access.Invoke(ref x, ref y);
        }
        
        public bool Check(PredicateGenericAccessDelegate access)
        {
            object o = Items;
            bool check = access.Invoke(ref o);
            Items = (object[])o;
            return check;
        }

        public bool Check(PredicateAccessDelegate<int, int> access)
        {
            return access.Invoke(ref x, ref y);
        }
        
        public void Set(object newValue)
        {
            if (CastUtils.To(newValue, out object[] newItems))
            {
                Items = newItems;
            }
        }

        public void Set(int newKey, int newValue)
        {
            x = newKey;
            y = newValue;
        }

        public void SetKey(int newKey)
        {
            x = newKey;
        }

        public void SetValue(int newValue)
        {
            y = newValue;
        }

        #endregion
        
        public KeyValuePair<int, int> ToKeyValuePair()
        {
            return new KeyValuePair<int, int>(Key, Value);
        }

        #region Operators

        public static implicit operator KeyValuePair<int, int>(IndexMap map)
        {
            return new KeyValuePair<int, int>(map.Key, map.Value);
        }

        public static implicit operator IndexMap(KeyValuePair<int, int> kvp)
        {
            return new IndexMap(kvp.Key, kvp.Value);
        }

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
