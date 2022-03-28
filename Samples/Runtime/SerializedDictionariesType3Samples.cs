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
            public override void Clear()
            {
                Debug.Log("Before Clear "+Count);
                
                base.Clear();
                
                Debug.Log("After Clear "+Count);
            }

            public override void AddRange(IEnumerable<KeyValuePair<string, GameObject>> enumerable)
            {
                base.AddRange(enumerable);
                
                Debug.Log("After Add Range "+Count);
            }
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