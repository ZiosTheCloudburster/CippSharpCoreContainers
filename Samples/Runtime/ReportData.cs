using System;
using UnityEngine;

namespace CippSharp.Core.Containers.Samples
{
    [Serializable]
    internal struct ReportData
    {
#pragma warning disable 649
        [TextArea(1, 10)] 
        public string output;
#pragma warning restore 649
    }
}
