#if UNITY_EDITOR
using UnityEngine;

namespace CippSharp.Core.Containers.Samples
{
    public class SerializedPairsSamples : MonoBehaviour
    {
        [SerializeField] private RendererMaterialsPair savedRendererMaterials;
        [SerializeField] private MeshRendererMeshFilterPair savedMeshRendererMeshFilterPair;

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
