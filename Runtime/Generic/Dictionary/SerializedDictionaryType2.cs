using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Serialized Dictionary Type'2
    /// 
    /// Consider this like an abstract class
    /// 
    /// Usage: Inherit from this class with a custom Serializable class to use this on inspector. 
    /// 
    /// Purpose: populate this by Inspector, during initialization get the Dictionary with ToDictionary at your script Initialization and store it.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class SerializedDictionary<TKey, TValue> : SerializedDictionaryBase, 
        IContainer<object>, IContainer<object[]>, IContainerPair<List<TKey>, List<TValue>>, IDictionaryContainer<TKey, TValue>,
        ISimplePair<List<TKey>, List<TValue>>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> keys = new List<TKey>();
        public List<TKey> Key => keys;
        [SerializeField] private List<TValue> values = new List<TValue>();
        public List<TValue> Value => values;

#if UNITY_EDITOR
        [SerializeField, NotEditable] private string messages = string.Empty;
        private float update = 1.0f;
        private double editorTime = 0;
        private double nextUpdate = 0;
#endif
        
        #region Items
        
        /// <summary>
        /// Items array
        /// </summary>
        protected readonly object[] items = new object[2];

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
        protected virtual object[] Items
        {
            get
            {
                items[0] = keys;
                items[1] = values;
                return items;
            }
            set
            {
                this.keys = CastUtils.ToOrDefault<List<TKey>>(value[0]);
                this.values = CastUtils.ToOrDefault<List<TValue>>(value[1]);
            }
        }
        
        #endregion

        public SerializedDictionary()
        {
            
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <exception cref="IndexOutOfRangeException">if keys count doesn't match values count</exception>
        /// <exception cref="Exception">if keys contains duplicates</exception>
        public SerializedDictionary(List<TKey> keys, List<TValue> values)
        {
            if (keys != null && values != null)
            {
                if (keys.Count != values.Count)
                {
                    //Not Supported
                    throw new IndexOutOfRangeException($"{nameof(keys)} and {nameof(values)} must have same count.");
                }
            }

            if (ArrayUtils.HasDuplicates(keys))
            {
                //NotSupported for dictionaries
                throw new Exception(DictionariesDuplicatesError);
            }
            
            this.keys = keys;
            this.values = values;
        }

        public SerializedDictionary(Dictionary<TKey, TValue> dictionary)
        {
            ReadDictionary(dictionary);
        }
        
        #region IContainerBase and IContainer Implementation
        
        /// <summary>
        /// The type of the stored value
        /// </summary>
        public override Type ContainerType => typeof(SerializedDictionary<TKey, TValue>);

        /// <summary>
        /// Retrieve all the contained element
        /// </summary>
        /// <returns></returns>
        public override object GetValueRaw()
        {
            return ToDictionary();
        }
        
        /// <summary>
        /// Retrieve first of the two elements
        /// </summary>
        /// <returns></returns>
        public virtual List<TKey> GetKey()
        {
            return keys;
        }
        
        /// <summary>
        /// Retrieve second of the two elements
        /// </summary>
        /// <returns></returns>
        public virtual List<TValue> GetValue()
        {
            return values;
        }


        /// <summary>
        /// Retrieve <see cref="Items"/>
        /// </summary>
        /// <returns></returns>
        object[] IContainer<object[]>.GetValue()
        {
            return Items;
        }

        /// <summary>
        /// Read/Write on data/value
        /// </summary>
        /// <param name="access"></param>
        public override void Access(GenericAccessDelegate access)
        {
            object o = ToDictionary();
            access.Invoke(ref o);
            ReadDictionary((Dictionary<TKey, TValue>)o);
        }
        
        /// <summary>
        /// Read/Write on data/value
        /// </summary>
        /// <param name="access"></param>
        public virtual void Access(AccessDelegate<object> access)
        {
            object o = Items;
            access.Invoke(ref o);
            Items = (object[])o;
        }
       
        /// <summary>
        /// Read/Write on data/value
        /// </summary>
        /// <param name="access"></param>
        public virtual void Access(AccessDelegate<object[]> access)
        {
            object[] o = Items;
            access.Invoke(ref o);
            Items = o;
        }
        
        /// <summary>
        /// Read/Write on data/value
        /// </summary>
        /// <param name="access"></param>
        public virtual void Access(AccessDelegate<List<TKey>, List<TValue>> access)
        {
            access.Invoke(ref keys, ref values);
        }

        /// <summary>
        /// Predicate on data/value
        /// </summary>
        /// <param name="access"></param>
        public override bool Check(PredicateGenericAccessDelegate access)
        {
            object o = ToDictionary();
            bool check = access.Invoke(ref o);
            ReadDictionary((Dictionary<TKey, TValue>)o);
            return check;
        }
        
        /// <summary>
        /// Predicate on data/value
        /// </summary>
        /// <param name="access"></param>
        public virtual bool Check(PredicateAccessDelegate<object> access)
        {
            object o = Items;
            bool check = access.Invoke(ref o);
            Items = (object[])o;
            return check;
        }
        
        /// <summary>
        /// Predicate on data/value
        /// </summary>
        /// <param name="access"></param>
        public virtual bool Check(PredicateAccessDelegate<object[]> access)
        {
            object[] o = Items;
            bool check = access.Invoke(ref o);
            Items = o;
            return check;
        }
        
        /// <summary>
        /// Predicate on data/value
        /// </summary>
        /// <param name="access"></param>
        public virtual bool Check(PredicateAccessDelegate<List<TKey>, List<TValue>> access)
        {
            return access.Invoke(ref keys, ref values);
        }

        object IContainer<object>.GetValue()
        {
            return GetValue();
        }

        public override void Set(object newValue)
        {
            switch (newValue)
            {
                case Dictionary<TKey, TValue> dictionary:
                    ReadDictionary(dictionary);
                    break;
                case object[] newItems:
                    Items = newItems;
                    break;
            }
        }
        
        public virtual void Set(object[] newItems)
        {
            Items = newItems;
        }
        
        public virtual void Set(List<TKey> newKeys, List<TValue> newValues)
        {
            this.keys = newKeys;
            this.values = newValues;
        }

        public virtual void SetKey(List<TKey> newKeys)
        {
            this.keys = newKeys;
        }

        public virtual void SetValue(List<TValue> newValues)
        {
            this.values = newValues;
        }

        #endregion

        #region ICollectionContainer and IDictionaryContainer Implementation

        /// <summary>
        /// Stored Collection
        /// </summary>
        public virtual ICollection<KeyValuePair<TKey, TValue>> Collection => ToDictionary();

        /// <summary>
        /// Dictionary Readonly
        /// </summary>
        public virtual IDictionary<TKey, TValue> Dictionary => ToDictionary();

        /// <summary>
        /// Keys
        /// </summary>
        public virtual ICollection<TKey> Keys => keys;

        /// <summary>
        /// Values
        /// </summary>
        public virtual ICollection<TValue> Values => values;
       
        /// <summary>
        /// Element at index
        /// </summary>
        /// <param name="index"></param>
        public virtual KeyValuePair<TKey, TValue> this[int index]
        {
            get => new KeyValuePair<TKey, TValue>(keys[index], values[index]);
            set => this[value.Key] = value.Value;
        }
        
        /// <summary>
        /// Value at Key
        /// </summary>
        /// <param name="key"></param>
        public virtual TValue this[TKey key]
        {
            get
            {
                int index = keys.IndexOf(key);
                return values[index];
            }
            set
            {
                int index = keys.IndexOf(key);
                //if that is not contained... add a new element as in real c# dictionaries 
                if (!ArrayUtils.IsValidIndex(index, values))
                {
                    keys.Add(key);
                    values.Add(value);
                }
                else
                {
                    values[index] = value;
                }
            }
        }

        /// <summary>
        /// Count of elements in this collection
        /// </summary>
        public virtual int Count => keys?.Count ?? 0;

        public override void Clear()
        {
            keys.Clear();
            values.Clear();
        }

        /// <summary>
        /// Access the collection of key value pairs
        /// </summary>
        /// <param name="access"></param>
        public virtual void Access(AccessDelegate<ICollection<KeyValuePair<TKey, TValue>>> access)
        {
            ICollection<KeyValuePair<TKey, TValue>> c = ToDictionary();
            access.Invoke(ref c);
            ReadCollection(c);
        }

        /// <summary>
        /// Check the collection of key value pairs
        /// </summary>
        /// <param name="access"></param>
        /// <returns></returns>
        public virtual bool Check(PredicateAccessDelegate<ICollection<KeyValuePair<TKey, TValue>>> access)
        {
            ICollection<KeyValuePair<TKey, TValue>> c = ToDictionary();
            bool check = access.Invoke(ref c);
            ReadCollection(c);
            return check;
        }

        /// <summary>
        /// Set a new collection
        /// </summary>
        /// <param name="newValue"></param>
        public virtual void Set(ICollection<KeyValuePair<TKey, TValue>> newValue)
        {
            ReadCollection(newValue);
        }
        
        /// <summary>
        /// WARNING: you cannot add an element with same key.
        /// </summary>
        /// <param name="element"></param>
        public virtual void Add(KeyValuePair<TKey, TValue> element)
        {
            if (!TryAdd(element))
            {
                Debug.LogWarning(DictionariesKeyAlreadyPresentWarning);
            }
        }

        /// <summary>
        /// WARNING: you cannot add multiple elements with same key
        /// </summary>
        /// <param name="enumerable"></param>
        public virtual void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> enumerable)
        {
            KeyValuePair<TKey, TValue>[] array = enumerable.ToArray();
            foreach (var t in array)
            {
                TryAdd(t);
            }
        }

        /// <summary>
        /// Tries to add an element to dictionary
        /// </summary>
        /// <param name="element"></param>
        /// <returns>success</returns>
        private bool TryAdd(KeyValuePair<TKey, TValue> element)
        {
            TKey tmpKey = element.Key;
            if (keys.Contains(tmpKey))
            {
               
                return false;
            }

            keys.Add(element.Key);
            values.Add(element.Value);
            return true;
        }
        

        public virtual bool Contains(KeyValuePair<TKey, TValue> element)
        {
            return ToArray().Contains(element);
        }

        public virtual int IndexOf(KeyValuePair<TKey, TValue> element)
        {
            return ToArray().IndexOf(element);
        }

        public virtual bool Find(Predicate<KeyValuePair<TKey, TValue>> predicate, out KeyValuePair<TKey, TValue> result)
        {
            return ArrayUtils.Find(ToArray(), predicate, out result);
        }

        public virtual bool Remove(KeyValuePair<TKey, TValue> element)
        {
            KeyValuePair<TKey, TValue>[] array = ToArray();
            int index = ArrayUtils.IndexOf(array, element);
            if (ArrayUtils.IsValidIndex(index, array))
            {
                keys.RemoveAt(index);
                values.RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void RemoveAt(int index)
        {
            int length = Count;
            if (index >= 0 && index < length)
            {
                keys.RemoveAt(index);
                values.RemoveAt(index);
            }
        }

        public virtual KeyValuePair<TKey, TValue>[] ToArray()
        {
            return ArrayUtils.ToArray(keys, values);
        }

        public override bool IsNullOrEmpty()
        {
            return ArrayUtils.IsNullOrEmpty(keys);
        }
        
        ICollection<KeyValuePair<TKey, TValue>> IContainer<ICollection<KeyValuePair<TKey, TValue>>>.GetValue()
        {
            return ToDictionary();
        }

        public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return ToDictionary().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        #endregion
        
        /// <summary>
        /// This is Dictionary!
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<TKey, TValue> ToDictionary()
        {
            return ArrayUtils.ToDictionary(keys, values);
        }

        /// <summary>
        /// From Collection
        /// </summary>
        /// <param name="collection"></param>
        public virtual void ReadCollection(ICollection<KeyValuePair<TKey, TValue>> collection)
        {
            Clear();
            AddRange(collection);
        }
        
        /// <summary>
        /// From dictionary to 'here.
        /// </summary>
        /// <param name="dictionary"></param>
        public virtual void ReadDictionary(Dictionary<TKey, TValue> dictionary)
        {
            Clear();
            AddRange(dictionary);
        }
      
        /// <summary>
        /// This as KeyValuePair
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<List<TKey>, List<TValue>> ToKeyValuePair()
        {
            return this;
        }

        #region ISerializationCallbackReceiver Implementation

        public virtual void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            editorTime = EditorApplication.timeSinceStartup;
            if (nextUpdate < editorTime)
            {
                nextUpdate = editorTime + update;
                DelayedUpdate();
            }
#endif
        }
        
#if UNITY_EDITOR
        private void DelayedUpdate()
        {
            messages = string.Empty;
            int warningCount = 0;
            if ((keys != null && values != null) && (keys.Count != values.Count))
            {
                messages += $"WARNING: {nameof(keys)} and {nameof(values)} must have same count.";
                warningCount++;
            }
            if (ArrayUtils.HasDuplicates(keys))
            {
                messages += $"{(warningCount > 0 ? "\t" : string.Empty)}WARNING: you keys are containing a duplicate. This is not allowed in dictionaries.";
                warningCount++;
            }
        }
#endif
        public virtual void OnAfterDeserialize()
        {
            
        }

        #endregion

        #region Operators

        public static implicit operator KeyValuePair<List<TKey>, List<TValue>>(SerializedDictionary<TKey, TValue> pair)
        {
            return new KeyValuePair<List<TKey>, List<TValue>>(pair.Key, pair.Value);
        }
        
        public static implicit operator SerializedDictionary<TKey, TValue>(KeyValuePair<TKey, TValue> pair)
        {
            return new SerializedDictionary<TKey, TValue>();
        }

        #endregion
    }
}
