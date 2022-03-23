using System;
using System.Collections.Generic;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    [Serializable]
    public class DictionaryRendererMaterialsPairs : SerializedDictionary<RendererMaterialsPair, Renderer, Material[]>
    {
        public DictionaryRendererMaterialsPairs(IEnumerable<RendererMaterialsPair> range) : base(range)
        {
            
        }
    }
}
