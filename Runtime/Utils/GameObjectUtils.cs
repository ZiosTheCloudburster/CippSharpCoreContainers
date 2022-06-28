using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Containers
{
    internal static class GameObjectUtils
    {
        #region → Cast

        /// <summary>
        /// Retrieve the first gameObject component of type T.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T As<T> (GameObject gameObject)
        {
            return gameObject.GetComponent<T>();
        }
         
        /// <summary>
        /// Generic Object as GameObject
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static GameObject From(Object target)
        {
            switch (target)
            {
                case GameObject g:
                    return g;
                case Component c:
                    return c.gameObject;
                default:
                    return null;
            }
        }

        #endregion
      
    }
}
