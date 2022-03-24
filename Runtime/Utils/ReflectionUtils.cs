//
// Author: Alessandro Salani (Cippo)
//

using System.Reflection;

namespace CippSharp.Core.Containers
{
    internal static class ReflectionUtils
    {
        /// <summary>
        /// Common binding flags for most public and non public methods.
        /// </summary>
        public const BindingFlags Common = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy;
    
        /// <summary>
        /// Returns true if the context object has the target field. It also throws out the interested field.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="fieldName"></param>
        /// <param name="field"></param>
        /// <param name="flags"></param>
        /// <returns>success</returns>
        public static bool HasField(object context, string fieldName, out FieldInfo field, BindingFlags flags = Common)
        {
            try
            {
                field = context.GetType().GetField(fieldName, flags);
                return field != null;
            }
            catch
            {
                //Ignored
                field = null;
                return false;
            }            
        }
        
        /// <summary>
        /// Call method if exists on target object.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <param name="result"></param>
        /// <param name="bindingFlags"></param>
        /// <returns>success</returns>
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
            catch
            {
                //Ignored
            }

            result = null;
            return false;
        }
    }
}
