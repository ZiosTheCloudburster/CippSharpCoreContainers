using System;
using System.Reflection;

namespace CippSharp.Core.Containers
{
    using Debug = UnityEngine.Debug;

    public static partial class ReflectionUtils
    {
        #region PropertyInfo Methods

        /// <summary>
        /// Returns true if the context object has the target property. It also throws out the interested property.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="propertyName"></param>
        /// <param name="property"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool HasProperty(object context, string propertyName, out PropertyInfo property, BindingFlags flags = Common)
        {
            try
            {
                property = context.GetType().GetProperty(propertyName, flags);
                return property != null;
            }
            catch (Exception e)
            {
                UnityEngine.Object obj = context as UnityEngine.Object;
                string logName = StringUtils.LogName(obj);
                Debug.LogError(logName+ErrorMessagePrefix+e.Message, obj);
                
                property = null;
                return false;
            }
        }

        /// <summary>
        /// Retrieve the value of a property if it exists otherwise return T's default value.
        /// </summary>
        /// <param name="contextType"></param>
        /// <param name="propertyName"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetStaticPropertyValue<T>(Type contextType, string propertyName, BindingFlags bindingFlags = Common)
        {
            TryGetStaticPropertyValue(contextType, propertyName, out T result, bindingFlags);
            return result;
        }
        
        /// <summary>
        /// Try to retrieve the value of a property if it exists otherwise return T's default value.
        /// </summary>
        /// <param name="contextType"></param>
        /// <param name="propertyName"></param>
        /// <param name="result"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryGetStaticPropertyValue<T>(Type contextType, string propertyName, out T result, BindingFlags bindingFlags = Common)
        {
            result = default(T);
            
            try
            {
                bindingFlags |= BindingFlags.Static;
                PropertyInfo property = contextType.GetProperty(propertyName, bindingFlags);
                if (property != null)
                {
                    result = (T) property.GetValue(null);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(LogName+ErrorMessagePrefix+e.Message);
            }

            return false;
        }
        
        

        /// <summary>
        /// Retrieve the value of a property if it exists otherwise return T's default value.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="propertyName"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetPropertyValue<T>(object context, string propertyName, BindingFlags bindingFlags = Common)
        {
            TryGetPropertyValue(context, propertyName, out T result, bindingFlags);
            return result;
        }

        /// <summary>
        /// Retrieve the value of a property if it exists otherwise return T's default value.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="propertyName"></param>
        /// <param name="result"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="T"></typeparam>
        public static bool TryGetPropertyValue<T>(object context, string propertyName, out T result, BindingFlags bindingFlags = Common)
        {
            result = default(T);
            
            try
            {
                PropertyInfo propertyInfo = context.GetType().GetProperty(propertyName, bindingFlags);
                if (propertyInfo != null)
                {
                    result = (T) propertyInfo.GetValue(context, null);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                UnityEngine.Object obj = context as UnityEngine.Object;
                string logName = StringUtils.LogName(obj);
                Debug.LogError(logName+ErrorMessagePrefix+e.Message, obj);
            }
            
            return false;
        } 

        /// <summary>
        /// Retrieve the value of a property if it exists otherwise return T's default value.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyValue"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static bool TrySetPropertyValue(object context, string propertyName, object propertyValue, BindingFlags bindingFlags = Common)
        {
            PropertyInfo propertyInfo = null;
            try
            {
               propertyInfo = context.GetType().GetProperty(propertyName, bindingFlags);
            }
            catch (Exception e)
            {
                UnityEngine.Object obj = context as UnityEngine.Object;
                string logName = StringUtils.LogName(obj);
                Debug.LogError(logName+ErrorMessagePrefix+e.Message, obj);
                return false;
            }

            if (propertyInfo == null)
            {
                return false;
            }

            try
            {
                propertyInfo.SetValue(context, propertyValue);
            }
            catch (Exception e)
            {
                UnityEngine.Object obj = context as UnityEngine.Object;
                string logName = StringUtils.LogName(obj);
                Debug.LogError(logName + e.Message, obj);
                return false;
            }
            
            return true;
        }
        
        #endregion
    }
}
