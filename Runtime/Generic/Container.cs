﻿using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Consider this like an abstract class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Container<T> : AContainerBase, IContainer<T>
    {
        [FormerlySerializedAs("m_data")]
        [FormerlySerializedAs("data")]
        [FormerlySerializedAs("list")]
        [FormerlySerializedAs("array")]
        [SerializeField] protected T value = default(T);
        public T Value => value;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value"></param>
        public Container(T value = default)
        {
            this.value = value;
        }
        
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
        public T GetValue ()
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
        public void Access(AccessDelegate<T> access)
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
        public bool Check(PredicateAccessDelegate<T> access)
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
        public void Set(T newData)
        {
            value = newData;
        }

        #endregion
   
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
        public K To<K>()
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
        public K ToOrDefault<K>()
        {
            return CastUtils.To<K>(value);
        }
        
        #endregion

        #region Operators
        
        public static implicit operator T(Container<T> container)
        {
            return container.Value;
        }

        public static implicit operator Container<T>(T value)
        {
            return new Container<T>(value);
        }
        
        #endregion
    }
}
