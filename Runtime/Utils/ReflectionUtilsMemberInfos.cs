using System;
using System.Linq;
using System.Reflection;

namespace CippSharp.Core.Containers
{
    using Debug = UnityEngine.Debug;
    
    public static partial class ReflectionUtils
    {
        #region MemberInfo Methods

        /// <summary>
        /// Returns true if the context object has the target member.
        /// It also throws out the interested member.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="memberName"></param>
        /// <param name="member"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        public static bool HasMember(object context, string memberName, out MemberInfo member, BindingFlags flags = Common)
        {
            try
            {
                member = context.GetType().GetMember(memberName, flags).FirstOrDefault();
                return member != null;
            }
            catch (Exception e)
            {
                UnityEngine.Object obj = context as UnityEngine.Object;
                string logName = StringUtils.LogName(obj);
                Debug.LogError(logName+ErrorMessagePrefix+e.Message, obj);
                member = null;
                return false;
            }            
        }

        /// <summary>
        /// Returns the value of target member if it exists otherwise return T's default value.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="memberName"></param>
        /// <param name="result"></param>
        /// <param name="bindingFlags"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryGetMemberValue<T>(object context, string memberName, out T result, BindingFlags bindingFlags = Common)
        {
            try
            {
                MemberInfo member = context.GetType().GetMember(memberName, bindingFlags).FirstOrDefault();
                return TryGetMemberValue(context, member, out result);
            }
            catch (Exception e)
            {
                UnityEngine.Object obj = context as UnityEngine.Object;
                string logName = StringUtils.LogName(obj);
                Debug.LogError(logName+ErrorMessagePrefix+e.Message, obj);
            }
            
            result = default(T);
            return false;
        }
        
        /// <summary>
        ///  If you already have the member, it returns the value of target member
        /// if it exists otherwise return T's default value.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="member"></param>
        /// <param name="result"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryGetMemberValue<T>(object context, MemberInfo member, out T result)
        {
            try
            {
                if (member != null)
                {
                    if (IsFieldInfo(member, out FieldInfo f))
                    {
                        result = (T) f.GetValue(context);
                        return true;
                    }
                    else if (IsPropertyInfo(member, out PropertyInfo p))
                    {
                        result = (T) p.GetValue(context, null);
                        return true;
                    }
                    else if (IsMethodInfo(member, out MethodInfo m))
                    {
                        result = (T) m.Invoke(context, null);
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                UnityEngine.Object obj = context as UnityEngine.Object;
                string logName = StringUtils.LogName(obj);
                Debug.LogError(logName+ErrorMessagePrefix+e.Message, obj);
            }
            
            result = default(T);
            return false;
        }


        #endregion
    }
}