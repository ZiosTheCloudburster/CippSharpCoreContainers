using System;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    [Serializable]
    public class MeshRendererMeshFilterPair : SerializedKeyValuePair<MeshRenderer, MeshFilter>
    {
        public MeshRendererMeshFilterPair(MeshRenderer key, MeshFilter value) : base(key, value)
        {
            
        }
    }
}
