#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    public static class GUIContentUtils
    {
        /// <summary>
        /// A string array to an array of GUI Contents
        /// </summary>
        /// <returns></returns>
        public static GUIContent[] ToGUIContents(IEnumerable<string> contents)
        {
            return contents.Select(c => new GUIContent(c)).ToArray();
        }
    }
}
#endif
