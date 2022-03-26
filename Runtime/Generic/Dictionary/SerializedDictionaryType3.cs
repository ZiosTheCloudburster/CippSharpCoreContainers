using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Serialized Dictionary Type'3
    /// 
    /// Consider this like an abstract class
    /// 
    /// Usage: Inherit from this class with a custom Serializable class to use this on inspector.
    /// 
    /// Purpose: populate this by Inspector, during initialization get the Dictionary with ToDictionary at your script Initialization and store it.
    ///
    /// Notes: It Supports Reorderable Attribute from CippSharp.ReorderableList 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public abstract class SerializedDictionary<T, TKey, TValue> : SerializedDictionaryBase, IContainer<object>, IContainer<List<T>>,
         IDictionaryContainer<TKey, TValue>, ISerializationCallbackReceiver
        where T : SerializedKeyValuePair<TKey, TValue>
    {
        /// <summary>
        /// When activator tries to create T and it fails
        /// </summary>
        private const string TActivatorFailedMessage = "Failed to create and add a new T instance.";
        
        [FormerlySerializedAs("m_data")]
        [FormerlySerializedAs("data")]
        [FormerlySerializedAs("m_list")]
        [FormerlySerializedAs("list")]
        [FormerlySerializedAs("array")]
        [FormerlySerializedAs("corners")]
        [FormerlySerializedAs("value")]
        [SerializeField] protected List<T> value = new List<T>();
        
        /// <summary>
        /// this has minus 'l' as old name reference
        /// </summary>
        public virtual List<T> list
        {
            get => this.value;
            set => this.value = value;
        }

#if UNITY_EDITOR
        [SerializeField, NotEditable] private string messages = string.Empty;
        private float update = 1.0f;
        private double editorTime = 0;
        private double nextUpdate = 0;
#endif
        
        public SerializedDictionary()
        {
            
        }

        public SerializedDictionary(IEnumerable<T> range)
        {
            if (ArrayUtils.HasDuplicates(range.Select(t => t.Key)))
            {
                //NotSupported for dictionaries
                throw new Exception(DictionariesDuplicatesError);
            }
            
            Clear();
            AddRange(range);
        }
        
         #region IContainerBase and IContainer Implementation
        
        /// <summary>
        /// The type of the stored value
        /// </summary>
        public override Type ContainerType => typeof(List<T>);

        /// <summary>
        /// Retrieve all the contained element
        /// </summary>
        /// <returns></returns>
        public override object GetValueRaw()
        {
            return ToDictionary();
        }
        
        public List<T> GetValue()
        {
            return value;
        }
        
        public List<T> GetPairs()
        {
            return value;
        }
        
        object IContainer<object>.GetValue()
        {
            return GetValue();
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
            object o = ToDictionary(value);
            access.Invoke(ref o);
            ReadDictionary((Dictionary<TKey, TValue>)o);
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
            object o = ToDictionary();
            bool check = access.Invoke(ref o);
            ReadDictionary((Dictionary<TKey, TValue>)o);
            return check;
        }
        
       
        public override void Set(object newValue)
        {
            switch (newValue)
            {
                case Dictionary<TKey, TValue> dictionary:
                    ReadDictionary(dictionary);
                    break;
            }
        }

        #endregion
        
        #region ICollectionContainer and IDictionaryContainer Implementation

        /// <summary>
        /// Stored Collection
        /// </summary>
        public virtual ICollection<KeyValuePair<TKey, TValue>> Collection => ToDictionary();

        public virtual IDictionary<TKey, TValue> Dictionary => ToDictionary();
        
        /// <summary>
        /// Keys
        /// </summary>
        public virtual ICollection<TKey> Keys => value.Select(p => p.Key).ToArray();

        /// <summary>
        /// Values
        /// </summary>
        public virtual ICollection<TValue> Values => value.Select(p => p.Value).ToArray();
       
        /// <summary>
        /// Element at index
        /// </summary>
        /// <param name="index"></param>
        public virtual KeyValuePair<TKey, TValue> this[int index]
        {
            get
            {
                T pair = value[index];
                return new KeyValuePair<TKey, TValue>(pair.Key, pair.Value); 
                
            }
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
                int index = IndexOf(value, key);
                return value[index].Value;
            }
            set
            {
                int index = IndexOf(this.value, key);
                //if that is not contained... add a new element as in real c# dictionaries 
                if (!ArrayUtils.IsValidIndex(index, this.value))
                {
                    try
                    {
                        this.value.Add((T)new KeyValuePair<TKey, TValue>(key, value));
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"{TActivatorFailedMessage}. Exception {e.Message}.");
                    }
                }
                else
                {
                    this.value[index].SetValue(value);
                }
            }
        }

        /// <summary>
        /// Count of elements in this collection
        /// </summary>
        public virtual int Count => this.value?.Count ?? 0;

        /// <summary>
        /// Clear
        /// </summary>
        public override void Clear()
        {
            value.Clear();
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
        /// Access the collection of key value pairs
        /// </summary>
        /// <param name="access"></param>
        public virtual void Access(AccessDelegate<List<T>> access)
        {
            access.Invoke(ref value);
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
        
        public virtual bool Check(PredicateAccessDelegate<List<T>> access)
        {
            return access.Invoke(ref value);
        }

        /// <summary>
        /// Set a new collection
        /// </summary>
        /// <param name="newValue"></param>
        public virtual void Set(ICollection<KeyValuePair<TKey, TValue>> newValue)
        {
            ReadCollection(newValue);
        }
        
        public virtual void Set(List<T> newValue)
        {
            Clear();
            AddRange(newValue);
        }
        
        /// <summary>
        /// WARNING: you cannot add an element with same key.
        /// </summary>
        /// <param name="element"></param>
        public void Add(T element)
        {
            if (!TryAdd(element))
            {
                Debug.LogWarning(DictionariesKeyAlreadyPresentWarning);
            }
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
        public void AddRange(IEnumerable<T> enumerable)
        {
            T[] array = enumerable.ToArray();
            foreach (var t in array)
            {
                TryAdd(t);
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
            if (Contains(value, tmpKey))
            {
                return false;
            }

            try
            {
                this.value.Add((T)new KeyValuePair<TKey, TValue>(tmpKey, element.Value));
            }
            catch (Exception e)
            {
                Debug.LogError($"{TActivatorFailedMessage}. Exception {e.Message}.");
            }
            
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
                value.RemoveAt(index);
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
                value.RemoveAt(index);
            }
        }

        public virtual KeyValuePair<TKey, TValue>[] ToArray()
        {
            return value.Select(t => (KeyValuePair<TKey, TValue>)t).ToArray();
        }

        public override bool IsNullOrEmpty()
        {
            return ArrayUtils.IsNullOrEmpty(value);
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
            return ToDictionary(value);
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
            if (ArrayUtils.HasDuplicates(value.Select(t => t.Key)))
            {
                messages = DictionariesDuplicatesWarning;
            }
            else
            {
                messages = string.Empty;
            }
        }
#endif
        public virtual void OnAfterDeserialize()
        {
            
        }

        #endregion

        
        #region Operators

//        public static implicit operator ListContainer<T>(SerializedDictionary<T, Key, Value> serializedDictionary)
//        {
//            return serializedDictionary.list;
//        }

        #endregion


        /// <summary>
        /// Contains the key element?
        /// </summary>
        /// <param name="list"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        protected static bool Contains(List<T> list, TKey element)
        {
            return IndexOf(list, element) >= 0;
        }
        
        /// <summary>
        /// Index of key element
        /// </summary>
        /// <param name="list"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        protected static int IndexOf(List<T> list, TKey element)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T pair = list[i];
                if ((object)pair.Key == (object)element)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Warning:
        /// - keys and values MUST have the same length
        /// - keys MUST NOT have duplicates
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        protected static Dictionary<TKey, TValue> ToDictionary(List<T> list)
        {
            Dictionary<TKey, TValue> newDictionary = new Dictionary<TKey, TValue>();
            foreach (var element in list)
            {
                newDictionary[element.Key] = element.Value;
            }
            return newDictionary;
        }

     
    }
}
