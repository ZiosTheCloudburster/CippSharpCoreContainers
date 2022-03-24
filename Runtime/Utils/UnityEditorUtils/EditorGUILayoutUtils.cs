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
	}
}
#endif
