namespace CippSharp.Core.Containers
{
    public interface IContainerPair<T1, T2> : IContainerBase
    {
        /// <summary>
        /// Retrieve first of the two elements
        /// </summary>
        /// <returns></returns>
        T1 GetKey();
        /// <summary>
        /// Retrieve second of the two elements
        /// </summary>
        /// <returns></returns>
        T2 GetValue();

        /// <summary>
        /// Read/Write on data/value
        /// </summary>
        /// <param name="access"></param>
        void Access(AccessDelegate<T1, T2> access);

        /// <summary>
        /// Predicate on data/value
        /// </summary>
        /// <param name="access"></param>
        bool Check(PredicateAccessDelegate<T1, T2> access);

        /// <summary>
        /// Set both the contained element
        /// </summary>
        /// <param name="newKey"></param>
        /// <param name="newValue"></param>
        void Set(T1 newKey, T2 newValue);

        /// <summary>
        /// Set first of the two elements
        /// </summary>
        /// <param name="newKey"></param>
        void SetKey(T1 newKey);

        /// <summary>
        /// Set second of the two elements
        /// </summary>
        /// <param name="newValue"></param>
        void SetValue(T2 newValue);
    }
}