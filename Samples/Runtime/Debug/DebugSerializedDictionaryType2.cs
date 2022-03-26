#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace CippSharp.Core.Containers.Samples
{
    using DictionaryStringGameObject = SerializedDictionariesType2Samples.DictionaryStringGameObject;
    
    internal class DebugSerializedDictionaryType2 : MonoBehaviour
    {
        /// <summary>
        /// Tooltip
        /// </summary>
        public string message = "Populate this to debug, then use the ContextMenu to launch the debug";
        
        /// <summary>
        /// Populate this from inspector, then cache it during runtime
        /// </summary>
        [SerializeField] 
        private DictionaryStringGameObject serializedDictionary = new DictionaryStringGameObject();
        
        /// <summary>
        /// Debug report
        /// </summary>
        [Header("Infos:")]
        [SerializeField, NotEditable] private ReportData reportData = new ReportData();
        
        /// <summary>
        /// Run Debug
        /// </summary>
        [ContextMenu("Run Debug")]        
        public void RunDebug()
        {
            reportData.output = string.Empty;
//            DebugContainerUtils.DebugContainer((IContainerBase)serializedDictionary, ref reportData.output);
//            DebugContainerUtils.DebugContainer((IContainer<object>)serializedDictionary, ref reportData.output);
//            DebugContainerUtils.DebugContainer((IContainer<object[]>)serializedDictionary, ref reportData.output);
//            DebugContainerUtils.DebugContainer((IContainer<KeyValuePair<Renderer, Material[]>>)serializedDictionary, ref reportData.output);
//            DebugContainerUtils.DebugContainer((IContainerPair<List<string>, List<GameObject>>)serializedDictionary, ref reportData.output);
            DebugContainerUtils.DebugContainer((ICollectionContainer<ICollection<KeyValuePair<string, GameObject>>, KeyValuePair<string, GameObject>>)serializedDictionary, ref reportData.output);
//            DebugContainerUtils.DebugContainer((IDictionaryContainer<string, GameObject>)serializedDictionary, ref reportData.output);
        }

    }
}
#endif