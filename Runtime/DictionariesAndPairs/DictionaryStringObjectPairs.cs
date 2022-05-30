using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Containers
{
    [Serializable]
    public class DictionaryStringObjectPairs : SerializedDictionary<StringObjectPair, string, Object>
    {
        public DictionaryStringObjectPairs() : base()
        {
            
        }
        
        public DictionaryStringObjectPairs(IEnumerable<StringObjectPair> range) : base(range)
        {
            
        }
    }
}
