using System;
using System.Text;

namespace CippSharp.Core.Containers
{
    internal static class StringUtils
    {
        #region Log Name
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
        
        #endregion
        
        /// <summary>
        /// Encode bytes in UTF8 string
        /// </summary>
        /// <param name="bytes">must be not null</param>
        /// <returns></returns>
        public static string EncodeBytes(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);;
        }
    }
}
