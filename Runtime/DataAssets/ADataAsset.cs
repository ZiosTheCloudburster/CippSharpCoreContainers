using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Purpose: consider this like an abstract class for data asset containers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ADataAsset<T> : ADataAssetBase, IContainer<T>
    {
//        /// <summary>
//        /// To access stored data.
//        /// </summary>
//        /// <param name="data"></param>
//        public delegate void AccessDelegate(ref T data);
//        public delegate bool PredicateAccessDelegate(ref T data);
//        
        /// <summary>
        /// The stored data
        /// </summary>
        [FormerlySerializedAs("m_data")]
        [FormerlySerializedAs("data")]
        [FormerlySerializedAs("list")]
        [FormerlySerializedAs("array")]
        [FormerlySerializedAs("value")]
        [SerializeField] protected T m_data = default(T);

        #region IContainerBase and IContainer Implementation
        
        /// <summary>
        /// The type of the stored value
        /// </summary>
        public override Type ContainerType => typeof(T);

        /// <summary>
        /// Retrieve the contained element
        /// </summary>
        /// <returns></returns>
        public override object GetValueRaw()
        {
            return value;
        }

        /// <summary>
        /// Retrieve the contained element
        /// </summary>
        public virtual T GetValue ()
        {
            return value;
        }

        /// <summary>
        /// Read/Write on data/value
        /// </summary>
        /// <param name="access"></param>
        public override void Access(GenericAccessDelegate access)
        {
            object o = value;
            access.Invoke(ref o);
            value = (T)o;
        }
        
        /// <summary>
        /// Custom edit of data
        /// </summary>
        /// <param name="access"></param>
        public virtual void Access(AccessDelegate<T> access)
        {
            access.Invoke(ref value);
        }
        
        /// <summary>
        /// Predicate on data/value
        /// </summary>
        /// <param name="access"></param>
        public override bool Check(PredicateGenericAccessDelegate access)
        {
            object o = value;
            bool b = access.Invoke(ref o);
            value = (T)o;
            return b;
        }
        
        /// <summary>
        /// Predicate on data/value
        /// </summary>
        /// <param name="access"></param>
        public virtual bool Check(PredicateAccessDelegate<T> access)
        {
            return access.Invoke(ref value);
        }
        
        /// <summary>
        /// Set the contained element
        /// </summary>
        /// <param name="newValue"></param>
        public override void Set(object newValue)
        {
            if (newValue.To<T>(out T tmp))
            {
                value = tmp;
            }
        }
        
        /// <summary>
        /// Set the contained element
        /// </summary>
        /// <param name="newData"></param>
        public virtual void Set(T newData)
        {
            value = newData;
        }

        #endregion
        
        
//        /// <summary>
//        /// Get Data
//        /// </summary>
//        public T Get ()
//        {
//            return m_data;
//        }
//
//        /// <summary>
//        /// Set Data
//        /// </summary>
//        /// <param name="newData"></param>
//        public void Set(T newData)
//        {
//            m_data = newData;
//        }
//
//        /// <summary>
//        /// Custom edit of data
//        /// </summary>
//        /// <param name="access"></param>
//        public void Access(AccessDelegate access)
//        {
//            access.Invoke(ref m_data);
//        }
//
//        /// <summary>
//        /// Access to verify a predicate on data
//        /// </summary>
//        /// <param name="access">mustn't be null</param>
//        /// <returns></returns>
//        public bool Check(PredicateAccessDelegate access)
//        {
//            return access.Invoke(ref m_data);
//        }
//        
//        /// <summary>
//        /// Custom edit of data
//        /// </summary>
//        /// <param name="edit"></param>
//        public void Edit(AccessDelegate edit)
//        {
//            Access(edit);
//        }
//        
//        /// <summary>
//        /// 'Cast' to Data
//        /// </summary>
//        /// <returns></returns>
//        public K To<K>()
//        {
//            return CastUtils.To<K>(m_data);
//        }
        
        /// <summary>
        /// Custom edit of data
        /// </summary>
        /// <param name="edit"></param>
        public void Edit(AccessDelegate<T> edit)
        {
            Access(edit);   
        }

        #region Cast Utils

        /// <summary>
        /// Cast data to K
        /// </summary>
        /// <returns></returns>
        public virtual K To<K>()
        {
            if (CastUtils.To<K>(value, out K tmp))
            {
                return tmp;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        /// <summary>
        /// Cast data to K. If it fails retrieve default value
        /// </summary>
        /// <returns></returns>
        public virtual K ToOrDefault<K>()
        {
            return CastUtils.To<K>(value);
        }
        
        #endregion

        #region Operators
        
        public static implicit operator T (ADataAsset<T> dataContainer)
        {
            return dataContainer.m_data;
        }
        
        #endregion
    }
}
