using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace CippSharp.Core.Containers
{
    public class PrefabTemplateReferenceAttribute : PropertyAttribute
    {
        public PrefabTemplateReferenceAttribute()
        {
            
        }

        #region Custom Editor
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(PrefabTemplateReferenceAttribute), true)]
        public class PaletteColorDrawer : PropertyDrawer
        {
            private static List<string> Keys = new List<string>();

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                if (ArrayUtils.IsNullOrEmpty(Keys))
                {
                    Keys = TemplatesHolderAsset.GetKeys();
                }
                
                if (property.propertyType == SerializedPropertyType.String)
                {
                    EditorGUI.BeginProperty(position, label, property);
                    EditorGUIUtils.DrawOptionsPopUpForStringProperty(position, label.text, property, Keys);
                    EditorGUI.EndProperty();
                }
                else
                {
                    EditorGUI.PropertyField(position, property, label);
                }
            }
        }
#endif
        #endregion
    }
}
