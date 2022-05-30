using System;

namespace CippSharp.Core.Containers
{
    [Serializable]
    public class StringObjectPair : SerializedKeyValuePair<string, UnityEngine.Object>
    {
        /// <summary>
        /// Empty StringObject
        /// </summary>
        public static readonly StringObjectPair Empty = new StringObjectPair(string.Empty, null);

        protected StringObjectPair()
        {
            
        }

        public StringObjectPair(string name, UnityEngine.Object o)
        {
            this.key = name;
            this.value = o;
        }
    }
}
