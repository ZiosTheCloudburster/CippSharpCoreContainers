using System;
using System.Collections.Generic;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    [Serializable]
    public class DictionaryMeshRendererMeshFilterPairs : SerializedDictionary<MeshRendererMeshFilterPair, MeshRenderer, MeshFilter>
    {
        public DictionaryMeshRendererMeshFilterPairs(IEnumerable<MeshRendererMeshFilterPair> range) : base(range)
        {
            
        }
    }
}
