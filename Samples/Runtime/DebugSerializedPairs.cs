using UnityEngine;

namespace CippSharp.Core.Containers.Samples
{
    public class DebugSerializedPairs : MonoBehaviour
    {
        /// <summary>
        /// Tooltip
        /// </summary>
        public string message = "Populate this to debug.";
        
        [SerializeField] private RendererMaterialsPair savedRendererMaterials = new RendererMaterialsPair(null, null);
        
        /// <summary>
        /// Debug report
        /// </summary>
        [Header("Infos:")]
        [SerializeField] 
        private ReportData reportData = new ReportData();


        public void RunDebug()
        {
            reportData.output = string.Empty;
            AContainerBase containerBase = (AContainerBase)savedRendererMaterials;
            reportData.output += containerBase.ContainerType.FullName;

        }
    }
}
