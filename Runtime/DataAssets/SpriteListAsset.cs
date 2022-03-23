using UnityEngine;

namespace CippSharp.Core.Containers
{
    [CreateAssetMenu(menuName = nameof(CippSharp) + "/Data Assets/Sprite List Asset")]
    public class SpriteListAsset : AListDataAsset<Sprite>
    {
        /// <summary>
        /// Create a new instance of <see cref="SpriteListAsset"/>
        /// </summary>
        /// <returns></returns>
        public static SpriteListAsset CreateSpriteListAsset()
        {
            return ScriptableObject.CreateInstance<SpriteListAsset>();
        }
    }
}
