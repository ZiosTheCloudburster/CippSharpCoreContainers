namespace CippSharp.Core.Containers
{
    public interface IContainerPair<TKey, TValue> : IContainerBase
    {
        /// <summary>
        /// Retrieve first of the two elements
        /// </summary>
        /// <returns></returns>
        TKey GetKey();
        /// <summary>
        /// Retrieve second of the two elements
        /// </summary>
        /// <returns></returns>
        TValue GetValue();

        /// <summary>
        /// Read/Write on data/value
        /// </summary>
        /// <param name="access"></param>
        void Access(AccessDelegate<TKey, TValue> access);

        /// <summary>
        /// Predicate on data/value
        /// </summary>
        /// <param name="access"></param>
        bool Check(PredicateAccessDelegate<TKey, TValue> access);

        /// <summary>
        /// Set both the contained element
        /// </summary>
        /// <param name="newKey"></param>
        /// <param name="newValue"></param>
        void Set(TKey newKey, TValue newValue);

        /// <summary>
        /// Set first of the two elements
        /// </summary>
        /// <param name="newKey"></param>
        void SetKey(TKey newKey);

        /// <summary>
        /// Set second of the two elements
        /// </summary>
        /// <param name="newValue"></param>
        void SetValue(TValue newValue);
    }
}