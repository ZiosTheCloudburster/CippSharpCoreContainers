using System;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    [Serializable]
    public class RendererMaterialsPair : SerializedKeyValuePair<Renderer, Material[]>
    {
        public RendererMaterialsPair(Renderer key, Material[] value) : base(key, value)
        {
        }
    }
}
