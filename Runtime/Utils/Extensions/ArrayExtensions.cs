
namespace CippSharp.Core.Containers
{
    internal static class ArrayExtensions
    {
        /// <summary>
        /// Index of element in the array
        /// </summary>
        /// <param name="array"></param>
        /// <param name="element"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static int IndexOf<T>(this T[] array, T element)
        {
            return ArrayUtils.IndexOf(array, element);
        }
    }
}
