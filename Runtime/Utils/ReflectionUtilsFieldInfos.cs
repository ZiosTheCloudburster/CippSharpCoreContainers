using System;
using System.Collections;
using System.Reflection;

namespace CippSharp.Core.Containers
{
    using Debug = UnityEngine.Debug;

    public static partial class ReflectionUtils
    {
        #region FieldInfo Methods
        
        /// <summary>
        /// Returns true if the context object has the target field. It also throws out the interested field.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fieldName"></param>
        /// <param name="field"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool HasField(object context, string fieldName, out FieldInfo field, BindingFlags flags = Common)
        {
            try
            {
                field = context.GetType().GetField(fieldName, flags);
                return field != null;
            }
            catch (Exception e)
            {
                UnityEngine.Object obj = context as UnityEngine.Object;
                string logName = StringUtils.LogName(obj);
                Debug.LogError(logName+ErrorMessagePrefix+e.Message, obj);
                
                field = null;
                return false;
            }            
        }
        
        /// <summary>
        /// Old way to retrieve all public constant fields of a type
        /// https://stackoverflow.com/questions/10261824/how-can-i-get-all-constants-of-a-type-by-reflection
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static FieldInfo[] GetPublicConstantsFields(Type type)
        {
            ArrayList constants = new ArrayList();

            FieldInfo[] fieldInfos = type.GetFields(
                // Gets all public and static fields

                BindingFlags.Public | BindingFlags.Static | 
                // This tells it to get the fields from all base types as well
                BindingFlags.FlattenHierarchy);

            // Go through the list and only pick out the constants
            foreach(FieldInfo fi in fieldInfos)
                // IsLiteral determines if its value is written at 
                //   compile time and not changeable
                // IsInitOnly determines if the field can be set 
                //   in the body of the constructor
                // for C# a field which is readonly keyword would have both true 
                //   but a const field would have only IsLiteral equal to true
                if(fi.IsLiteral && !fi.IsInitOnly)
                    constants.Add(fi);           

            // Return an array of FieldInfos
            return (FieldInfo[])constants.ToArray(typeof(FieldInfo));
        }
        
        
        /// <summary>
        /// Returns the value of target field if it exists otherwise return T's default value.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fieldName"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static T GetFieldValue<T>(object context, string fieldName, BindingFlags bindingFlags = Common)
        {
            TryGetFieldValue(context, fieldName, out T result, bindingFlags);
            return result;
        }

        /// <summary>
        /// Try to return the value of target field if it exists otherwise return T's default value.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fieldName"></param>
        /// <param name="result"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryGetFieldValue<T>(object context, string fieldName, out T result, BindingFlags bindingFlags = Common)
        {
            result = default(T); 
            try
            {
                FieldInfo fieldInfo = context.GetType().GetField(fieldName, bindingFlags);
                if (fieldInfo != null)
                {
                    result = (T) fieldInfo.GetValue(context);
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
        /// Returns true if successful set the new value to the field.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool TrySetFieldValue(object context, string fieldName, object fieldValue, BindingFlags flags = Common)
        {
            return TrySetFieldValue(ref context, fieldName, fieldValue);
        }

        /// <summary>
        /// Returns true if successful set the new value to the field.
        /// NOTE: use this for 'structs'
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fieldName"></param>
        /// <param name="fieldValue"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool TrySetFieldValue(ref object context, string fieldName, object fieldValue, BindingFlags flags = Common)
        {
            FieldInfo fieldInfo = null;
            try
            {
                fieldInfo = context.GetType().GetField(fieldName, flags);
            }
            catch (Exception e)
            {
                UnityEngine.Object obj = context as UnityEngine.Object;
                string logName = StringUtils.LogName(obj);
                Debug.LogError(logName+ErrorMessagePrefix+e.Message, obj);
                return false;
            }

            if (fieldInfo == null)
            {
                return false;
            }

            try
            {
                fieldInfo.SetValue(context, fieldValue);
            }
            catch (Exception e)
            {
                UnityEngine.Object obj = context as UnityEngine.Object;
                string logName = StringUtils.LogName(obj);
                Debug.LogError(logName+ErrorMessagePrefix+e.Message, obj);
                return false;
            }

            return true;
        }

        #endregion
    }
}
