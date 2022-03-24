using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Purpose: make an inspector array of thousands elements more viewable
    /// without affecting performances
    ///
    /// USAGE: it's intended just as 'viewer' not as editor of inspected elements
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public abstract class PagedArrayContainer<T> : APagedArrayContainer<T[], T>, ICollectionContainer<T[], T>
    {
        #region ICollectionContainer Implementation
        
        public override Type ContainerType => typeof(T[]);
        
        /// <summary>
        /// Stored Collection
        /// </summary>
        public virtual ICollection<T> Collection => value;

        /// <summary>
        /// this has minus 'a' as old name reference
        /// </summary>
        public virtual T[] array
        {
            get => this.value;
            set => this.value = value;
        }
        
        /// <summary>
        /// Element at index
        /// </summary>
        /// <param name="index"></param>
        public virtual T this[int index]
        {
            get => this.value[index];
            set => this.value[index]= value;
        }

        /// <summary>
        /// Count of elements in this collection
        /// </summary>
        public virtual int Count => value?.Length ?? 0;

        public virtual void Clear()
        {
            value = new T[0];
        }
        
        public virtual void Add(T element)
        {
            int length = Count;
            Array.Resize(ref value, length+1);
            value[length] = element;
        }

        public virtual void AddRange(IEnumerable<T> enumerable)
        {
            int length = Count;
            int extra = enumerable.Count();
            Array.Resize(ref value, length + extra);
            for (int i = length; i < length+extra; i++)
            {
                value[i] = enumerable.ElementAt(i-length);
            }
        }
        
        public virtual bool Contains(T element)
        {
            return value.Contains(element);
        }
        
        public virtual int IndexOf(T element)
        {
            return Array.IndexOf(value, element);
        }

        public virtual bool Find(Predicate<T> predicate, out T result)
        {
            return ArrayUtils.Find(value, predicate, out result);
        }

        public virtual bool Remove(T element)
        {
            value = Array.FindAll(value, (T o) => (object)o != (object)element).ToArray();
            return true;
        }

        public virtual void RemoveAt(int index)
        {
            ArrayUtils.RemoveAt(ref value, index);
        }

        public virtual T[] ToArray()
        {
            return value.ToArray();
        }
        
        public virtual IEnumerator<T> GetEnumerator()
        {
            return ((ICollection<T>)value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return value.GetEnumerator();
        }
        
        #endregion
    }
}
