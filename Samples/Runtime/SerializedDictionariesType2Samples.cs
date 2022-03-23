#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CippSharp.Core.Containers.Samples
{
    internal class SerializedDictionariesType2Samples : MonoBehaviour
    {
        [Serializable]
        internal class DictionaryStringGameObject : SerializedDictionary<string, GameObject>
        {

        }

//        public string message = "Populate the lists and see what happens during runtime.";
        
        [SerializeField]
        private DictionaryStringGameObject serializedDictionary = new DictionaryStringGameObject();

//        [SerializeField] private List<string> names = new List<string>();
//        [SerializeField] private List<GameObject> targets = new List<GameObject>();

//        [Space(10)]
//        [SerializeField]

        private Dictionary<string, GameObject> cachedDictionary = null;
        
        private void Start()
        {
            cachedDictionary = serializedDictionary.ToDictionary();
        }
    }
}
#endif