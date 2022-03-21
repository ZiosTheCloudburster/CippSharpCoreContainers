//using System;
//using System.Collections;
//using System.Collections.Generic;
//using CippSharp.Core.Extensions;
//using UnityEngine;
//
//namespace CippSharp.Core
//{
//    [Serializable]
//    public class PagedListContainer<T> : PagedArrayContainerBase, IEnumerable
//    {
//        [SerializeField, HideInInspector] public List<T> list = new List<T>();
//
//        #region Custom Editor
//#if UNITY_EDITOR
//        [Space(5)]
//        [SerializeField] public int elementsPerPage = 32;
//        
//        [SerializeField] private T[] inspectedElements = new T[0];
//        
//        #region Page Index
//        [SerializeField, HideInInspector] private int pageIndex = 0;
//  
//        /// <summary>
//        /// Set Page index
//        /// </summary>
//        /// <param name="value"></param>
//        public void SetPageIndex(int value)
//        {
//            if (pageIndex == value)
//            {
//                return;
//            }
//
//            pageIndex = value;
//                
//            int index = pageIndex * elementsPerPage;
//            int length = Length;
//            if (index + elementsPerPage < length)
//            {
//                inspectedElements = list.ToArray().SubArray(index, elementsPerPage);
//            }
//            else
//            {
//                inspectedElements = list.ToArray().SubArray(index, Mathf.Clamp(length - index, 0, elementsPerPage));
//            }
//            
//            OnSetPageIndex(value, inspectedElements);
//        }
//        
//        /// <summary>
//        /// Children classes can do things here.
//        /// Its not called or compiled outside editor!
//        /// </summary>
//        /// <param name="value"></param>
//        /// <param name="elements"></param>
//        protected virtual void OnSetPageIndex(int value, T[] elements)
//        {
//            
//        }
//        
//        #endregion
//#endif
//        #endregion
//        
//        public T this[int index]
//        {
//            get { return list[index]; }
//            set { list[index] = value; }
//        }
//      
//        public PagedListContainer()
//        {
//            this.list = new List<T>();
//        }
//      
//        private PagedListContainer(T[] array) : this()
//        {
//            this.list.AddRange(array);
//        }
//
//        public T[] GetElements()
//        {
//            return list.ToArray();
//        }
//
//        public int Length
//        {
//            get { return list?.Count ?? 0; }
//        }
//        
//        #region Operators
//
//        public static implicit operator List<T>(PagedListContainer<T> container)
//        {
//            return container.list;
//        }
//      
//        public static implicit operator PagedListContainer<T>(T[] list)
//        {
//            return new PagedListContainer<T>(list);
//        }
//      
//        #endregion
//        
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return list.GetEnumerator();
//        }
//    }
//}
