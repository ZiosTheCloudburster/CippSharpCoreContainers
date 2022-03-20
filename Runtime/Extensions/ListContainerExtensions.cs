using System.Collections.Generic;

namespace CippSharp.Core.Containers
{
    public static class ListContainerExtensions
    {
        /// <summary>
        /// Is this list and container null or empty?
        /// </summary>
        /// <param name="container"></param>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<K, T>(this ICollectionContainer<K, T> container) 
            where K : ICollection<T>
        {
            return !container.IsValid() || ArrayUtils.IsNullOrEmpty(container.Collection);
        }

        /// <summary>
        /// Retrieve a random element in container
        /// </summary>
        /// <param name="container"></param>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomElement<K, T>(this ICollectionContainer<K, T> container)
            where K : ICollection<T>
        {
            int index = UnityEngine.Random.Range(0, container.Count);
            return container[index];
        }
    }
}
