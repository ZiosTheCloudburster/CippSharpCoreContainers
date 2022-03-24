#if UNITY_EDITOR
using UnityEditor;

namespace CippSharp.Core.Containers
{
    public static partial class EditorGUILayoutUtils
    {
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
                using (new EditorGUI.DisabledScope(EditorConstants.ScriptSerializedPropertyName == iterator.propertyPath))
                {
                    drawPropertyDelegate.Invoke(iterator.Copy());
                }
            }
            serializedObject.ApplyModifiedProperties();
            return EditorGUI.EndChangeCheck();
        }

    }
}
#endif
