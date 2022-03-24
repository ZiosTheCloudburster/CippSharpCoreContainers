using System;
using System.Reflection;

namespace CippSharp.Core.Containers
{
    using Debug = UnityEngine.Debug;

    public static partial class ReflectionUtils
    {
        #region MethodInfo Methods
        
//        /// <summary>
//        /// Find a method via string
//        /// </summary>
//        /// <param name="type"></param>
//        /// <param name="methodName"></param>
//        /// <param name="methodInfo"></param>
//        /// <param name="flags"></param>
//        /// <returns></returns>
//        public static bool FindMethod(Type type, string methodName, out MethodInfo methodInfo, BindingFlags flags = Common)
//        {
//            methodInfo = type.GetMethod(methodName, flags);
//            return methodInfo != null;
//        }
        
//        /// <summary>
//        /// Returns true if the context object has the target method. It also throws out the interested method.
//        /// </summary>
//        /// <param name="context"></param>
//        /// <param name="methodName"></param>
//        /// <param name="method"></param>
//        /// <param name="flags"></param>
//        /// <returns></returns>
//        public static bool HasMethod(object context, string methodName, out MethodInfo method, BindingFlags flags = Common)
//        {
//            try
//            {
//                method = context.GetType().GetMethod(methodName, flags);
//                return method != null;
//            }
//            catch (Exception e)
//            {
//                UnityEngine.Object obj = context as UnityEngine.Object;
//                Debug.LogError(e.Message, obj);
//                
//                method = null;
//                return false;
//            }
//        }

//        /// <summary>
//        /// Check a condition on parameters of a method
//        /// </summary>
//        /// <param name="method"></param>
//        /// <param name="predicate"></param>
//        /// <returns></returns>
//        public static bool HasParametersCondition(MethodInfo method, Predicate<ParameterInfo[]> predicate)
//        {
//            try
//            {
//                ParameterInfo[] parameters = method.GetParameters();
//                return predicate.Invoke(parameters);
//            }
//            catch (Exception e)
//            {
//                Debug.LogError(e.Message);
//                return false;
//            }
//        }


        /// <summary>
        /// Call method if exists on target object.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <param name="result"></param>
        /// <param name="bindingFlags"></param>
        public static bool TryCallMethod(object context, string methodName, out object result, object[] parameters = null, BindingFlags bindingFlags = Common)
        {
            try
            {
                MethodInfo methodInfo = context.GetType().GetMethod(methodName, bindingFlags);
                if (methodInfo != null)
                {
                    result = methodInfo.Invoke(context, parameters);
                    return true;
                }
            }
            catch (Exception e)
            {
                UnityEngine.Object obj = context as UnityEngine.Object;
                Debug.LogError(e.Message, obj);
            }

            result = null;
            return false;
        }
        
        #endregion
    }
}
