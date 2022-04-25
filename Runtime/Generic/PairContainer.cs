using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Consider this like an abstract class
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    [Serializable]
    public class PairContainer<TKey, TValue> : APairContainerBase, IContainer<object>, IContainer<object[]>, IContainer<KeyValuePair<TKey, TValue>>, IContainerPair<TKey, TValue>, ISimplePair<TKey, TValue>
    {
        [FormerlySerializedAs("m_data")]
        [FormerlySerializedAs("data")]
        [FormerlySerializedAs("m_list")]
        [FormerlySerializedAs("list")]
        [FormerlySerializedAs("array")]
        [FormerlySerializedAs("corners")]
        [FormerlySerializedAs("value")]
        [FormerlySerializedAs("m_name")]
        [FormerlySerializedAs("name")]
        [SerializeField] protected TKey key;
        public TKey Key => key;
        [FormerlySerializedAs("m_value")]
        [FormerlySerializedAs("color")]
        [SerializeField] protected TValue value;
        public TValue Value => value;

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
                items[0] = key;
                items[1] = value;
                return items;
            }
            set
            {
                this.key = CastUtils.ToOrDefault<TKey>(value[0]);
                this.value = CastUtils.ToOrDefault<TValue>(value[1]);
            }
        }
        
        #endregion

        /// <summary>
        /// The fixed array length;
        /// </summary>
        public virtual int Count => 2;

        public PairContainer()
        {
            this.key = default(TKey);
            this.value = default(TValue);
        }

        public PairContainer(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        #region IContainerBase and IContainer Implementation
        
        /// <summary>
        /// The type of the stored value
        /// </summary>
        public override Type ContainerType => typeof(PairContainer<TKey, TValue>);

        /// <summary>
        /// Retrieve all the contained element
        /// </summary>
        /// <returns></returns>
        public override object GetValueRaw()
        {
            return Items;
        }

        /// <summary>
        /// Retrieve first of the two elements
        /// </summary>
        /// <returns></returns>
        public virtual TKey GetKey()
        {
            return key;
        }
        
        /// <summary>
        /// Retrieve second of the two elements
        /// </summary>
        /// <returns></returns>
        public virtual TValue GetValue()
        {
            return value;
        }

        //Get items
        object IContainer<object>.GetValue()
        {
            return Items;
        }
        
        /// <inheritdoc />
        /// <summary>
        /// Retrieve all the contained element
        /// </summary>
        /// <returns></returns>
        object[] IContainer<object[]>.GetValue()
        {
            return Items;
        }
        
        /// <summary>
        /// return key value pair
        /// </summary>
        /// <returns></returns>
        KeyValuePair<TKey, TValue> IContainer<KeyValuePair<TKey, TValue>>.GetValue()
        {
            return this;
        }
        
        /// <summary>
        /// Read/Write on data/value
        /// </summary>
        /// <param name="access"></param>
        public override void Access(GenericAccessDelegate access)
        {
            object o = Items;
            access.Invoke(ref o);
            Items = (object[])o;
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
        public virtual void Access(AccessDelegate<object[]> access)
        {
            object[] o = Items;
            access.Invoke(ref o);
            Items = o;
        }
        
        /// <summary>
        /// Custom edit of data
        /// </summary>
        /// <param name="access"></param>
        public virtual void Access(AccessDelegate<TKey, TValue> access)
        {
            access.Invoke(ref key, ref value);
        }
        
        /// <summary>
        /// Access key value pair
        /// </summary>
        /// <param name="access"></param>
        public void Access(AccessDelegate<KeyValuePair<TKey, TValue>> access)
        {
            KeyValuePair<TKey, TValue> pair = this;
            access.Invoke(ref pair);
            Set(pair);
        }
        
        /// <summary>
        /// Predicate on data/value
        /// </summary>
        /// <param name="access"></param>
        public override bool Check(PredicateGenericAccessDelegate access)
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
        public virtual bool Check(PredicateAccessDelegate<TKey, TValue> access)
        {
            return access.Invoke(ref key, ref value);
        }
        
        public bool Check(PredicateAccessDelegate<KeyValuePair<TKey, TValue>> access)
        {
            KeyValuePair<TKey, TValue> pair = this;
            bool check = access.Invoke(ref pair);
            Set(pair);
            return check;
        }
       
        public override void Set(object newValue)
        {
            if (CastUtils.To(newValue, out object[] newItems))
            {
                Items = newItems;
            }
        }
        
        public virtual void Set(object[] newValue)
        {
            Items = newValue;
        }
        
        public virtual void Set(TKey newKey, TValue newValue)
        {
            this.key = newKey;
            this.value = newValue;
        }

        public virtual void SetKey(TKey newKey)
        {
            this.key = newKey;
        }

        public virtual void SetValue(TValue newValue)
        {
            this.value = newValue;
        }
        
        public void Set(KeyValuePair<TKey, TValue> newValue)
        {
            key = newValue.Key;
            value = newValue.Value;
        }
        
        #endregion
        
        public virtual KeyValuePair<TKey, TValue> ToKeyValuePair()
        {
            return this;
        }

        #region Operators
       
        public static implicit operator KeyValuePair<TKey, TValue>(PairContainer<TKey, TValue> pair)
        {
            return new KeyValuePair<TKey, TValue>(pair.Key, pair.Value);
        }
        
        public static implicit operator PairContainer<TKey, TValue>(KeyValuePair<TKey, TValue> pair)
        {
            return new PairContainer<TKey, TValue>(pair.Key, pair.Value);
        }
        
        #endregion

       
    }
}
