#if UNITY_EDITOR
//
// Author: Alessandro Salani (Cippo)
//
using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Containers
{
	public static partial class EditorGUILayoutUtils
	{
		public const string inspectorModePropertyName = "inspectorMode";
		public const string instanceIdLabelValue = "Instance ID";
		public const string identfierLabelValue = "Local Identfier in File";
		public const string selfLabelValue = "Self";
		public const string k_BackingField = SerializedPropertyUtils.k_BackingField;

		private const string PropertyIsNotArrayError = "Property isn't an array.";
		private const string PropertyIsNotValidArrayWarning = "Property isn't a valid array.";
		
		#region Get Local Identfier In File
		
		/// <summary>
		/// WARNING: This works only in a custom editor OnEnable(). You must cache it if you want to display it.
		/// It retrieves the m_LocalIdentfierInFile of an Object.
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public static int GetLocalIdentfierInFile(Object target)
		{
			try
			{
				if (EditorUtility.IsPersistent(target))
				{
					PropertyInfo inspectorModeInfo = typeof(SerializedObject).GetProperty(inspectorModePropertyName, BindingFlags.NonPublic | BindingFlags.Instance);
					SerializedObject serializedObject = new SerializedObject(target);
					inspectorModeInfo.SetValue(serializedObject, InspectorMode.Debug, null);
					SerializedProperty localIdProp =serializedObject.FindProperty(EditorConstants.LocalIdentfierInFilePropertyName);
					return localIdProp.intValue;
				}
				else
				{
					return 0;
				}
			}
			catch (Exception e)
			{
				Debug.LogError(e.Message);
				return 0;
			}
		}
		
		#endregion
		
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
