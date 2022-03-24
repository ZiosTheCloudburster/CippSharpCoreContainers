using System;
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

        #endregion
    }
}
