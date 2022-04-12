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
        public ListContainer()
        {
            this.value = new List<T>();
        }

        public ListContainer(T element)
        {
            this.value = new List<T> {element};
        }
        
        public ListContainer(IEnumerable<T> enumerable)
        {
            this.value = new List<T>();
            this.value.AddRange(enumerable);
        }
        
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
    }
}
