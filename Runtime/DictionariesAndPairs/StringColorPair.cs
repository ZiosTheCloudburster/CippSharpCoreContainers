using System;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    [Serializable]
    public class StringColorPair : SerializedKeyValuePair<string, Color>
    {
        /// <summary>
        /// Empty StringColor
        /// </summary>
        public static readonly StringColorPair Empty = new StringColorPair("", default(Color));

        protected StringColorPair()
        {
            
        }

        public StringColorPair(string name, Color color)
        {
            this.key = name;
            this.value = color;
        }
    }
}
