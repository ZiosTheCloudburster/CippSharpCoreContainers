using System;
using UnityEngine;

#if UNITY_EDITOR
using CippSharp.Core;
using CippSharpEditor.Core.Extensions;
using UnityEditor;
#endif

namespace CippSharp.Core
{
	[CreateAssetMenu(menuName = nameof(CippSharp)+"/Data Assets/Link Holder Asset")]
	public class LinkHolderAsset : ScriptableObject
	{
		[Header(Constants.Settings+":")]
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
	}
}

#if UNITY_EDITOR
namespace CippSharpEditor.Core 
{
	[CustomEditor(typeof(LinkHolderAsset))]
	public class LinkHolderAssetEditor : Editor
	{
		private int identfier;
		private LinkHolderAsset linkHolderAsset;
		private SerializedProperty ser_description;
		private SerializedProperty ser_link;

		private void OnEnable()
		{
			linkHolderAsset = (LinkHolderAsset) target;
			identfier = linkHolderAsset.GetLocalIdentfierInFile();
			ser_description = serializedObject.FindProperty("description");
			ser_link = serializedObject.FindProperty("link");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.DrawSerializedObjectData(identfier);
			serializedObject.Update();
			EditorGUILayout.PropertyField(ser_description);
			EditorGUILayout.PropertyField(ser_link);
			EditorGUILayoutUtils.DrawMiniButton("Open Url", linkHolderAsset.OpenUrl);
			serializedObject.ApplyModifiedProperties();
		}
	}
}
#endif
