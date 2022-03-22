using System;
using System.Collections.Generic;

namespace CippSharp.Core.Containers
{
    public interface ICollectionContainer<K, T> : IContainer<K>, IEnumerable<T> 
        where K : ICollection<T>
    {
        /// <summary>
        /// The collection
        /// </summary>
        ICollection<T> Collection { get; }
        
        /// <summary>
        /// Count of elements in this collection
        /// </summary>
        int Count { get; }
        
        /// <summary>
        /// Element at index
        /// </summary>
        /// <param name="index"></param>
        T this[int index] { get; set; }

        /// <summary>
        /// Clears all elements and resize the collection to size 0
        /// </summary>
        void Clear();

        /// <summary>
        /// Add an element to the collection
        /// </summary>
        /// <param name="element"></param>
        void Add(T element);

        /// <summary>
        /// Add a collection of elements to the collection
        /// </summary>
        /// <param name="enumerable"></param>
        void AddRange(IEnumerable<T> enumerable);

        /// <summary>
        /// Contains the element?
        /// </summary>
        /// <param name="element"></param>
        bool Contains(T element);

        /// <summary>
        /// Retrieve index of element
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        int IndexOf(T element);
        
        /// <summary>
        /// Search for an element to match predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="result"></param>
        /// <returns>success</returns>
        bool Find(Predicate<T> predicate, out T result);

        /// <summary>
        /// Remove an element to the collection
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        bool Remove(T element);

        /// <summary>
        /// Remove an element at index
        /// </summary>
        /// <param name="index"></param>
        void RemoveAt(int index);
        
        /// <summary>
        /// Retrieve the array of this collection
        /// </summary>
        /// <returns></returns>
        T[] ToArray();
    }
}
