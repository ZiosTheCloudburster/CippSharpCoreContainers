#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    public static partial class EditorGUILayoutUtils
    {
        #region Draw Property
		
		/// <summary>
		/// It draws the property only if its different from null.
		/// </summary>
		/// <param name="property"></param>
		public static void DrawProperty(SerializedProperty property)
		{
			if (property != null)
			{
				EditorGUILayout.PropertyField(property, property.isExpanded && property.hasChildren);
			}
		}
		
		/// <summary>
		/// It draws the property only if its different from null.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="label"></param>
		public static void DrawProperty(SerializedProperty property, GUIContent label)
		{
			if (property != null)
			{
				EditorGUILayout.PropertyField(property, label, property.isExpanded && property.hasChildren);
			}
		}
	
		/// <summary>
		/// It draws the property only if its different from null in a not-editable way.
		/// </summary>
		/// <param name="property"></param>
		public static void DrawNotEditableProperty(SerializedProperty property)
		{	
			bool guiEnabled;
			guiEnabled = GUI.enabled;
			GUI.enabled = false;
			if (property != null)
			{
				EditorGUILayout.PropertyField(property, property.isExpanded && property.hasChildren);
			}
			GUI.enabled = guiEnabled;
		}
		
		#endregion    
    }
}
#endif