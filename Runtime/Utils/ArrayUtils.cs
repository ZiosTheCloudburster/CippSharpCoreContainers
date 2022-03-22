﻿
using System;
using System.Collections.Generic;
using System.Linq;
namespace CippSharp.Core.Containers
{
    using Array = System.Array;
    using Exception = System.Exception;
    using Debug = UnityEngine.Debug;
    
    internal static class ArrayUtils
    {
        #region Generic
        
        /// <summary>
        /// Retrieve if context object is an array.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool IsArray(object context)
        {
            return context.GetType().IsArray;
        }
        
         /// <summary>
        /// Try to get value
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="element"></param>
        /// <returns>success</returns>
        public static bool TryGetValue(object[] array, int index, out object element)
        {
            try
            {
                element = array[index];
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                element = null;
                return false;
            }
        }

        /// <summary>
        /// Try to set value
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="element"></param>
        /// <returns>success</returns>
        public static bool TrySetValue(object[] array, int index, object element)
        {
            try
            {
                array[index] = element;
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Try to cast an object to object array
        /// </summary>
        /// <param name="value"></param>
        /// <param name="array"></param>
        /// <returns>success</returns>
        public static bool TryCast(object value, out object[] array)
        {
            try
            {
                array = ((Array)value).Cast<object>().ToArray();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                array = null;
                return false;
            }
        }
        
        /// <summary>
        /// Try to cast a generic Array to object[]
        /// </summary>
        /// <param name="value"></param>
        /// <param name="array"></param>
        /// <returns>success</returns>
        public static bool TryCast(Array value, out object[] array)
        {
            try
            {
                array = (value).Cast<object>().ToArray();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                array = null;
                return false;
            }
        }
        

        #endregion
        
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
        
        #region Is Valid Index
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
        /// <summary>
        /// Returns true if the given index is in the list range.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="collection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsValidIndex<T>(int index, ICollection<T> collection)
        {
            return index >= 0 && index < collection.Count;
        }
        
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
        
        #endregion
        
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
//        #endcregion

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

        #region SubArray

        /// <summary>
        /// Same as substring, but for arrays.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] SubArrayOrDefault<T>(ICollection<T> collection, int index, int length)
        {
            return TrySubArray(collection.ToArray(), index, length, out T[] subArray) ? subArray : default;
        }
        
        /// <summary>
        /// Try to get a subArray from an array
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <param name="subArray"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TrySubArray<T>(T[] array, int index, int length, out T[] subArray)
        {
            try
            {
                T[] result = new T[length];
                Array.Copy(array, index, result, 0, length);
                subArray = result;
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to retrieve subArray. Exception: {e.Message}.");
                subArray = null;
                return false;
            }
        }


        #endregion
    }
}