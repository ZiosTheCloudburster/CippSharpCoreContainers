
using System;
using System.Collections.Generic;
using System.Linq;

namespace CippSharp.Core.Containers
{
    internal static class ArrayUtils
    {
        #region Find
        
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
        
        #endregion

//        #region Index Of
//
//        /// <summary>
//        /// Index of element in target collection.
//        /// Retrieve -1 if the element is not present.
//        /// </summary>
//        /// <param name="array"></param>
//        /// <param name="element"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static int IndexOf<T>(T[] array, T element)
//        {
//            for (int i = 0; i < array.Length; i++)
//            {
//                var storedElement = array[i];
//                if ((object)storedElement == (object)element)
//                {
//                    return i;
//                }
//            }
//
//            return -1;
//        }
//
//        #endregion
        
        #region Is Null or Empty
        
//        /// <summary>
//        /// Returns true if the given array is null or empty
//        /// </summary>
//        /// <param name="array"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static bool IsNullOrEmpty<T>(T[] array)
//        {
//            return array == null || array.Length < 1;
//        }
//        
//        /// <summary>
//        /// Returns true if the given list is null or empty
//        /// </summary>
//        /// <param name="list"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static bool IsNullOrEmpty<T>(List<T> list)
//        {
//            return list == null || list.Count < 1;
//        }

        /// <summary>
        /// Returns true if the given dictionary is null or empty
        /// </summary>
        /// <param name="dictionary"></param>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<K, V>(Dictionary<K, V> dictionary)
        {
            return dictionary == null || dictionary.Count < 1;
        }

        /// <summary>
        /// Returns true if the given collection is null or empty
        /// </summary>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(ICollection<T> collection)
        {
            return collection == null || collection.Count < 1;
        }
        
//        /// <summary>
//        /// Returns true if the given enumerable is null or empty
//        /// </summary>
//        /// <param name="enumerable"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static bool IsNullOrEmpty<T>(IEnumerable<T> enumerable)
//        {
//            return enumerable == null || !enumerable.Any();
//        }

        #endregion
        
//        #region Is Valid Index
//
//        /// <summary>
//        /// Returns true if the given index is the array range.
//        /// </summary>
//        /// <param name="index"></param>
//        /// <param name="array"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static bool IsValidIndex<T>(int index, T[] array)
//        {
//            return index >= 0 && index < array.Length;
//        }
//        
//        /// <summary>
//        /// Returns true if the given index is in the list range.
//        /// </summary>
//        /// <param name="index"></param>
//        /// <param name="list"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static bool IsValidIndex<T>(int index, List<T> list)
//        {
//            return index >= 0 && index < list.Count;
//        }
//
//        /// <summary>
//        /// Returns true if the given index is in the list range.
//        /// </summary>
//        /// <param name="index"></param>
//        /// <param name="enumerable"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static bool IsValidIndex<T>(int index, IEnumerable<T> enumerable)
//        {
//            return index >= 0 && index < enumerable.Count();
//        }
//        
//        #endregion
        
//        #region Random Element
//
//        /// <summary>
//        /// Retrieve a random element in array.
//        /// </summary>
//        /// <param name="array"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static T RandomElement<T>(T[] array)
//        {
//            int index = UnityEngine.Random.Range(0, array.Length);
//            return array[index];
//        }
//        
//        /// <summary>
//        /// Retrieve a random element in list.
//        /// </summary>
//        /// <param name="list"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static T RandomElement<T>(List<T> list)
//        {
//            int index = UnityEngine.Random.Range(0, list.Count);
//            return list[index];
//        }
//
//        #endregion

        #region Remove

        /// <summary>
        /// Remove element at index
        /// </summary>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void RemoveAt<T>(ref T[] source, int index)
        {
            source = source.Where((e, i) => i != index).ToArray();
        }

        #endregion
    }
}
