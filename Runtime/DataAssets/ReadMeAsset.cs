using System;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    [CreateAssetMenu(menuName = nameof(CippSharp)+"/Data Assets/ReadMe Asset")]
    public class ReadMeAsset : ADataAsset<ReadMeAsset.Data>
    {
        [Serializable]
        public struct Data
        {
            [TextArea(1, 50)]
            public string text;
        }
    }
}
