//
// Author: Alessandro Salani (Cippo)
//

using System;
using UnityEngine;

namespace CippSharp.Core.Containers.Legacy
{
    /// <summary>
    /// To keep just as reference.
    /// Please use the new non legacy <see cref="UrlDataAsset"/>
    /// </summary>
//    [CreateAssetMenu(menuName = nameof(CippSharp)+"/Data Assets/ReadMe Asset")]
    [Obsolete("Legacy Link Holder Asset. Use the non-legacy ReadMeAsset instead.")]
    public class ReadMeAsset : ScriptableObject
    {
        [TextArea(1, 50)] 
        public string text;
    }
}
