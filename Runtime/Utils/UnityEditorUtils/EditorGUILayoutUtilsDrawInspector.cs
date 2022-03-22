#if UNITY_EDITOR
using System;
using UnityEditor;

namespace CippSharp.Core.Containers
{
    public static partial class EditorGUILayoutUtils
    {
        #region Draw Inspector

        [Obsolete("2021/08/14 → Use DrawInspector instead. This will be removed in future versions.")]
        public static bool DrawCascadeInspector(SerializedObject serializedObject, DrawSerializedPropertyDelegate drawPropertyDelegate)
        {
            return DrawInspector(serializedObject, drawPropertyDelegate);
        }

        /// <summary>
        /// Foreach element (<see cref="SerializedProperty"/>) found in the <param name="serializedObject"></param> iterator,
        /// this will invoke a callback where you can override the draw of each or of some properties.
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="drawPropertyDelegate"></param>
        /// <returns></returns>
        public static bool DrawInspector(SerializedObject serializedObject, DrawSerializedPropertyDelegate drawPropertyDelegate)
        {
            EditorGUI.BeginChangeCheck();
            serializedObject.UpdateIfRequiredOrScript();
            SerializedProperty iterator = serializedObject.GetIterator();
            for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                using (new EditorGUI.DisabledScope(Constants.ScriptSerializedPropertyName == iterator.propertyPath))
                {
                    drawPropertyDelegate.Invoke(iterator.Copy());
                }
            }
            serializedObject.ApplyModifiedProperties();
            return EditorGUI.EndChangeCheck();
        }

        #endregion
    }
}
#endif
