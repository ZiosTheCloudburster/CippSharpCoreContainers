using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Consider this like an abstract class for PagedArrayContainers
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class PagedArrayContainer<K, T> : APagedArrayContainerBase, IContainer<K>
        where K : ICollection<T>
    {
        /// <summary>
        /// The stored data/value
        /// </summary>
        [FormerlySerializedAs("m_data")]
        [FormerlySerializedAs("data")]
        [FormerlySerializedAs("m_list")]
        [FormerlySerializedAs("list")]
        [FormerlySerializedAs("array")]
        [FormerlySerializedAs("value")]
        [SerializeField, HideInInspector] protected K value = default(K);
        public virtual K Value => value;
        
        #region Custom Editor
#if UNITY_EDITOR
        [Space(5)]
        [SerializeField] public int elementsPerPage = 32;
        
        [SerializeField] private T[] inspectedElements = new T[0];
        
        #region Page Index
        
        [SerializeField, HideInInspector] private int pageIndex = 0;
  
        /// <summary>
        /// Set Page index
        /// </summary>
        /// <param name="newPageIndex"></param>
        public void SetPageIndex(int newPageIndex)
        {
            if (pageIndex == newPageIndex)
            {
                return;
            }

            pageIndex = newPageIndex;
                
            int index = pageIndex * elementsPerPage;
            int length = value.Count;
            if (index + elementsPerPage < length)
            {
                inspectedElements = ArrayUtils.SubArrayOrDefault(value, index, elementsPerPage);
            }
            else
            {
                inspectedElements = ArrayUtils.SubArrayOrDefault(value, index, Mathf.Clamp(length - index, 0, elementsPerPage));
            }
            
            OnSetPageIndex(newPageIndex, inspectedElements);
        }
        
        /// <summary>
        /// Children classes can do things here.
        /// Its not called or compiled outside editor!
        /// </summary>
        /// <param name="newPageIndex"></param>
        /// <param name="elements"></param>
        protected virtual void OnSetPageIndex(int newPageIndex, T[] elements)
        {
            
        }
        
        #endregion
#endif
        #endregion

        public PagedArrayContainer(K value = default)
        {
            this.value = value;
        }
        
        #region IContainerBase and IContainer Implementation
        
        /// <summary>
        /// The type of the stored value
        /// </summary>
        public override Type ContainerType => typeof(K);

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
        public virtual K GetValue ()
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
            value = (K)o;
        }
        
        /// <summary>
        /// Custom edit of data
        /// </summary>
        /// <param name="access"></param>
        public virtual void Access(AccessDelegate<K> access)
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
            value = (K)o;
            return b;
        }
        
        /// <summary>
        /// Predicate on data/value
        /// </summary>
        /// <param name="access"></param>
        public virtual bool Check(PredicateAccessDelegate<K> access)
        {
            return access.Invoke(ref value);
        }
        
        /// <summary>
        /// Set the contained element
        /// </summary>
        /// <param name="newValue"></param>
        public override void Set(object newValue)
        {
            if (newValue.To<K>(out K tmp))
            {
                value = tmp;
            }
        }
        
        /// <summary>
        /// Set the contained element
        /// </summary>
        /// <param name="newData"></param>
        public virtual void Set(K newData)
        {
            value = newData;
        }

        #endregion
        
        #region Cast Utils

        /// <summary>
        /// Cast data to K1
        /// </summary>
        /// <returns></returns>
        public virtual K1 To<K1>()
        {
            if (CastUtils.To<K1>(value, out K1 tmp))
            {
                return tmp;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        /// <summary>
        /// Cast data to K1. If it fails retrieve default value
        /// </summary>
        /// <returns></returns>
        public virtual K1 ToOrDefault<K1>()
        {
            return CastUtils.To<K1>(value);
        }
        
        #endregion
        
        #region Operators
        
        public static implicit operator K(PagedArrayContainer<K, T> container)
        {
            return container.Value;
        }

        public static implicit operator PagedArrayContainer<K, T>(K value)
        {
            return new PagedArrayContainer<K, T>(value);
        }
        
        #endregion
        
    }
}
