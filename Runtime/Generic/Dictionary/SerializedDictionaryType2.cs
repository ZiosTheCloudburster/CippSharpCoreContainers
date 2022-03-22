using System;
using System.Collections;
using System.Collections.Generic;
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
    public class SerializedDictionary<TKey, TValue> : SerializedDictionaryBase, IContainer<object>, IContainer<object[]>, IContainerPair<List<TKey>, List<TValue>>, ISimplePair<List<TKey>, List<TValue>>, IDictionaryContainer<TKey, TValue>
    {
        [SerializeField] private List<TKey> keys = new List<TKey>();
        public List<TKey> Key => keys;
        [SerializeField] private List<TValue> values = new List<TValue>();
        public List<TValue> Value => values;
        
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
                this.items[0] = value[0];
                this.keys = CastUtils.ToOrDefault<List<TKey>>(value[0]);
                this.items[1] = value[1];
                this.values = CastUtils.ToOrDefault<List<TValue>>(value[1]);
            }
        }
        
        #endregion

        public SerializedDictionary()
        {
            
        }

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
        public List<TKey> GetKey()
        {
            return keys;
        }
        
        /// <summary>
        /// Retrieve second of the two elements
        /// </summary>
        /// <returns></returns>
        public List<TValue> GetValue()
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
        public void Access(AccessDelegate<object> access)
        {
            object o = Items;
            access.Invoke(ref o);
            Items = (object[])o;
        }
       
        /// <summary>
        /// Read/Write on data/value
        /// </summary>
        /// <param name="access"></param>
        public void Access(AccessDelegate<object[]> access)
        {
            object[] o = Items;
            access.Invoke(ref o);
            Items = o;
        }
        
        /// <summary>
        /// Read/Write on data/value
        /// </summary>
        /// <param name="access"></param>
        public void Access(AccessDelegate<List<TKey>, List<TValue>> access)
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
        public bool Check(PredicateAccessDelegate<object> access)
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
        public bool Check(PredicateAccessDelegate<object[]> access)
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
        public bool Check(PredicateAccessDelegate<List<TKey>, List<TValue>> access)
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
        
        public void Set(object[] newItems)
        {
            Items = newItems;
        }
        
        public void Set(List<TKey> newKeys, List<TValue> newValues)
        {
            this.keys = newKeys;
            this.values = newValues;
        }

        public void SetKey(List<TKey> newKeys)
        {
            this.keys = newKeys;
        }

        public void SetValue(List<TValue> newValues)
        {
            this.values = newValues;
        }

        #endregion

        #region ICollectionContainer and IDictionaryContainer Implementation

        /// <summary>
        /// Stored Collection
        /// </summary>
        public virtual ICollection<KeyValuePair<TKey, TValue>> Collection => ToDictionary();

        public virtual IDictionary<TKey, TValue> Dictionary => ToDictionary();
       
        /// <summary>
        /// Element at index
        /// </summary>
        /// <param name="index"></param>
        public virtual KeyValuePair<TKey, TValue> this[int index]
        {
            get => new KeyValuePair<TKey, TValue>(keys[index], values[index]);
            set
            {
                this.keys[index] = value.Key;
                this.values[index] = value.Value;
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

        public void Access(AccessDelegate<ICollection<KeyValuePair<TKey, TValue>>> access)
        {
            ICollection<KeyValuePair<TKey, TValue>> c = ToDictionary();
            access.Invoke(ref c);
            ReadCollection(c);
        }

        public bool Check(PredicateAccessDelegate<ICollection<KeyValuePair<TKey, TValue>>> access)
        {
            ICollection<KeyValuePair<TKey, TValue>> c = ToDictionary();
            bool check = access.Invoke(ref c);
            ReadCollection(c);
            return check;
        }

        public void Set(ICollection<KeyValuePair<TKey, TValue>> newValue)
        {
            ReadCollection(newValue);
        }
        
        public void Add(KeyValuePair<TKey, TValue> element)
        {
            keys.Add(element.Key);
            values.Add(element.Value);
        }

        public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> enumerable)
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<TKey, TValue> element)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(KeyValuePair<TKey, TValue> element)
        {
            throw new NotImplementedException();
        }

        public bool Find(Predicate<KeyValuePair<TKey, TValue>> predicate, out KeyValuePair<TKey, TValue> result)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<TKey, TValue> element)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public KeyValuePair<TKey, TValue>[] ToArray()
        {
            throw new NotImplementedException();
        }

        public override bool IsNullOrEmpty()
        {
            return ArrayUtils.IsNullOrEmpty(keys);
        }
        
        ICollection<KeyValuePair<TKey, TValue>> IContainer<ICollection<KeyValuePair<TKey, TValue>>>.GetValue()
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

       

        public TValue this[TKey key]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
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
            ArrayUtils.SplitCollection(collection, out keys, out values);
        }
        
        /// <summary>
        /// From dictionary to 'here.
        /// </summary>
        /// <param name="dictionary"></param>
        public virtual void ReadDictionary(Dictionary<TKey, TValue> dictionary)
        {
            ArrayUtils.SplitDictionary(dictionary, out keys, out values);
        }
      
        /// <summary>
        /// This as KeyValuePair
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<List<TKey>, List<TValue>> ToKeyValuePair()
        {
            return this;
        }

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

    #endregion
}
