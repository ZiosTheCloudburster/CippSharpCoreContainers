using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Consider this like an abstract class
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    [Serializable]
    public class PairContainer<T1, T2> : APairContainerBase, IContainer<object[]>, IContainerPair<T1, T2>, ISimplePair<T1, T2>
    {
        [FormerlySerializedAs("m_data")]
        [FormerlySerializedAs("data")]
        [FormerlySerializedAs("m_list")]
        [FormerlySerializedAs("list")]
        [FormerlySerializedAs("array")]
        [FormerlySerializedAs("corners")]
        [FormerlySerializedAs("value")]
        [SerializeField] protected T1 key;
        public T1 Key => key;
        [SerializeField] protected T2 value;
        public T2 Value => value;

        #region Items
        
        /// <summary>
        /// Items array
        /// </summary>
        protected readonly object[] items = new object[2];

        /// <summary>
        /// Items array property.
        /// </summary>
        public virtual object[] Items
        {
            get
            {
                items[0] = key;
                items[1] = value;
                return items;
            }
            set
            {
                this.items[0] = value[0];
                this.key = CastUtils.ToOrDefault<T1>(value[0]);
                this.items[1] = value[1];
                this.value = CastUtils.ToOrDefault<T2>(value[1]);
            }
        }
        
        #endregion

        public virtual int Count => 2;
        
        #region IContainerBase and IContainer Implementation
        
        /// <summary>
        /// The type of the stored value
        /// </summary>
        public override Type ContainerType
        {
            get => typeof(PairContainer<T1, T2>);
        }
        
        /// <summary>
        /// Retrieve the contained element
        /// </summary>
        /// <returns></returns>
        public override object GetValueRaw()
        {
            return Items;
        }
        
        /// <summary>
        /// Retrieve the contained element
        /// </summary>
        /// <returns></returns>
        public virtual object[] GetValue()
        {
            return Items;
        }
        
        /// <summary>
        /// Retrieve the contained element
        /// </summary>
        public T GetValue<T>(PairElement target) where T : T1, T2
        {
            switch (target)
            {
                case PairElement.Key:
                    return (T) key;
                case PairElement.Value:
                    return (T) value;
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), target, null);
            }
            
            return default(T);
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
        public void Access(AccessDelegate<object[]> access)
        {
            object[] o = Items;
            access.Invoke(ref o);
            Items = o;
        }

        /// <summary>
        /// Read/Write on data/value
        /// </summary>
        /// <param name="target"></param>
        /// <param name="access"></param>
        public void Access<T>(PairElement target, AccessDelegate<T> access) where T : T1, T2
        {
            switch (target)
            {
                case PairElement.Key:
                    T tmpKey = (T)key;
                    access.Invoke(ref tmpKey);
                    key = tmpKey;
                    break;
                case PairElement.Value:
                    T tmpValue = (T) value;
                    access.Invoke(ref tmpValue);
                    value = tmpValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), target, null);
            }
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
        /// <param name="target"></param>
        /// <param name="access"></param>
        public bool Check<T>(PairElement target, PredicateAccessDelegate<T> access) where T : T1, T2
        {
            bool check = false;
            switch (target)
            {
                case PairElement.Key:
                    T tmpKey = (T)key;
                    check = access.Invoke(ref tmpKey);
                    key = tmpKey;
                    break;
                case PairElement.Value:
                    T tmpValue = (T) value;
                    check = access.Invoke(ref tmpValue);
                    value = tmpValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(target), target, null);
            }
            
            return check;
        }

        public override void Set(object newValue)
        {
            
        }
        
        public void Set(object[] newValue)
        {
            throw new NotImplementedException();
        }
        
        public void Set<T>(PairElement target, T newValue) where T : T1, T2
        {
            
        }
        
        
        #endregion

        
        public KeyValuePair<T1, T2> ToKeyValuePair()
        {
            
        }

      

     

       

        

  

        

        
    }
}
