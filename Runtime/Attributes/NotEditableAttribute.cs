using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Disable GUI on current property
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    internal class NotEditableAttribute : PropertyAttribute
    {
        #region Custom Editor
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(NotEditableAttribute))]
        internal class NotEditableDrawer : PropertyDrawer
        {
            public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            {
                return EditorGUIUtils.GetPropertyHeight(property, label);
            }

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                EditorGUIUtils.DrawNotEditableProperty(position, property, label);
            }
        }
#endif
        #endregion
    }
}
