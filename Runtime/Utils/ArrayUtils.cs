
using System;
using System.Collections.Generic;

namespace CippSharp.Core.Containers
{
    internal static class ArrayUtils
    {
        /// <summary>
        /// Find Method
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <param name="result"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Find<T>(ICollection<T> collection, Predicate<T> predicate, out T result)
        {
            foreach (var element in collection)
            {
                if (!predicate.Invoke(element))
                {
                    continue;
                }
                
                result = element;
                return true;
            }
            
            result = default;
            return false;
        }
        
        #region Random Element

        /// <summary>
        /// Retrieve a random element in array.
        /// </summary>
        /// <param name="array"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomElement<T>(T[] array)
        {
            int index = UnityEngine.Random.Range(0, array.Length);
            return array[index];
        }
        
        /// <summary>
        /// Retrieve a random element in list.
        /// </summary>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomElement<T>(List<T> list)
        {
            int index = UnityEngine.Random.Range(0, list.Count);
            return list[index];
        }

        #endregion
    }
}
