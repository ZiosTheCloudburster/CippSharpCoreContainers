using System;
using System.Collections.Generic;
using UnityEngine;

namespace CippSharp.Core
{
    /// <summary>
    /// Purpose: u must inherit from this class in order to create your custom SerializedKeyValuePair
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    [Serializable]
    public class SerializedKeyValuePair<K, V> : ISimplePair<K, V>
    {
        [SerializeField] protected K key;
        public K Key => key;
        [SerializeField] protected V value;
        public V Value => value;

        public SerializedKeyValuePair(K key, V value)
        {
            this.key = key;
            this.value = value;
        }

        public KeyValuePair<K, V> ToKeyValuePair()
        {
            return this;
        }

        public void SetNewValue(V newValue)
        {
            this.value = newValue;
        }

        public static implicit operator KeyValuePair<K, V>(SerializedKeyValuePair<K, V> sk)
        {
            return new KeyValuePair<K, V>(sk.Key, sk.Value);
        }
        
        public static implicit operator SerializedKeyValuePair<K, V>(KeyValuePair<K, V> k)
        {
            return new SerializedKeyValuePair<K, V>(k.Key, k.Value);
        }
    }
}
