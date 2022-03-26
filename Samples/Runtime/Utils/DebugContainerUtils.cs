#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;

namespace CippSharp.Core.Containers.Samples
{
    using Type = System.Type;
    
    internal static class DebugContainerUtils
    {
        /// <summary>
        /// When something is 'null'
        /// </summary>
        private const string NullValue = "null";
        
        /// <summary>
        /// New Line Value
        /// </summary>
        private static readonly string NewLine = System.Environment.NewLine;

        /// <summary>
        /// Container is invalid error
        /// </summary>
        /// <returns></returns>
        private const string InvalidContainerOutput = "This container is null!"; 
        
        #region Log Name
        /// <summary>
        /// Retrieve a more contextual name for logs, based on type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string LogName(Type type)
        {
            return $"[{type.Name}]: ";
        }
        
        /// <summary>
        /// Retrieve a more contextual name for logs, based on object.
        /// If object is null an empty string is returned.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static string LogName(object context)
        {
            return ((object)context == null) ? string.Empty : LogName(context.GetType());
        }
        
        #endregion

        /// <summary>
        /// Raw-ly write value of target.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private static string ToStringObjectValue(object target)
        {
            if (target == null)
            {
                return NullValue;
            }
            else
            {
                if (target is string s)
                {
                    return s;
                }
                else
                {
                    return target.ToString();
                }
            }
        }
       
        /// <summary>
        /// Debug an abstract container
        /// </summary>
        /// <param name="container"></param>
        /// <param name="output"></param>
        public static void DebugContainer(IContainerBase container, ref string output)
        {
            //Default
            EnsureStartWithNewLine(ref output);
            if (!CheckValidity(container, ref output)) { return; }
            
            //Output
            string logName = LogName(container);
            output += $"{logName} container type {container.ContainerType.FullName}";
            output += $"{NewLine} raw value: {ToStringObjectValue(container.GetValueRaw())}";
            string delegatesOutput = string.Empty;
            try
            {
                container.Access((ref object data) =>
                {
                    delegatesOutput = $"{NewLine} accessed data: {ToStringObjectValue(data)}";
                });
            }
            catch (Exception e)
            {
                delegatesOutput = $"{NewLine} failed to access data. Exception: {e.Message}";
            }
            output += delegatesOutput;
            delegatesOutput = string.Empty;
            try
            {
                container.Check((ref object data) =>
                {
                    delegatesOutput = $"{NewLine} checked data: {ToStringObjectValue(data)}";
                    return true;
                });
            }
            catch (Exception e)
            {
                delegatesOutput = $"{NewLine} failed to check data. Exception: {e.Message}";
            }
            output += delegatesOutput;
            delegatesOutput = string.Empty;
            try
            {
                //self set of container
                container.Set(container.GetValueRaw());
                output += $"{NewLine} set data successful.";
            }
            catch (Exception e)
            {
                output += $"{NewLine} set data failed. Exception: {e.Message}";
            }
        }

        /// <summary>
        /// Debug a container of type T
        /// </summary>
        /// <param name="container"></param>
        /// <param name="output"></param>
        public static void DebugContainer<T>(IContainer<T> container, ref string output)
        {
            //Default
            EnsureStartWithNewLine(ref output);
            if (!CheckValidity(container, ref output)) { return; }
            
            //Output
            string logName = LogName(container);
            output += $"{logName} container type {container.ContainerType.FullName}";
            output += $"{NewLine} value: {ToStringObjectValue(container.GetValue())}";
            string delegatesOutput = string.Empty;
            try
            {
                container.Access((ref T data) =>
                {
                    delegatesOutput = $"{NewLine} accessed data: {ToStringObjectValue(data)}";
                });
            }
            catch (Exception e)
            {
                delegatesOutput = $"{NewLine} failed to access data. Exception: {e.Message}";
            }
            output += delegatesOutput;
            delegatesOutput = string.Empty;
            try
            {
                container.Check((ref T data) =>
                {
                    delegatesOutput = $"{NewLine} checked data: {ToStringObjectValue(data)}";
                    return true;
                });
            }
            catch (Exception e)
            {
                delegatesOutput = $"{NewLine} failed to check data. Exception: {e.Message}";
            }
            output += delegatesOutput;
            delegatesOutput = string.Empty;
            try
            {
                //self set of container
                container.Set(container.GetValue());
                output += $"{NewLine} set data successful.";
            }
            catch (Exception e)
            {
                output += $"{NewLine} set data failed. Exception: {e.Message}";
            }
        }
        
