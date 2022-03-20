//
// Author: Alessandro Salani (Cippo)
//

using UnityEngine;

namespace CippSharp.Core
{
    [CreateAssetMenu(menuName = nameof(CippSharp)+"/Data Assets/ReadMe Asset")]
    public class ReadMeAsset : ScriptableObject
    {
        [TextArea(1, 50)] 
        public string text;
    }
}
