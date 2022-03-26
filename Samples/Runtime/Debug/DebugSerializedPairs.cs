#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace CippSharp.Core.Containers.Samples
{
    public class DebugSerializedPairs : MonoBehaviour
    {
        /// <summary>
        /// Tooltip
        /// </summary>
        public string message = "Populate this to debug, then use the ContextMenu to launch the debug";
        
        [SerializeField] private RendererMaterialsPair savedRendererMaterials = new RendererMaterialsPair(null, null);
        
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
            DebugContainerUtils.DebugContainer((IContainerBase)savedRendererMaterials, ref reportData.output);
            DebugContainerUtils.DebugContainer((IContainer<object>)savedRendererMaterials, ref reportData.output);
            DebugContainerUtils.DebugContainer((IContainer<object[]>)savedRendererMaterials, ref reportData.output);
            DebugContainerUtils.DebugContainer((IContainer<KeyValuePair<Renderer, Material[]>>)savedRendererMaterials, ref reportData.output);
            DebugContainerUtils.DebugContainer((IContainerPair<Renderer, Material[]>)savedRendererMaterials, ref reportData.output);
        }
    }
}
#endif