#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    /// <summary>
    ///
    /// 2021/08/14 → Added 'by refs' methods to reduce memory heaps
    /// and sometimes to edit variables
    /// </summary>
    public static partial class EditorGUIUtils
    {
        /// <summary>
        /// Draws an isExpanded foldout for the passed property.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="rect">the edited position value</param>
        public static void DrawFoldout(Rect position, SerializedProperty property, out Rect rect)
        {
            rect = position;
            rect.height = SingleLineHeight;
            property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, property.displayName);
            rect.y += LineHeight;
        }

        /// <summary>
        /// Draws an isExpanded foldout for the passed property.
        /// </summary>
        /// <param name="position">will be edited on height to SingleLineHeight and in Y by adding the LineHeight</param>
        /// <param name="property"></param>
        public static void DrawFoldout(ref Rect position, SerializedProperty property)
        {
            position.height = SingleLineHeight;
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, property.displayName);
            position.y += LineHeight;
        }
    }
}
#endif