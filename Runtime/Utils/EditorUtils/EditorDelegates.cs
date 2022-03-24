#if UNITY_EDITOR
using UnityEditor;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Reference object callback.
    /// </summary>
    /// <param name="context"></param>
    internal delegate void GenericRefAction(ref object context);
    
    /// <summary>
    /// Custom Delegate to draw a serialized property
    /// </summary>
    /// <param name="property"></param>
    internal delegate void DrawSerializedPropertyDelegate(SerializedProperty property);
}
#endif
