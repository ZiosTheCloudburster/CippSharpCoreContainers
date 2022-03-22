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
    public class APagedArrayContainer<K, T> : APagedArrayContainerBase, IContainer<K>
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

        #region Paged Array Implementation
        
        [Space(5)]
        [SerializeField] public int elementsPerPage = 32;

        /// <summary>
        /// The index of the collection <see cref="value"/> from where to draw the elements
        /// </summary>
        [SerializeField, HideInInspector] private int pageIndex = 0;
//        [SerializeField, HideInInspector] private int pageIndex = 0;

        /// <summary>
        /// Retrieve the current page index
        /// </summary>
        public int CurrentPageIndex => pageIndex;
        
        /// <summary>
        /// Retrieve the Pages Count (based on the current elements per page)
        /// </summary>
        public int PagesCount
        {
            get
            {
                int length = value.Count;
                return Mathf.CeilToInt(length / (float)elementsPerPage);
            }
        }

        /// <summary>
        /// Current Elements per Page
        /// </summary>
        public int ElementsPerCurrentPage
        {
            get
            {
                int index = pageIndex * elementsPerPage;
                int length = value.Count;
                return index + elementsPerPage < length ? elementsPerPage : Mathf.Clamp(length - index, 0, elementsPerPage);
            }
        }
        
        #region Custom Editor
        
#if UNITY_EDITOR
        [SerializeField] private T[] inspectedElements = new T[0];
#endif
        
        #region Set Page Index
  
        /// <summary>
        /// Set Page index
        /// During Editor it works also as Hook for inspected elements,
        /// During Build it works only to change the pageIndex.
        /// </summary>
        /// <param name="newPageIndex"></param>
        public void SetPageIndex(int newPageIndex)
        {
            if (pageIndex == newPageIndex)
            {
                return;
            }
#if UNITY_EDITOR
            inspectedElements = GetInspectedElementsAt(newPageIndex);
#else
            pageIndex = newPageIndex;
#endif
#if UNITY_EDITOR            
            OnSetPageIndex(newPageIndex, inspectedElements);
#endif
        }
        
#if UNITY_EDITOR
        /// <summary>
        /// Children classes can do things here.
        /// Its not called or compiled outside editor!
        /// </summary>
        /// <param name="newPageIndex"></param>
        /// <param name="elements"></param>
        protected virtual void OnSetPageIndex(int newPageIndex, T[] elements)
        {
            
        }
#endif
        #endregion
        
        #endregion

        /// <summary>
        /// Retrieve the subArray of elements at current page. 
        /// </summary>
        /// <returns></returns>
        public T[] GetInspectedElements()
        {
            return GetInspectedElementsAt(pageIndex);
        }

        /// <summary>
        /// Retrieve the subArray of elements at target page. 
        /// </summary>
        /// <param name="newPageIndex"></param>
        /// <returns></returns>
        public T[] GetInspectedElementsAt(int newPageIndex)
        {
            if (!ArrayUtils.IsValidIndex(newPageIndex, value))
            {
                return null;
            }

            pageIndex = newPageIndex;
            int index = pageIndex * elementsPerPage;
            int length = value.Count;
            int clampedElementsPerPagePage = pageIndex + elementsPerPage < length ? elementsPerPage : Mathf.Clamp(length - index, 0, elementsPerPage);
            T[] array = ArrayUtils.SubArrayOrDefault(value, pageIndex, clampedElementsPerPagePage);
            
            return array;
        }

        #endregion
        
        public APagedArrayContainer(K value = default)
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
        
        public static implicit operator K(APagedArrayContainer<K, T> container)
        {
            return container.Value;
        }

        public static implicit operator APagedArrayContainer<K, T>(K value)
        {
            return new APagedArrayContainer<K, T>(value);
        }
        
        #endregion
    }
}
