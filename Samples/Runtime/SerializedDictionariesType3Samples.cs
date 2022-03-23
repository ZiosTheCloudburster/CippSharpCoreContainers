#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CippSharp.Core.Containers.Samples
{
    internal class SerializedDictionariesType3Samples : MonoBehaviour
    {
        [Serializable]
        internal class StringGameObjectPair : SerializedKeyValuePair<string, GameObject>
        {

        }

        [Serializable]
        internal class DictionaryStringGameObject : SerializedDictionary<StringGameObjectPair, string, GameObject>
        {

        }

        /// <summary>
        /// Populate this from inspector, then cache it during runtime
        /// </summary>
        [SerializeField] private DictionaryStringGameObject serializedDictionary = new DictionaryStringGameObject();

        private Dictionary<string, GameObject> cachedDictionary = null;

        private void Start()
        {
            //Cache the exposed dictionary from inspector to c# to use it in code. 
            cachedDictionary = serializedDictionary.ToDictionary();
        }
    }
}
#endif