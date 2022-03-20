using System;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Interface for any container
    /// </summary>
    public interface IContainerBase
    {
        /// <summary>
        /// The type of the contained element
        /// </summary>
        Type ContainerType { get; }

        /// <summary>
        /// Retrieve the contained element
        /// </summary>
        /// <returns></returns>
        object GetValueRaw();
        
        /// <summary>
        /// Read/Write on data/value
        /// </summary>
        /// <param name="access"></param>
        void Access(GenericAccessDelegate access);
        
        /// <summary>
        /// Predicate on data/value
        /// </summary>
        /// <param name="access"></param>
        bool Check(PredicateGenericAccessDelegate access);

        /// <summary>
        /// Set the contained element
        /// </summary>
        /// <param name="newValue"></param>
        void Set(object newValue);
    }
}
