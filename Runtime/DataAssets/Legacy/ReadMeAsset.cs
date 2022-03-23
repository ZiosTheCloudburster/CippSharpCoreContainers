//
// Author: Alessandro Salani (Cippo)
//

using System;
using UnityEngine;

namespace CippSharp.Core.Containers.Legacy
{
    /// <summary>
    /// To keep just as reference.
    /// Please use the new non legacy <see cref="CippSharp.Core.Containers.ReadMeAsset"/>
    /// </summary>
    [Obsolete("Legacy Link Holder Asset. Use the non-legacy ReadMeAsset instead.")]
    internal class ReadMeAsset : ScriptableObject
    {
#pragma warning disable 649
        [TextArea(1, 50)] 
        public string text = string.Empty;
#pragma warning restore 649
    }
}
