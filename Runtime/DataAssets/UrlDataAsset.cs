using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Containers
{
    [CreateAssetMenu(menuName = nameof(CippSharp)+"/Data Assets/Url Holder Asset")]
    public class UrlDataAsset : ADataAsset<UrlDataAsset.Data>
    {
        [Serializable]
        public struct Data
        {
            [TextArea(1, 10)] public string description;
            [Space(5)]
            [ContextMenuItem("Open Url", "OpenUrl")]
            [TextArea(1, 5)] public string link;

            private void OpenUrl()
            {
                TryOpenUrl(link, this);
            }
        }
        
        [ContextMenu("Open Url")]
        public void OpenUrl()
        {
            TryOpenUrl(value.link, this);   
        }

        private static void TryOpenUrl(string url, object context = null)
        {
            try
            {
                Application.OpenURL(url);
            }
            catch (Exception e)
            {
                Object debug = context as Object;
                string logName = StringUtils.LogName(context);
                Debug.LogError(logName+e.Message, debug);
            }
        }
    }
}
