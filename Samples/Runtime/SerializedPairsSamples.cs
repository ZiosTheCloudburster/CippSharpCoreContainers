#if UNITY_EDITOR
using UnityEngine;

namespace CippSharp.Core.Containers.Samples
{
    internal class SerializedPairsSamples : MonoBehaviour
    {
        /// <summary>
        /// Tooltip
        /// </summary>
        public string message = "See what happens during runtime.";
        
        [Space(5)]
        [SerializeField] private RendererMaterialsPair savedRendererMaterials = new RendererMaterialsPair(null, null);
        [SerializeField] private MeshRendererMeshFilterPair savedMeshRendererMeshFilterPair = new MeshRendererMeshFilterPair(null, null);

        private void Start()
        {
            Renderer rend = GetComponentInChildren<Renderer>();
            MeshFilter filter = rend.GetComponent<MeshFilter>();
            savedRendererMaterials = new RendererMaterialsPair(rend, rend.materials);
            savedMeshRendererMeshFilterPair = new MeshRendererMeshFilterPair(rend as MeshRenderer, filter);
        }
    }
}
#endif
