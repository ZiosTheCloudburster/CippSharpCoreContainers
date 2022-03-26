using System.Collections.Generic;

namespace CippSharp.Core.Containers
{
    public interface IDictionaryContainer<TKey, TValue> : ICollectionContainer<ICollection<KeyValuePair<TKey, TValue>>, KeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// The Dictionary
        /// </summary>
        IDictionary<TKey, TValue> Dictionary { get; }
        
        /// <summary>
        /// The Keys
        /// </summary>
        ICollection<TKey> Keys { get; }
        
        /// <summary>
        /// The Values
        /// </summary>
        ICollection<TValue> Values { get; }

        /// <summary>
        /// Access the dictionary
        /// </summary>
        /// <param name="key"></param>
        TValue this [TKey key] { get; set; }
    }
}
