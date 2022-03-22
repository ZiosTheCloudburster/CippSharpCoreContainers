namespace CippSharp.Core.Containers
{
    public interface IContainerPair<in T1, in T2> : IContainerBase
    {
        /// <summary>
        /// Retrieve one of the contained elements
        /// </summary>
        /// <returns></returns>
        T GetValue<T>(PairElement target) where T : T1, T2;

        /// <summary>
        /// Read/Write on data/value
        /// </summary>
        /// <param name="target"></param>
        /// <param name="access"></param>
        void Access<T>(PairElement target, AccessDelegate<T> access) where T : T1, T2;

        /// <summary>
        /// Predicate on data/value
        /// </summary>
        /// <param name="target"></param>
        /// <param name="access"></param>
        bool Check<T>(PairElement target, PredicateAccessDelegate<T> access) where T : T1, T2;

        /// <summary>
        /// Set the contained element
        /// </summary>
        /// <param name="target"></param>
        /// <param name="newValue"></param>
        void Set<T>(PairElement target, T newValue) where T : T1, T2;
    }
}