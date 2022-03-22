using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    #region Serialized Dictionary Type'2

    /// <summary>
    /// Consider this like an abstract class
    /// 
    /// Usage: Inherit from this class with a custom Serializable class to use this on inspector. 
    /// 
    /// Purpose: populate this by Inspector, during initialization get the Dictionary with ToDictionary at your script Initialization and store it.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class SerializedDictionary<TKey, TValue> : SerializedDictionaryBase, IContainerPair<List<TKey>, List<TValue>>, ISimplePair<List<TKey>, List<TValue>>
    {
        [SerializeField] private List<TKey> keys = new List<TKey>();
        [SerializeField] private List<TValue> values = new List<TValue>();
        
        #region IContainerBase and IContainer Implementation
        
        /// <summary>
        /// The type of the stored value
        /// </summary>
        public override Type ContainerType => typeof(SerializedDictionary<TKey, TValue>);

        public override object GetValueRaw()
        {
            return ToDictionary();
        }

        public override void Access(GenericAccessDelegate access)
        {
            object o = ToDictionary();
            access.Invoke(ref o);
            FromDictionary((Dictionary<TKey, TValue>)o);
        }

        public override bool Check(PredicateGenericAccessDelegate access)
        {
            throw new NotImplementedException();
        }

        public override void Set(object newValue)
        {
            throw new NotImplementedException();
        }

        #endregion
        
        public override void Clear()
        {
            keys.Clear();
            values.Clear();
        }

        /// <summary>
        /// This is Dictionary!
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<TKey, TValue> ToDictionary()
        {
            return ArrayUtils.ToDictionary(keys, values);
        }

        /// <summary>
        /// From dictionary to 'here.
        /// </summary>
        /// <param name="dictionary"></param>
        public virtual void FromDictionary(Dictionary<TKey, TValue> dictionary)
        {
            ArrayUtils.FromDictionary(dictionary, out keys, out values);
        }
        
        
        
        public override bool IsNullOrEmpty()
        {
            return ArrayUtils.IsNullOrEmpty(keys);
        }

        public List<TKey> GetKey()
        {
            throw new NotImplementedException();
        }

        public List<TValue> GetValue()
        {
            throw new NotImplementedException();
        }

        public void Access(AccessDelegate<List<TKey>, List<TValue>> access)
        {
            throw new NotImplementedException();
        }

        public bool Check(PredicateAccessDelegate<List<TKey>, List<TValue>> access)
        {
            throw new NotImplementedException();
        }

        public void Set(List<TKey> newKey, List<TValue> newValue)
        {
            throw new NotImplementedException();
        }

        public void SetKey(List<TKey> newKey)
        {
            throw new NotImplementedException();
        }

        public void SetValue(List<TValue> newValue)
        {
            throw new NotImplementedException();
        }

        public List<TKey> Key { get; }
        public List<TValue> Value { get; }
        public KeyValuePair<List<TKey>, List<TValue>> ToKeyValuePair()
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
