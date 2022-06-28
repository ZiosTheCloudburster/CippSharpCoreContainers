//
// Author: Alessandro Salani (Cippo)
//
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    internal static class EditorGUIUtils
    {
        /// <summary>
        /// Wrap of unity's default single line height.
        /// </summary>
        public static readonly float SingleLineHeight = EditorGUIUtility.singleLineHeight;
  
        /// <summary>
        /// Wrap of unity's default vertical spacing between lines.
        /// </summary>
        public static readonly float VerticalSpacing = EditorGUIUtility.standardVerticalSpacing;
  
        /// <summary>
        /// Sum of <see cref="SingleLineHeight"/> + <seealso cref="VerticalSpacing"/>.
        /// </summary>
        public static readonly float LineHeight = SingleLineHeight + VerticalSpacing;
        
        /// <summary>
        /// Invalid suffix for PopUps
        /// </summary>
        public const string InvalidSuffix = " (invalid)";
        

        #region Draw Property
        
        /// <summary>
        /// It draws the property only if it is different from null.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        public static void DrawProperty(ref Rect rect, SerializedProperty property)
        {
            if (property != null)
            {
                EditorGUI.PropertyField(rect, property, property.isExpanded && property.hasChildren);
            }
        }
        
        #endregion
        
        #region Draw Not Editable Property
        
        /// <summary>
        /// It draws the property only in a readonly way only if its different from null.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public static void DrawNotEditableProperty(Rect rect, SerializedProperty property, GUIContent label = null)
        {
            DrawNotEditableProperty(ref rect, property, label);
        }
        
        /// <summary>
        /// It draws the property only in a readonly way only if its different from null.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="property"></param>
        /// <param name="label"></param>
        public static void DrawNotEditableProperty(ref Rect rect, SerializedProperty property, GUIContent label = null)
        { 
            bool enabled =  GUI.enabled; 
            GUI.enabled = false;
            
            if (property != null)
            {
                if (label != null)
                {
                    EditorGUI.PropertyField(rect, property, label, property.isExpanded && property.hasChildren);
                }
                else
                {
                    EditorGUI.PropertyField(rect, property, property.isExpanded && property.hasChildren);
                }
            }
            GUI.enabled = enabled;
        }
        
        #endregion

        #region Get Property Height

        /// <summary>
        /// Retrieve the height of property's rect.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static float GetPropertyHeight(SerializedProperty property)
        {
            return EditorGUI.GetPropertyHeight(property, property.isExpanded && property.hasChildren) + VerticalSpacing;
        }

        /// <summary>
        /// Retrieve the height of property's rect.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="label"></param>
        /// <returns></returns>
        public static float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, property.isExpanded && property.hasChildren) + VerticalSpacing;;
        }

        #endregion
        
        /// <summary>
        /// Draws an isExpanded foldout for the passed property.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="rect">the edited position value</param>
        public static void DrawFoldout(Rect position, SerializedProperty property, out Rect rect)
        {
            rect = position;
            rect.height = SingleLineHeight;
            property.isExpanded = EditorGUI.Foldout(rect, property.isExpanded, property.displayName);
            rect.y += LineHeight;
        }

        /// <summary>
        /// Draws an isExpanded foldout for the passed property.
        /// </summary>
        /// <param name="position">will be edited on height to SingleLineHeight and in Y by adding the LineHeight</param>
        /// <param name="property"></param>
        public static void DrawFoldout(ref Rect position, SerializedProperty property)
        {
            position.height = SingleLineHeight;
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, property.displayName);
            position.y += LineHeight;
        }
        
        #region → Draw PopUp

        /// <summary>
        /// None Element is at options 0
        /// Draws a popup of options for the string property.
        /// Automatically calculates the index.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="stringProperty"></param>
        /// <param name="options"></param>
        public static bool DrawOptionsPopUpForStringProperty(Rect position, SerializedProperty stringProperty, List<string> options)
        {
            return DrawOptionsPopUpForStringProperty(position, stringProperty.displayName, stringProperty, options);
        }

        /// <summary>
        /// None Element is at options 0
        /// Draws a popup of options for the string property.
        /// Automatically calculates the index.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="labelText"></param>
        /// <param name="stringProperty"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool DrawOptionsPopUpForStringProperty(Rect position, string labelText, SerializedProperty stringProperty, List<string> options)
        {
            int index = -1;
            string stringValue = stringProperty.stringValue;
            if (string.IsNullOrEmpty(stringValue))
            {
                index = 0;
            }
            else if (ArrayUtils.Any(options, s => s == stringValue, out int tmpIndex))
            {
                index = tmpIndex;
            }
            else
            {
                index = options.Count;
                options.Add($"{stringValue}{InvalidSuffix}");
            }
          
            EditorGUI.BeginChangeCheck();
            index = EditorGUI.Popup(position, labelText, index, options.ToArray());
            
            if (index == 0)
            {
                stringProperty.stringValue = string.Empty;
            }
            else if (ArrayUtils.IsValidIndex(index, options))
            {
                stringProperty.stringValue = options[index].Replace(InvalidSuffix, string.Empty);
            }
            else
            {
                stringProperty.stringValue = string.Empty;
            }

            bool endCheck = EditorGUI.EndChangeCheck();
            return endCheck;
        }

        /// <summary>
        /// None Element is at options 0
        /// Draws a popup of options for the string property.
        /// Automatically calculates the index.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="label"></param>
        /// <param name="stringProperty"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool DrawOptionsPopUpForStringProperty(Rect position, GUIContent label, SerializedProperty stringProperty, List<GUIContent> options)
        {
            int index = -1;
            string stringValue = stringProperty.stringValue;
            if (string.IsNullOrEmpty(stringValue))
            {
                index = 0;
            }
            else if (ArrayUtils.Any(options, s => s.text == stringValue, out int tmpIndex))
            {
                index = tmpIndex;
            }
            else
            {
                index = options.Count;
                options.Add(new GUIContent($"{stringValue}{InvalidSuffix}"));
            }
          
            EditorGUI.BeginChangeCheck();
            index = EditorGUI.Popup(position, label, index, options.ToArray());
            
            if (index == 0)
            {
                stringProperty.stringValue = string.Empty;
            }
            else if (ArrayUtils.IsValidIndex(index, options))
            {
                stringProperty.stringValue = options[index].text.Replace(InvalidSuffix, string.Empty);
            }
            else
            {
                stringProperty.stringValue = string.Empty;
            }

            bool endCheck = EditorGUI.EndChangeCheck();
            return endCheck;
        }
        
        #endregion
    }
}
#endif
