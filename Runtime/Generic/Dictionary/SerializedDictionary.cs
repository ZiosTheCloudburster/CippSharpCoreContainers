using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CippSharp.Core.Containers
{
//    #region Serialized Dictionary Type'2
//    
//    /// <summary>
//    /// Usage: Inherit from this class with a custom Serializable class to use this on inspector. 
//    /// 
//    /// Purpose: populate this by Inspector, during initialization get the Dictionary with ToDictionary at your script Initialization and store it.
//    /// </summary>
//    /// <typeparam name="Key"></typeparam>
//    /// <typeparam name="Value"></typeparam>
//    [Serializable]
//    public abstract class SerializedDictionary<Key, Value> : SerializedDictionaryBase
//    {
//        [SerializeField] private List<Key> keys = new List<Key>();
//        [SerializeField] private List<Value> values = new List<Value>();
//        private object _syncRoot;
//
//        public void AddRange(ICollection<KeyValuePair<Key, Value>> enumerable)
//        {
//            keys.AddRange(enumerable.Select(k => k.Key));
//            values.AddRange(enumerable.Select(k => k.Value));
//        }
//        
//        public override void Clear()
//        {
//            keys.Clear();
//            values.Clear();
//        }
//
//        /// <summary>
//        /// This is Dictionary!
//        /// </summary>
//        /// <returns></returns>
//        public Dictionary<Key, Value> ToDictionary()
//        {
//            List<KeyValuePair<Key, Value>> tmpDictionary = new List<KeyValuePair<Key, Value>>();
//            for (int i = 0; i < keys.Count; i++)
//            {
//                tmpDictionary.Add(new KeyValuePair<Key, Value>(keys[i], values[i]));
//            }
//            return ArrayUtils.ToDictionary(tmpDictionary);
//        }
//        
//        public override bool IsNullOrEmpty()
//        {
//            return ArrayUtils.IsNullOrEmpty(keys);
//        }
//    }
//
//    #endregion
    
    #region Serialized Dictionary Type'3
    
    /// <summary>
    /// Usage: Inherit from this class with a custom Serializable class to use this on inspector.
    /// 
    /// Purpose: populate this by Inspector, during initialization get the Dictionary with ToDictionary at your script Initialization and store it.
    ///
    /// Notes: It Supports Reorderable Attribute from CippSharp.ReorderableList 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    [Serializable]
    public abstract class SerializedDictionary<T, Key, Value> : SerializedDictionaryBase
        where T : SerializedKeyValuePair<Key, Value>
    {
        [SerializeField] public List<T> list = new List<T>();
        private object _syncRoot;

        public SerializedDictionary()
        {
            
        }

        public SerializedDictionary(IEnumerable<T> range)
        {
            list.AddRange(range);
        }
        
        public Dictionary<Key, Value> ToDictionary()
        {
            return ArrayUtils.ToDictionary(list.Select(e => e.ToKeyValuePair()));
        }

        public override void Clear()
        {
           list.Clear();
        }

        public void Add(T element)
        {
            list.Add(element);
        }

        public void AddRange(IEnumerable<T> enumerable)
        {
            list.AddRange(enumerable);
        }
        
        public override bool IsNullOrEmpty()
        {
            return ArrayUtils.IsNullOrEmpty(list);
        }

        public List<T> GetPairs()
        {
            return list;
        }

        #region Operators

//        public static implicit operator ListContainer<T>(SerializedDictionary<T, Key, Value> serializedDictionary)
//        {
//            return serializedDictionary.list;
//        }

        #endregion
    }
    
    #endregion
}
