namespace CippSharp.Core.Containers
{
    public static class ContainerExtensions
    {
        /// <summary>
        /// Is this container valid?
        /// </summary>
        /// <param name="container"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsValid<T>(this Container<T> container)
        {
            return container != null;
        }
    }
}
