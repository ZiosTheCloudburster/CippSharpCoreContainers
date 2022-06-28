using System;
using System.Collections.Generic;
using System.Linq;

namespace CippSharp.Core.Containers
{
    using Debug = UnityEngine.Debug;
    
    internal static class ArrayUtils
    {
        
        #region Conversions

        /// <summary>
        /// From list of Keys and Values to Dictionary
        /// 
        /// Warning:
        /// - keys and values MUST have the same length
        /// - keys MUST NOT have duplicates
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(List<TKey> keys, List<TValue> values)
        {
            Dictionary<TKey, TValue> newDictionary = new Dictionary<TKey, TValue>();
            
            for (int i = 0; i < keys.Count; i++)
            {
                newDictionary[keys[i]] = values[i];
            }

            return newDictionary;
        }

        /// <summary>
        /// From list of Keys and Values to Array of KeyValuePairs
        ///
        /// Warning: keys and values MUST have the same length
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <returns></returns>
        public static KeyValuePair<TKey, TValue>[] ToArray<TKey, TValue>(List<TKey> keys, List<TValue> values)
        {
            int length = keys.Count;
            KeyValuePair<TKey, TValue>[] newArray = new KeyValuePair<TKey, TValue>[length];
            for (int i = 0; i < length; i++)
            {
                newArray[i] = new KeyValuePair<TKey, TValue>(keys[i], values[i]);
            }

            return newArray;
        }
        
        #endregion
        
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
                Debug.LogError($"Failed to get element from array at index. Caught Exception: {e.Message}");
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
                Debug.LogError($"Failed to cast object to object[]. Caught Exception: {e.Message}");
                array = null;
                return false;
            }
        }
        
        #endregion
        
        
        #region → Add

        /// <summary>
        /// Add an element to a list only if it is new
        /// </summary>
        /// <param name="list"></param>
        /// <param name="element"></param>
        /// <typeparam name="T"></typeparam>
        public static void AddIfNew<T>(List<T> list, T element)
        {
            if (!list.Contains(element))
            {
                list.Add(element);
            }
        }

        #endregion
        
        #region → Any, Contains or Find Element
        
        /// <summary>
        /// Similar to Any of <see> <cref>System.linq</cref> </see>
        /// it retrieve a valid index of the first element matching the predicate.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Any<T>(List<T> list, Predicate<T> predicate, out int index)
        {
            index = IndexOf(list, predicate);
            return index > -1;
        }
        
        /// <summary>
        /// Similar to Any of <see> <cref>System.linq</cref> </see>
        /// it retrieve a valid index of the first element matching the predicate.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="predicate"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Any<T>(T[] array, Predicate<T> predicate, out int index)
        {
            index = IndexOf(array, predicate);
            return index > -1;
        }
        
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
        
        #region Has Duplicates
        
        /// <summary>
        /// Has Duplicates?
        /// </summary>
        /// <param name="enumerable"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool HasDuplicates<T>(IEnumerable<T> enumerable) 
        {
            HashSet<T> hs = new HashSet<T>();
            return enumerable.Any(t => !hs.Add(t));
        }

        #endregion

        #region Index Of

        /// <summary>
        /// Index of element in the array
        /// </summary>
        /// <param name="array"></param>
        /// <param name="element"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int IndexOf<T>(T[] array, T element)
        {
            return Array.IndexOf(array, element);
        }

        /// <summary>
        /// Retrieve index if array contains an element with given predicate.
        /// Otherwise -1
        /// </summary>
        /// <param name="array"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>-1 if it fails</returns>
        public static int IndexOf<T>(T[] array, Predicate<T> predicate)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (predicate.Invoke(array[i]))
                {
                    return i;
                }
            }

            return -1;
        }
        
        /// <summary>
        /// Retrieve index if list contains an element with given predicate.
        /// Otherwise -1
        /// </summary>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>-1 if it fails</returns>
        public static int IndexOf<T>(List<T> list, Predicate<T> predicate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate.Invoke(list[i]))
                {
                    return i;
                }
            }

            return -1;
        }
        
        #endregion
        
        
        #region Is Null or Empty

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

        #endregion
        
        #region Is Valid Index
        
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
        
        #endregion
        
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
                Debug.LogError($"Failed to retrieve subArray. Caught Exception: {e.Message}.");
                subArray = null;
                return false;
            }
        }


        #endregion
    }
}
