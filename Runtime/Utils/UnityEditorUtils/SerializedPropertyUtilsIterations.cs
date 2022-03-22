#if UNITY_EDITOR
using UnityEditor;

namespace CippSharp.Core.Containers
{
    public static partial class SerializedPropertyUtils
    {
        #region Iterations
        
        /// <summary>
        /// Invokes a callback during a for iteration of a serialized property array.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="callback"></param>
        public static void For(SerializedProperty[] array, ForSerializedPropertyAction callback)
        {
            for (int i = 0; i < array.Length; i++)
            {
                callback.Invoke(array[i], i);
            }
        }
        
        #endregion
    }
}
#endif
