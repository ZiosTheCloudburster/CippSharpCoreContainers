
namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Interface for specific container
    /// </summary>
    public interface IContainer<T> : IContainerBase
    {
        /// <summary>
        /// Retrieve the contained element
        /// </summary>
        /// <returns></returns>
        T GetValue();

        /// <summary>
        /// Read/Write on data/value
        /// </summary>
        /// <param name="access"></param>
        void Access(AccessDelegate<T> access);

        /// <summary>
        /// Predicate on data/value
        /// </summary>
        /// <param name="access"></param>
        bool Check(PredicateAccessDelegate<T> access);

        /// <summary>
        /// Set the contained element
        /// </summary>
        /// <param name="newValue"></param>
        void Set(T newValue);
    }
}
