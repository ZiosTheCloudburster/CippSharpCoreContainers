using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace CippSharp.Core.Containers.Legacy
{
	/// <summary>
	/// To keep just as reference.
	/// Please use the new <see cref="UrlDataAsset"/>
	/// </summary>
	[Obsolete("Legacy Link Holder Asset. Use UrlDataAsset instead.")]
	internal class LinkHolderAsset : ScriptableObject
	{
		[Header("Settings:")]
		[TextArea(1, 10)] public string description = "";
		[Space(5)]
		[ContextMenuItem("Open Url", "OpenUrl")]
		[TextArea(1, 5)] public string link = "";

		[ContextMenu("Open Link")]
		public void OpenUrl()
		{
			try
			{
				Application.OpenURL(link);
			}
			catch (Exception e)
			{
				Debug.LogError(e.Message, this);
			}
		}
#if UNITY_EDITOR
		[CustomEditor(typeof(LinkHolderAsset))]
		internal class LinkHolderAssetEditor : Editor
		{
			private int identfier;
			private LinkHolderAsset linkHolderAsset;
			private SerializedProperty ser_description;
			private SerializedProperty ser_link;

			private void OnEnable()
			{
				linkHolderAsset = (LinkHolderAsset) target;
				identfier = EditorGUILayoutUtils.GetLocalIdentfierInFile(linkHolderAsset);
				ser_description = serializedObject.FindProperty(nameof(description));
				ser_link = serializedObject.FindProperty(nameof(link));
			}
			
			public override void OnInspectorGUI()
			{
				EditorGUILayoutUtils.DrawSerializedObjectData(serializedObject, identfier);
				serializedObject.Update();
				EditorGUILayout.PropertyField(ser_description);
				EditorGUILayout.PropertyField(ser_link);
				EditorGUILayoutUtils.DrawMiniButton("Open Url", linkHolderAsset.OpenUrl);
				serializedObject.ApplyModifiedProperties();
			}
		}
#endif
	}
}