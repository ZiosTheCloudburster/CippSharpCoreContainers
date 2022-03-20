using UnityEngine;

namespace CippSharp.Core.Containers
{
    [CreateAssetMenu(menuName = nameof(CippSharp) + "/Data Assets/Sprite List Asset")]
    public class SpriteListAsset : AListDataAsset<Sprite>
    {
        public static SpriteListAsset CreateInstance()
        {
            return ScriptableObject.CreateInstance<SpriteListAsset>();
        }
    }
}
