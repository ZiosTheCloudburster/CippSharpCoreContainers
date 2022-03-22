//
// Author: Alessandro Salani (Cippo)
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    public static partial class ReflectionUtils
    {
        /// <summary>
        /// Common binding flags for most public and non public methods.
        /// </summary>
        public const BindingFlags Common = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy;

        public const string ErrorMessagePrefix = "Error ";
        
        /// <summary>
        /// A better name for logs
        /// </summary>
        public static readonly string LogName = $"[{nameof(ReflectionUtils)}]: ";
        
        /// <summary>
        /// Create an instance of a type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        /// <param name="flags"></param>
        /// <returns>success</returns>
        public static bool CreateInstance(Type type, out object instance, BindingFlags flags = Common)
        {
            var constructor = type.GetConstructors(flags).FirstOrDefault(c => c.GetParameters().Length == 0);
            if (constructor == null)
            {
                instance = null;
                return false;
            }

            instance = constructor.Invoke(null);
            return true;
        }

        #region Find Type(s)
        
        
        /// <summary>
        /// Find type via string
        /// </summary>
        /// <param name="typeFullName"></param>
        /// <param name="foundType"></param>
        /// <returns></returns>
        public static bool FindType(string typeFullName, out Type foundType)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.FullName != typeFullName)
                    {
                        continue;
                    }
                    
                    foundType = type;
                    return true;
                }
            }

            foundType = null;
            return false;
        }

        /// <summary>
        /// Find types via predicate
        /// </summary>
        /// <param name="predicate">must not be null</param>
        /// <param name="foundTypes"></param>
        /// <returns></returns>
        public static bool FindTypes(Predicate<Type> predicate, out List<Type> foundTypes)
        {
            foundTypes = (from assembly in AppDomain.CurrentDomain.GetAssemblies() from type in assembly.GetTypes() where predicate.Invoke(type) select type).ToList();
            return foundTypes.Count > 0;
        }
        
        #endregion
        
        #region Get Attributes

        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example><![CDATA[string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;]]></example>
        public static T GetAttributeOfType<T>(Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }
        
//        /// <summary>
//        /// Gets attributes on an enum field value
//        /// </summary>
//        /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
//        /// <param name="enumVal">The enum value</param>
//        /// <returns>The attribute of type T that exists on the enum value</returns>
//        /// <example><![CDATA[string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;]]></example>
//        public static T[] GetAttributesOfType<T>(Enum enumVal) where T : Attribute
//        {
//            var type = enumVal.GetType();
//            var memInfo = type.GetMember(enumVal.ToString());
//            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
//            return ArrayUtils.SelectIf(attributes, a => a is T, a => (T)a).ToArray();
//        }

        #endregion
        
        #region Is
        
        /// <summary>
        /// Is field info
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsFieldInfo(MemberInfo member)
        {
            return member is FieldInfo;
        }

        /// <summary>
        /// Is field info
        /// </summary>
        /// <param name="member"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool IsFieldInfo(MemberInfo member, out FieldInfo field)
        {
            if (member is FieldInfo f)
            {
                field = f;
                return true;
            }
            else
            {
                field = null;
                return false;
            }
        }
        
        /// <summary>
        /// Is property info 
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsPropertyInfo(MemberInfo member)
        {
            return member is PropertyInfo;
        }

        /// <summary>
        /// Is property info 
        /// </summary>
        /// <param name="member"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool IsPropertyInfo(MemberInfo member, out PropertyInfo property)
        {
            if (member is PropertyInfo p)
            {
                property = p;
                return true;
            }
            else
            {
                property = null;
                return false;
            }
        }

        /// <summary>
        /// Is member info
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static bool IsMethodInfo(MemberInfo member)
        {
            return member is MethodInfo;
        }

        /// <summary>
        /// Is member info
        /// </summary>
        /// <param name="member"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsMethodInfo(MemberInfo member, out  MethodInfo method)
        {
            if (member is MethodInfo m)
            {
                method = m;
                return true;
            }
            else
            {
                method = null;
                return false;
            }
        }
        

        #endregion

        #region Print Stuffs
        
        /// <summary>
        /// Log methods of type in console.
        /// </summary>
        public static void PrintMethods(Type type, BindingFlags flags = Common, UnityEngine.Object context = null)
        {
            List<string> methodNames = new List<string>();
            foreach (var methodInfo in type.GetMethods(flags))
            {
                string methodName = methodInfo.Name;
                methodNames.Add(methodName);
                string logName = StringUtils.LogName(context);
                string message = string.Format("Method name: <i>{0}</i>, Overload Count: <i>{1}</i>.", methodName, methodNames.Count(m => m == methodName));
                Debug.Log(logName+message, context);
            }
        }
        
        /// <summary>
        /// Log members of type in console.
        /// </summary>
        public static void PrintMembers(Type type, BindingFlags flags = Common, UnityEngine.Object context = null)
        {
            List<string> membersNames = new List<string>();
            foreach (var members in type.GetMembers(flags))
            {
                string memberName = members.Name;
                membersNames.Add(memberName);
                string logName = StringUtils.LogName(context);
                string message = string.Format("Method name: <i>{0}</i>, Overload Count: <i>{1}</i>.", memberName, membersNames.Count(m => m == memberName));
                Debug.Log(logName+message, context);
            }
        }

        #endregion
        
    }
}
