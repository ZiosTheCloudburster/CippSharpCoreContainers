using System;
using System.Collections.Generic;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Consider this like an abstract class
    /// 
    /// Purpose: u must inherit from this class in order to create your custom SerializedKeyValuePair
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class SerializedKeyValuePair<TKey, TValue> : PairContainer<TKey, TValue>
    {
        public SerializedKeyValuePair()
        {
            this.key = default(TKey);
            this.value = default(TValue);
        }
        
        public SerializedKeyValuePair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
        
        #region Operators
        
        public static implicit operator KeyValuePair<TKey, TValue>(SerializedKeyValuePair<TKey, TValue> sk)
        {
            return new KeyValuePair<TKey, TValue>(sk.Key, sk.Value);
        }
        
        public static implicit operator SerializedKeyValuePair<TKey, TValue>(KeyValuePair<TKey, TValue> k)
        {
            return new SerializedKeyValuePair<TKey, TValue>(k.Key, k.Value);
        }
        
        #endregion
    }
}
