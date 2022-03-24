#if UNITY_EDITOR
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    public static partial class EditorGUIUtils
    {
        #region Draw Property
        
//        /// <summary>
//        /// It draws the property only if it is different from null.
//        /// </summary>
//        /// <param name="rect"></param>
//        /// <param name="property"></param>
//        public static void DrawProperty(Rect rect, SerializedProperty property)
//        {
//            if (property != null)
//            {
//                EditorGUI.PropertyField(rect, property, property.isExpanded && property.hasChildren);
//            }
//        }
//        
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
        
//        /// <summary>
//        /// It draws the property only if its different from null.
//        /// </summary>
//        /// <param name="rect"></param>
//        /// <param name="property"></param>
//        /// <param name="label"></param>
//        public static void DrawProperty(Rect rect, SerializedProperty property, GUIContent label)
//        { 
//            if (property != null)
//            {
//                EditorGUI.PropertyField(rect, property, label, property.isExpanded && property.hasChildren);
//            }
//        }
        
//        /// <summary>
//        /// It draws the property only if its different from null.
//        /// </summary>
//        /// <param name="rect"></param>
//        /// <param name="property"></param>
//        /// <param name="label"></param>
//        public static void DrawProperty(ref Rect rect, SerializedProperty property, GUIContent label)
//        { 
//            if (property != null)
//            {
//                EditorGUI.PropertyField(rect, property, label, property.isExpanded && property.hasChildren);
//            }
//        }
        
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

        #region Draw Default Property

//        public static bool DrawPropertyFieldInternal(Rect position, SerializedProperty property, GUIContent label)
//        {
//
//        }

//        /// <summary>
//        /// Draw single line default property field (yes single line even if it has children)
//        /// </summary>
//        /// <param name="position"></param>
//        /// <param name="property"></param>
//        /// <param name="label"></param>
//        /// <returns></returns>
//        public static bool DrawDefaultSingleLinePropertyField(Rect position, SerializedProperty property, GUIContent label)
//        {
//            return (bool)DefaultPropertyFieldMethodInfo.Invoke(null, new object[] {position, property, label});
//        }

        
//        /// <summary>
//        /// Draw single line not editable default property field (yes single line even if it has children)
//        /// </summary>
//        /// <param name="position"></param>
//        /// <param name="property"></param>
//        /// <param name="label"></param>
//        /// <returns></returns>
//        public static bool DrawNotEditableDefaultSingleLinePropertyField(Rect position, SerializedProperty property, GUIContent label)
//        {
//            bool enabled =  GUI.enabled; 
//            GUI.enabled = false;
//            
//            bool b = (bool) DefaultPropertyFieldMethodInfo.Invoke(null, new object[] {position, property, label});
//            
//            GUI.enabled = enabled;
//            
//            return b;
//        }

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
        
//        #region Draw Property (with delegates / iterators)

        /// <summary>
        /// Draws a standard generic property from a delegate
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        /// <param name="drawSerializedPropertyDelegate"></param>
        /// <returns></returns>
        public static void DrawPropertyWithFoldout(Rect position, SerializedProperty property, DrawSerializedPropertyDelegate1 drawSerializedPropertyDelegate)
        {
            DrawFoldout(ref position, property);

            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;

                drawSerializedPropertyDelegate.Invoke(ref position, property);

                EditorGUI.indentLevel--;
            }
        }
        
//        /// <summary>
//        /// Draw by iterator
//        /// </summary>
//        /// <param name="position"></param>
//        /// <param name="property"></param>
//        public static void DrawPropertyIterator(Rect position, SerializedProperty property)
//        {
//            DrawFoldout(ref position, property);
//            if (property.isExpanded)
//            {
//                EditorGUI.indentLevel++;
//
//                IEnumerator iterator = property.GetEnumerator();
//                while (iterator.MoveNext())
//                {
//                    SerializedProperty element = (SerializedProperty) iterator.Current;
//                    position.height = GetPropertyHeight(element);
//                    DrawProperty(ref position, element);
//                    position.y += VerticalSpacing + position.height;
//                }
//
//                EditorGUI.indentLevel--;
//            }
//        }
//
//        #endregion
        
//        #region Get Property Height (with delegates / iterators)
//        
//        /// <summary>
//        /// Calculate a standard generic property height with a delegate.
//        /// </summary>
//        /// <param name="property"></param>
//        /// <param name="getPropertyHeightDelegate"></param>
//        /// <returns></returns>
//        public static float GetPropertyHeight(SerializedProperty property, GetPropertyHeightDelegate getPropertyHeightDelegate)
//        {
//            float h = LineHeight;
//            if (property.isExpanded)
//            {
//                h += getPropertyHeightDelegate.Invoke(property);
//            }
//            return h;
//        }
//
//        /// <summary>
//        /// Get Height by iterator
//        /// </summary>
//        /// <param name="property"></param>
//        /// <returns></returns>
//        public static float GetPropertyHeightIterator(SerializedProperty property)
//        {
//            float h = LineHeight;
//
//            if (property.isExpanded)
//            {
//                IEnumerator iterator = property.GetEnumerator();
//                while (iterator.MoveNext())
//                {
//                    SerializedProperty element = (SerializedProperty) iterator.Current;
//                    h += GetPropertyHeight(element);
//                    h += VerticalSpacing;
//                }
//            }
//
//            return h;
//        }
//        
//        #endregion
    }
}
#endif
