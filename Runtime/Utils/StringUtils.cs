using System;

namespace CippSharp.Core.Containers
{
    internal static class StringUtils
    {
        /// <summary>
        /// Retrieve a more contextual name for logs, based on type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string LogName(Type type)
        {
            return $"[{type.Name}]: ";
        }
        
        /// <summary>
        /// Retrieve a more contextual name for logs, based on object.
        /// If object is null an empty string is returned.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string LogName(object context)
        {
            return ((object)context == null) ? string.Empty : LogName(context.GetType());
        }
    }
}