        /// <summary>
        /// Debug a pair container
        /// </summary>
        /// <param name="container"></param>
        /// <param name="output"></param>
        public static void DebugContainer<TKey, TValue>(IContainerPair<TKey, TValue> container, ref string output)
        {
            //Default
            EnsureStartWithNewLine(ref output);
            if (!CheckValidity(container, ref output)) { return; }
            //Output
            string logName = LogName(container);
            output += $"{logName} container type {container.ContainerType.FullName}";
            output += $"{NewLine} key: {ToStringObjectValue(container.GetKey())}";
            output += $"{NewLine} value: {ToStringObjectValue(container.GetValue())}";
            string delegatesOutput = string.Empty;
            try
            {
                container.Access((ref TKey key, ref TValue value) =>
                {
                    delegatesOutput = $"{NewLine} accessed key: {ToStringObjectValue(key)}; accessed value: {ToStringObjectValue(value)}";
                });
            }
            catch (Exception e)
            {
                delegatesOutput = $"{NewLine} failed to access data. Exception: {e.Message}";
            }
            output += delegatesOutput;
            delegatesOutput = string.Empty;
            try
            {
                container.Check((ref TKey key, ref TValue value) =>
                {
                    delegatesOutput = $"{NewLine} checked key: {ToStringObjectValue(key)}; checked value: {ToStringObjectValue(value)}";
                    return true;
                });
            }
            catch (Exception e)
            {
                delegatesOutput = $"{NewLine} failed to check data. Exception: {e.Message}";
            }
            output += delegatesOutput;
            delegatesOutput = string.Empty;
            try
            {
                //self set of container
                container.SetKey(container.GetKey());
                container.SetValue(container.GetValue());
                output += $"{NewLine} set data successful.";
            }
            catch (Exception e)
            {
                output += $"{NewLine} set data failed. Exception: {e.Message}";
            }
        }

        /// <summary>
        /// Debug a collection container
        /// </summary>
        /// <param name="container"></param>
        /// <param name="output"></param>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="T"></typeparam>
        public static void DebugContainer<K, T>(ICollectionContainer<K, T> container, ref string output) where K : ICollection<T>
        {
            //Default
            EnsureStartWithNewLine(ref output);
            if (!CheckValidity(container, ref output)) { return; }
            //Output
            string logName = LogName(container);
            output += $"{logName} container type {container.ContainerType.FullName}";
            output += $"{NewLine} collection: {ToStringObjectValue(container.Collection)}";
            output += $"{NewLine} count: {ToStringObjectValue(container.Count)}";
            try
            {
                output += $"{NewLine} elements: [";
                for (int i = 0; i < container.Count; i++)
                {
                    T element = container[i];
                    output += $"element ({i.ToString()} {ToStringObjectValue(element)}), ";
                    container[i] = element;
                }
                
                output += $"]{NewLine} iteration successful.";
            }
            catch (Exception e)
            {
                output += $"{NewLine} iteration failed. Exception: {e.Message}";
            }

            try
            {
                output += $"{NewLine} methods: [";
                T[] array = container.ToArray();
                output += $"{nameof(container.ToArray)}(), ";
                
                if (container.Count > 0)
                {
                    T firstElement = container[0];
                    
                    output += "(count > 0 case), ";
                    container.Clear();
                    output += $"{nameof(container.Clear)}(), ";
                    
                    container.Add(firstElement);
                    output += $"{nameof(container.Add)}({nameof(firstElement)}), ";
                    bool check = container.Contains(firstElement);
                    output += $"{nameof(container.Contains)}({nameof(firstElement)}) {check}, ";
                    int index = container.IndexOf(firstElement);
                    output += $"{nameof(container.IndexOf)}({nameof(firstElement)}) {index}, ";
                    check = container.Find(o => Equals(o, firstElement), out T tmpFirstElement);
                    output += $"{nameof(container.Find)}({nameof(firstElement)}) {check}, ";
                    check = container.Remove(firstElement);
                    output += $"{nameof(container.Remove)}({nameof(firstElement)}) {check}, ";
                    container.Add(firstElement);
                    output += $"{nameof(container.Add)}({nameof(firstElement)}), ";
                    container.RemoveAt(0);
                    output += $"{nameof(container.RemoveAt)}({nameof(firstElement)}), ";

                }
                //Restore
                container.Clear();
                output += $"{nameof(container.Clear)}(), ";
                container.AddRange(array);
                output += $"{nameof(container.AddRange)}({nameof(array)}), ]";
                output += $"{NewLine} default methods worked successful.";
            }
            catch (Exception e)
            {
                output += $"{NewLine} default methods failed. Exception: {e.Message}";
            }
        }

        /// <summary>
        /// Debug a dictionary collection container
        /// </summary>
        /// <param name="container"></param>
        /// <param name="output"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        public static void DebugContainer<TKey, TValue>(IDictionaryContainer<TKey, TValue> container, ref string output)
        {
            //Default
            EnsureStartWithNewLine(ref output);
            if (!CheckValidity(container, ref output)) { return; }
            //Output
            string logName = LogName(container);
            output += $"{logName} container type {container.ContainerType.FullName}";

            try
            {
                TKey[] keys = container.Keys.ToArray();
                TValue[] values = container.Values.ToArray();
                
                container.Clear();
                for (int i = 0; i < keys.Length; i++)
                {
                    TKey key = keys[i];
                    container[key] = values[i];
                }
                
                output += $"{NewLine} dictionary pair item get set worked successful.";
            }
            catch (Exception e)
            {
                output += $"{NewLine} dictionary pair item get set failed. Exception: {e.Message}";
            }
        }


        private static bool CheckValidity(IContainerBase container, ref string output)
        {
            if (!container.IsValid())
            {
                output += InvalidContainerOutput;
                return false;
            }
            else
            {
                return true;
            }
        }

        private static void EnsureStartWithNewLine(ref string output)
        {
            if (!string.IsNullOrEmpty(output))
            {
                output += NewLine;
            }
        } 
    }
}
#endif
