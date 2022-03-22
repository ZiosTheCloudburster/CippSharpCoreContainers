using System;

namespace CippSharp.Core.Containers
{
    public interface IContainer<T1, T2> : IContainerBase
    {
        /// <summary>
        /// Retrieve the contained element
        /// </summary>
        /// <returns></returns>
        Tuple<T1, T2> GetValue();
        
        /// <summary>
        /// Read/Write on data/value
        /// </summary>
        /// <param name="access"></param>
        void Access(AccessDelegate<Tuple<T1, T2>> access);
        
        /// <summary>
        /// Predicate on data/value
        /// </summary>
        /// <param name="access"></param>
        bool Check(PredicateAccessDelegate<Tuple<T1, T2>> access);

        /// <summary>
        /// Set the contained element
        /// </summary>
        /// <param name="newValue"></param>
        void Set(Tuple<T1, T2> newValue);
    }
}
