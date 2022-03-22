using System;
using System.Collections.Generic;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Consider this like an abstract class 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class ListContainer<T> : AListContainer<T>
    {
//        [SerializeField] public List<T> list = new List<T>();
//
//        public T this[int index]
//        {
//            get { return list[index]; }
//            set { list[index] = value; }
//        }
        
//        public override Type ContainerType => typeof(List<T>);
        
        public ListContainer()
        {
            this.value = new List<T>();
//            this.list = new List<T>();
        }

        public ListContainer(T element)
        {
            this.value = new List<T> {element};
        }
        
        public ListContainer(IEnumerable<T> enumerable)
        {
            this.value = new List<T>();
            this.value.AddRange(enumerable);
//            this.list = new List<T>();
//            this.value = new List<T>();
        }

//        public int Count
//        {
//            get { return list?.Count ?? 0; }
//        }
//
//        public void Clear()
//        {
//            if (list == null)
//            {
//                list = new List<T>();
//            }
//            
//            list.Clear();
//        }
//
//        public void Add(T element)
//        {
//            list.Add(element);
//        }
//
//        public void AddRange(IEnumerable<T> enumerable)
//        {
//            list.AddRange(enumerable);
//        }
//
//        public T[] ToArray()
//        {
//            return list.ToArray();
//        }
        
        #region Operators

        public static implicit operator List<T>(ListContainer<T> container)
        {
            return container.list;
        }

        public static implicit operator ListContainer<T>(List<T> list)
        {
            return new ListContainer<T>(list);
        }

        #endregion

//        public IEnumerator<T> GetEnumerator()
//        {
//            return list.GetEnumerator();
//        }
//
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return list.GetEnumerator();
//        }
    }
}
