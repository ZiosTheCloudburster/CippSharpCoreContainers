namespace CippSharp.Core.Containers
{
    internal static class CastExtensions
    {
        /// <summary>
        /// Casts an object to type T. In case of failure returns T default value. 
        /// </summary>
        /// <param name="target"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T ToOrDefault<T>(this object target)
        {
            return CastUtils.ToOrDefault<T>(target);
        }

        /// <summary>
        /// Returns true if successfully casted to T.
        /// Casts an object to type T.
        /// In case of failure returns false and T default value.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="result"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>success</returns>
        public static bool To<T>(this object target, out T result)
        {
            return CastUtils.To<T>(target, out result);
        }
    }
}
