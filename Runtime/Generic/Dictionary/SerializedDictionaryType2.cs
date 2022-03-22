//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;
//
//namespace CippSharp.Core.Containers
//{
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
//    public abstract class SerializedDictionary<Key, Value> : SerializedDictionaryBase, IContainer<Tuple<List<Key>, List<Value>>>, ISimplePair<List<Key>, List<Value>>
//    {
//        [SerializeField] private List<Key> keys = new List<Key>();
//        [SerializeField] private List<Value> values = new List<Value>();
//
////        private Tuple<List<Key>, List<Value>> m_value = new Tuple<List<Key>, List<Value>>(null, null);
//        /// <summary>
//        /// Get / Set of value as Tuple
//        /// </summary>
//        private Tuple<List<Key>, List<Value>> value 
//        {
//            get => new Tuple<List<Key>, List<Value>>(keys, values);
//            set
//            {
//                this.keys = value.Item1;
//                this.values = value.Item2;
//            }
//        }
//
//    #region IContainerBase and IContainer Implementation
//        
//        /// <summary>
//        /// The type of the stored value
//        /// </summary>
//        public override Type ContainerType => typeof(Tuple<List<Key>, List<Value>>);
//
//        /// <summary>
//        /// Retrieve the contained element
//        /// </summary>
//        /// <returns></returns>
//        public override object GetValueRaw()
//        {
//            return value;
//        }
//
//        /// <summary>
//        /// Retrieve the contained element
//        /// </summary>
//        public virtual Tuple<List<Key>, List<Value>> GetValue ()
//        {
//            return value;
//        }
//
//        /// <summary>
//        /// Read/Write on data/value
//        /// </summary>
//        /// <param name="access"></param>
//        public override void Access(GenericAccessDelegate access)
//        {
//            object o = value;
//            access.Invoke(ref o);
//            value = (Tuple<List<Key>, List<Value>>)o;
//        }
//        
//        /// <summary>
//        /// Custom edit of data
//        /// </summary>
//        /// <param name="access"></param>
//        public virtual void Access(AccessDelegate<Tuple<List<Key>, List<Value>>> access)
//        {
//            Tuple<List<Key>, List<Value>> t = value;
//            access.Invoke(ref t);
//            value = t;
//        }
//        
//        /// <summary>
//        /// Predicate on data/value
//        /// </summary>
//        /// <param name="access"></param>
//        public override bool Check(PredicateGenericAccessDelegate access)
//        {
//            object o = value;
//            bool b = access.Invoke(ref o);
//            value = (T)o;
//            return b;
//        }
//        
//        /// <summary>
//        /// Predicate on data/value
//        /// </summary>
//        /// <param name="access"></param>
//        public virtual bool Check(PredicateAccessDelegate<T> access)
//        {
//            return access.Invoke(ref value);
//        }
//        
//        /// <summary>
//        /// Set the contained element
//        /// </summary>
//        /// <param name="newValue"></param>
//        public override void Set(object newValue)
//        {
//            if (newValue.To<T>(out T tmp))
//            {
//                value = tmp;
//            }
//        }
//        
//        /// <summary>
//        /// Set the contained element
//        /// </summary>
//        /// <param name="newData"></param>
//        public virtual void Set(T newData)
//        {
//            value = newData;
//        }
//
//        #endregion
//        
////        private object _syncRoot;
//
////        public void AddRange(ICollection<KeyValuePair<Key, Value>> enumerable)
////        {
////            keys.AddRange(enumerable.Select(k => k.Key));
////            values.AddRange(enumerable.Select(k => k.Value));
////        }
////        
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
//}
