//#if UNITY_EDITOR
//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Serialization;
//
//namespace CippSharp.Core.Containers.Samples
//{
//    [Serializable]
//    internal struct StructPairTemplate : IContainer<object>, IContainer<object[]>, IContainer<KeyValuePair<string, string>>, IContainerPair<string, string>, ISimplePair<string, string>, ISerializationCallbackReceiver
//    {
//        [FormerlySerializedAs("m_data")]
//        [FormerlySerializedAs("data")]
//        [FormerlySerializedAs("m_list")]
//        [FormerlySerializedAs("list")]
//        [FormerlySerializedAs("array")]
//        [FormerlySerializedAs("corners")]
//        [FormerlySerializedAs("value")]
//        [FormerlySerializedAs("m_name")]
//        [FormerlySerializedAs("name")]
//        [FormerlySerializedAs("x")]
//        [SerializeField] private string key;
//        public string Key => key;
//        [FormerlySerializedAs("m_value")]
//        [FormerlySerializedAs("color")]
//        [FormerlySerializedAs("y")]
//        [SerializeField] private string value;
//        public string Value => value;
//
//        #region Items
//        
//        /// <summary>
//        /// Items array
//        /// </summary>
//        private object[] items;
//
//        /// <summary>
//        /// Items array property.
//        /// 
//        /// Usage: get is 'readonly'.
//        ///
//        /// Example:
//        /// { 
//        ///    object[] pairContainerItems = myPairContainer.Items;
//        ///     //DoStuffs;
//        ///    myPairContainer.Items = pairContainerItems;
//        /// } 
//        /// </summary>
//        private object[] Items
//        {
//            get
//            {
//                int two = Count;
//                if (items != null && items.Length == two)
//                {
//                    items[0] = key;
//                    items[1] = value;
//                    return items;
//                }
//                
//                if (items == null)
//                {
//                    items = new object[two];
//                }
//                else if (items.Length != two)
//                {
//                    Array.Resize(ref items, two);
//                }
//                
//                items[0] = key;
//                items[1] = value;
//                return items;
//            }
//            set
//            {
//                this.key = CastUtils.ToOrDefault<string>(value[0]);
//                this.value = CastUtils.ToOrDefault<string>(value[1]);
//            }
//        }
//        
//        #endregion
//
//        /// <summary>
//        /// The fixed array length;
//        /// </summary>
//        public int Count => 2;
//
//        public StructPairTemplate(string key, string value) : this()
//        {
//            this.key = key;
//            this.value = value;
//            this.items = new object[2];
//        }
//
//        #region IContainerBase and IContainer Implementation
//        
//        /// <summary>
//        /// The type of the stored value
//        /// </summary>
//        public Type ContainerType => typeof(StructPairTemplate);
//
//        /// <summary>
//        /// Retrieve all the contained element
//        /// </summary>
//        /// <returns></returns>
//        public object GetValueRaw()
//        {
//            return Items;
//        }
//
//        /// <summary>
//        /// Retrieve first of the two elements
//        /// </summary>
//        /// <returns></returns>
//        public string GetKey()
//        {
//            return key;
//        }
//        
//        /// <summary>
//        /// Retrieve second of the two elements
//        /// </summary>
//        /// <returns></returns>
//        public string GetValue()
//        {
//            return value;
//        }
//
//        //Get items
//        object IContainer<object>.GetValue()
//        {
//            return Items;
//        }
//        
//        /// <inheritdoc />
//        /// <summary>
//        /// Retrieve all the contained element
//        /// </summary>
//        /// <returns></returns>
//        object[] IContainer<object[]>.GetValue()
//        {
//            return Items;
//        }
//        
//        /// <summary>
//        /// return key value pair
//        /// </summary>
//        /// <returns></returns>
//        KeyValuePair<string, string> IContainer<KeyValuePair<string, string>>.GetValue()
//        {
//            return this;
//        }
//        
//        /// <summary>
//        /// Read/Write on data/value
//        /// </summary>
//        /// <param name="access"></param>
//        public void Access(GenericAccessDelegate access)
//        {
//            object o = Items;
//            access.Invoke(ref o);
//            Items = (object[])o;
//        }
//        
//        /// <summary>
//        /// Read/Write on data/value
//        /// </summary>
//        /// <param name="access"></param>
//        public void Access(AccessDelegate<object> access)
//        {
//            object o = Items;
//            access.Invoke(ref o);
//            Items = (object[])o;
//        }
//        
//        /// <summary>
//        /// Read/Write on data/value
//        /// </summary>
//        /// <param name="access"></param>
//        public void Access(AccessDelegate<object[]> access)
//        {
//            object[] o = Items;
//            access.Invoke(ref o);
//            Items = o;
//        }
//        
//        /// <summary>
//        /// Custom edit of data
//        /// </summary>
//        /// <param name="access"></param>
//        public void Access(AccessDelegate<string, string> access)
//        {
//            access.Invoke(ref key, ref value);
//        }
//        
//        /// <summary>
//        /// Access key value pair
//        /// </summary>
//        /// <param name="access"></param>
//        public void Access(AccessDelegate<KeyValuePair<string, string>> access)
//        {
//            KeyValuePair<string, string> pair = this;
//            access.Invoke(ref pair);
//            Set(pair);
//        }
//        
//        /// <summary>
//        /// Predicate on data/value
//        /// </summary>
//        /// <param name="access"></param>
//        public bool Check(PredicateGenericAccessDelegate access)
//        {
//            object o = Items;
//            bool check = access.Invoke(ref o);
//            Items = (object[])o;
//            return check;
//        }
//        
//        /// <summary>
//        /// Predicate on data/value
//        /// </summary>
//        /// <param name="access"></param>
//        public bool Check(PredicateAccessDelegate<object> access)
//        {
//            object o = Items;
//            bool check = access.Invoke(ref o);
//            Items = (object[])o;
//            return check;
//        }
//        
//        /// <summary>
//        /// Predicate on data/value
//        /// </summary>
//        /// <param name="access"></param>
//        public bool Check(PredicateAccessDelegate<object[]> access)
//        {
//            object[] o = Items;
//            bool check = access.Invoke(ref o);
//            Items = o;
//            return check;
//        }
//        
//        /// <summary>
//        /// Predicate on data/value
//        /// </summary>
//        /// <param name="access"></param>
//        public bool Check(PredicateAccessDelegate<string, string> access)
//        {
//            return access.Invoke(ref key, ref value);
//        }
//        
//        public bool Check(PredicateAccessDelegate<KeyValuePair<string, string>> access)
//        {
//            KeyValuePair<string, string> pair = this;
//            bool check = access.Invoke(ref pair);
//            Set(pair);
//            return check;
//        }
//       
//        public void Set(object newValue)
//        {
//            if (CastUtils.To(newValue, out object[] newItems))
//            {
//                Items = newItems;
//            }
//        }
//        
//        public void Set(object[] newValue)
//        {
//            Items = newValue;
//        }
//        
//        public void Set(string newKey, string newValue)
//        {
//            this.key = newKey;
//            this.value = newValue;
//        }
//
//        public void SetKey(string newKey)
//        {
//            this.key = newKey;
//        }
//
//        public void SetValue(string newValue)
//        {
//            this.value = newValue;
//        }
//        
//        public void Set(KeyValuePair<string, string> newValue)
//        {
//            key = newValue.Key;
//            value = newValue.Value;
//        }
//        
//        #endregion
//        
//        public KeyValuePair<string, string> ToKeyValuePair()
//        {
//            return new KeyValuePair<string, string>(Key, Value);
//        }
//
//        #region ISerializationCallbackReceiver Implementation
//        
//        public void OnBeforeSerialize()
//        {
//            
//        }
//
//        public void OnAfterDeserialize()
//        {
//            items = new object[Count];
//        }
//        
//        #endregion
//        
//        #region Operators
//       
//        public static implicit operator KeyValuePair<string, string>(StructPairTemplate pair)
//        {
//            return new KeyValuePair<string, string>(pair.Key, pair.Value);
//        }
//        
//        public static implicit operator StructPairTemplate(KeyValuePair<string, string> pair)
//        {
//            return new StructPairTemplate(pair.Key, pair.Value);
//        }
//        
//        #endregion
//    }
//}
//#endif