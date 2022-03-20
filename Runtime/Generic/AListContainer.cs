using System;
using System.Collections;
using System.Collections.Generic;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Abstract class for generic list containers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public abstract class AListContainer<T> : Container<List<T>>, ICollectionContainer<List<T>, T>
    {
        #region ICollectionContainer Implementation
        
        public override Type ContainerType => typeof(List<T>);
        
        /// <summary>
        /// Stored Collection
        /// </summary>
        public virtual ICollection<T> Collection => value;

        /// <summary>
        /// this has minus 'l' as old name reference
        /// </summary>
        public virtual List<T> list
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
        public virtual int Count => value?.Count ?? 0;

        public virtual void Clear()
        {
            if (value == null)
            {
                value = new List<T>();
            }
            
            value.Clear();
        }
        
        public virtual void Add(T element)
        {
            value.Add(element);
        }

        public virtual void AddRange(IEnumerable<T> enumerable)
        {
            value.AddRange(enumerable);
        }
        
        public virtual bool Contains(T element)
        {
            return value.Contains(element);
        }
        
        public virtual int IndexOf(T element)
        {
            return value.IndexOf(element);
        }

        public virtual bool Find(Predicate<T> predicate, out T result)
        {
            return ArrayUtils.Find(value, predicate, out result);
        }

        public virtual bool Remove(T element)
        {
            return value.Remove(element);
        }

        public virtual void RemoveAt(int index)
        {
            value.RemoveAt(index);
        }

        public virtual T[] ToArray()
        {
            return value.ToArray();
        }
        
        public virtual IEnumerator<T> GetEnumerator()
        {
            return value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return value.GetEnumerator();
        }
        
        #endregion
    }
}
