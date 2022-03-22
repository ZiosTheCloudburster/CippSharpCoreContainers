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
					SerializedProperty localIdProp =serializedObject.FindProperty(Constants.LocalIdentfierInFilePropertyName);
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

		#region Get Property Backing Field Name

		/// <summary>
		/// Retrieve property backing field name;
		/// </summary>
		/// <param name="originalPropertyName">of a property exposed with [field:]</param>
		/// <returns></returns>
		[Obsolete("2021/12/03 → Use SerializedPropertyUtils.GetPropertyBackingFieldName instead.")]
		public static string GetPropertyBackingFieldName(string originalPropertyName)
		{
			return SerializedPropertyUtils.GetPropertyBackingFieldName(originalPropertyName);
		}

		/// <summary>
		/// Retrieve property original name;
		/// </summary>
		/// <param name="backingFieldName">of a property exposed with [field:]</param>
		/// <returns></returns>
		[Obsolete("2021/12/03 → Use SerializedPropertyUtils.GetPropertyNameFromPropertyBackingFieldName instead.")]
		public static string GetPropertyNameFromPropertyBackingFieldName(string backingFieldName)
		{
			return SerializedPropertyUtils.GetPropertyNameFromPropertyBackingFieldName(backingFieldName);
		}
		
		#endregion
	
		/// <summary>
		/// Draws an enum. It returns the int value of a property.
		/// </summary>
		/// <param name="displayedName"></param>
		/// <param name="enum"></param>
		/// <returns></returns>
		[Obsolete("2021/08/14 → Use Draw Enum instead. This will be removed in future versions.")]
		public static int DrawEnumField(string displayedName, Enum @enum)
		{
			return DrawEnum(ref displayedName, ref @enum);
		}
	}
}
#endif
