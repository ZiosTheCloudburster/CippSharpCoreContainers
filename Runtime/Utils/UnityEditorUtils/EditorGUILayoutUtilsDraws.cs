#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Containers
{
    public static partial class EditorGUILayoutUtils
    {
	    #region Draw Button

	    /// <summary>
	    /// Draws a minibutton that calls, if pressed, the action.
	    /// </summary>
	    /// <param name="name"></param>
	    /// <param name="action"></param>
	    public static void DrawMiniButton(string name, Action action)
	    {
		    if (GUILayout.Button(name, EditorStyles.miniButton))
		    {
			    action.Invoke();
		    }
	    }

	    #endregion

	    #region Draw Serialized Object Infos
		
		/// <summary>
        /// It help to draw easily infos of a class in custom editors.
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="localIdentfierInFile"></param>
        public static void DrawSerializedObjectData(SerializedObject serializedObject, int localIdentfierInFile = 0)
        {
            EditorGUILayout.BeginVertical();
	        bool enabled = GUI.enabled;
	        GUI.enabled = false;
            
            DrawLocalIdentifierInFile(serializedObject, localIdentfierInFile);
	        DrawTargetInstanceID(serializedObject);
			
	        DrawScriptReferenceField(serializedObject);
            DrawTargetObjectReferenceField(serializedObject);
            
	        GUI.enabled = enabled;
            EditorGUILayout.EndVertical();
        }

		/// <summary>
		/// Draws the local identfier in file of the serialized object target.
		/// </summary>
		/// <param name="serializedObject"></param>
		/// <param name="identfier"></param>
		public static void DrawLocalIdentifierInFile(SerializedObject serializedObject, int identfier = 0)
		{
			bool enabled = GUI.enabled;
			GUI.enabled = false;
			Object targetObject = serializedObject.targetObject;
			ulong showedIdentfier = 0;
#if UNITY_2019_2_OR_NEWER
			showedIdentfier = (identfier != 0) ? (ulong)identfier : Unsupported.GetLocalIdentifierInFileForPersistentObject(targetObject);
#else
			showedIdentfier = (ulong)((identfier != 0) ? identfier : Unsupported.GetLocalIdentifierInFile(targetObject.GetInstanceID()));
#endif
			EditorGUILayout.LabelField(identfierLabelValue, showedIdentfier.ToString());
			GUI.enabled = enabled;
		}
		
		/// <summary>
		/// Draw the instance id of the serialized object target.
		/// </summary>
		/// <param name="serializedObject"></param>
		public static void DrawTargetInstanceID(SerializedObject serializedObject)
		{
			bool enabled = GUI.enabled;
			GUI.enabled = false;
			int instanceID = serializedObject.targetObject.GetInstanceID();
			EditorGUILayout.LabelField(instanceIdLabelValue, instanceID.ToString());      
			GUI.enabled = enabled;
		}

		/// <summary>
		/// Draw a reference to monoscript asset.
		/// </summary>
		/// <param name="serializedObject"></param>
		public static void DrawScriptReferenceField(SerializedObject serializedObject)
		{
			if (serializedObject == null)
			{
				return;
			}
			
			bool enabled = GUI.enabled;
			GUI.enabled = false;
			SerializedProperty m_Script = serializedObject.FindProperty(EditorConstants.ScriptSerializedPropertyName);
			if (m_Script != null)
			{
				EditorGUILayout.PropertyField(m_Script, m_Script.isExpanded && m_Script.hasChildren);
			}
			GUI.enabled = enabled;
		}
		
		/// <summary>
		/// Draw a reference to self: it's useful to navigate different window and ping again the same object.
		/// </summary>
		/// <param name="serializedObject"></param>
		public static void DrawTargetObjectReferenceField(SerializedObject serializedObject)
		{	
			bool enabled = GUI.enabled;
			GUI.enabled = false;
			EditorGUILayout.ObjectField(selfLabelValue, serializedObject.targetObject, typeof(Object), true);
			GUI.enabled = enabled;
		}
		
		#endregion
    }
}
#endif
