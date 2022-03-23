#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.Containers.Samples
{
    /// <summary>
    /// <see cref="ContainersSamples"/> holds some containers of custom data.
    ///
    /// Containers are also good to keep custom inspector and custom property drawers properties without affecting the code too.
    /// They're just 'boxes'.
    ///
    /// For Example: you have other custom drawers for top inspected property as 'NotEditableAttribute/ReadOnlyAttribute'
    /// that conflicts with your property drawer, don't overthink about it. Just put your data in one container.
    /// 
    /// </summary>
    internal class ContainersSamples : MonoBehaviour
    {
        [Serializable]
        public struct CustomData
        {
#pragma warning disable 649
            public float a;
            public float b;
            public float c;
#pragma warning restore 649

            [CustomPropertyDrawer(typeof(CustomData))]
            internal class ContainersSamplesDrawer : PropertyDrawer
            {
                public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
                {
                    return EditorGUIUtils.GetPropertyHeight(property);
                }

                public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
                {
                    Rect r = position;
                    EditorGUIUtils.DrawFoldout(ref r, property);
                    if (property.isExpanded)
                    {
                        EditorGUI.indentLevel++;
                        
                        r = EditorGUI.IndentedRect(r);

                        var bkgColorGUI = GUI.backgroundColor;
                        GUI.backgroundColor = Color.red;
                        EditorGUI.PropertyField(r, property.FindPropertyRelative(nameof(a)));
                        r.y += EditorGUIUtils.LineHeight;
                        
                        GUI.backgroundColor = Color.cyan;
                        EditorGUI.PropertyField(r, property.FindPropertyRelative(nameof(b)));
                        r.y += EditorGUIUtils.LineHeight;
                        
                        GUI.backgroundColor = Color.green;
                        EditorGUI.PropertyField(r, property.FindPropertyRelative(nameof(c)));

                        GUI.backgroundColor = bkgColorGUI;
                        EditorGUI.indentLevel--;
                    }
                }
            }
        }
        
        [Serializable]
        public class CustomDataContainer : Container<CustomData>
        {
            
        }
        
        [Serializable]
        public class CustomDataListContainer : Container<List<CustomData>>
        {
            
        }
        
        [Serializable]
        public class SpecificCustomDataListContainer : AListContainer<CustomData>
        {
            
        }

        [Header("Settings:")]
        //[CustomAttributeDrawer]
        public CustomData data0 = new CustomData();
        public CustomDataContainer dataContainer0 = new CustomDataContainer();
        public CustomDataListContainer dataListContainer0 = new CustomDataListContainer();
        public SpecificCustomDataListContainer specificDataListContainer0 = new SpecificCustomDataListContainer();
    }
}
#endif