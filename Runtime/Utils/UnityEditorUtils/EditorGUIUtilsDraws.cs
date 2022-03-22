#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    /// <summary>
    ///
    /// 20201/08/14 → Added 'by refs' methods to reduce memory heaps
    /// and sometimes to edit variables
    /// </summary>
    public static partial class EditorGUIUtils
    {
        #region Draw Button

        /// <summary>
        /// Draws a button
        /// </summary>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <param name="style"></param>
        public static void DrawButton(Rect position, string name, GUIStyle style = null)
        {
            DrawButton(ref position, ref name);
        }

        /// <summary>
        /// Ref draws a button
        /// </summary>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <param name="style"></param>
        public static void DrawButton(ref Rect position, ref string name, GUIStyle style = null)
        {
            if (style == null)
            {
                if (GUI.Button(position, name))
                {
                    
                }
            }
            else
            {
                if (GUI.Button(position, name, style))
                {
                    
                }
            }
        }

        /// <summary>
        /// Draws a button with callback
        /// </summary>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <param name="clickCallback"></param>
        /// <param name="style"></param>
        public static void DrawButtonWithCallback(Rect position, string name, Action clickCallback, GUIStyle style = null)
        {
            DrawButtonWithCallback(ref position, name, clickCallback, style);
        }

        /// <summary>
        /// Ref Draws a Button with callback
        /// </summary>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <param name="clickCallback"></param>
        /// <param name="style"></param>
        public static void DrawButtonWithCallback(ref Rect position, string name, Action clickCallback, GUIStyle style = null)
        {
            if (style == null)
            {
                if (GUI.Button(position, name))
                {
                    clickCallback.Invoke();
                }
            }
            else
            {
                if (GUI.Button(position, name, style))
                {
                    clickCallback.Invoke();
                }
            }
        }

        /// <summary>
        /// Draws a Button with callbacks
        /// </summary>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <param name="clickCallback"></param>
        /// <param name="notClickedCallback"></param>
        /// <param name="style"></param>
        public static void DrawButtonWithCallback(Rect position, string name, Action clickCallback, Action notClickedCallback, GUIStyle style = null)
        {
            DrawButtonWithCallback(ref position, name, clickCallback, notClickedCallback, style);
        }
        
        /// <summary>
        /// Ref draws a Button with callbacks
        /// </summary>
        /// <param name="position"></param>
        /// <param name="name"></param>
        /// <param name="clickCallback"></param>
        /// <param name="notClickedCallback"></param>
        /// <param name="style"></param>
        public static void DrawButtonWithCallback(ref Rect position, string name, Action clickCallback, Action notClickedCallback, GUIStyle style = null)
        {
            if (style == null)
            {
                if (GUI.Button(position, name))
                {
                    clickCallback.Invoke();
                }
                else
                {
                    notClickedCallback.Invoke();
                }
            }
            else
            {
                if (GUI.Button(position, name, style))
                {
                    clickCallback.Invoke();
                }
                else
                {
                    notClickedCallback.Invoke();
                }
            }
        }

        #endregion

        #region Draw Foldout
        
        /// <summary>
        /// Draws an isExpanded foldout for the passed property.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="property"></param>
        public static void DrawFoldout(Rect position, SerializedProperty property)
        {
            DrawFoldout(ref position, property);
        }

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

        #endregion
        
        #region Draw Help Box

        /// <summary>
        /// The height of an help box based on his text message.
        /// </summary>
        /// <param name="helpBoxMessage"></param>
        /// <returns></returns>
        public static float GetHelpBoxHeight(string helpBoxMessage)
        {
            return GetHelpBoxHeight(ref helpBoxMessage);
        }
        
        /// <summary>
        /// The height of an help box based on his text message.
        /// </summary>
        /// <param name="helpBoxMessage"></param>
        /// <returns></returns>
        public static float GetHelpBoxHeight(ref string helpBoxMessage)
        {
            GUIStyle style = EditorStyles.helpBox;
            GUIContent descriptionWrapper = new GUIContent(helpBoxMessage);
            return style.CalcHeight(descriptionWrapper, Screen.width);
        }
        
        /// <summary>
        /// Draw an help box with the passed rect and text.
        /// It doesn't matter about his height/resizing..
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="text"></param>
        /// <param name="messageType"></param>
        public static void DrawHelpBox(Rect rect, string text, MessageType messageType = MessageType.Info)
        {
            EditorGUI.HelpBox(rect, text, messageType);
        }
        
        /// <summary>
        /// Draw an help box with the passed rect and text.
        /// It doesn't matter about his height/resizing..
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="text"></param>
        /// <param name="messageType"></param>
        public static void DrawHelpBox(ref Rect rect, ref string text, ref MessageType messageType)
        {
            EditorGUI.HelpBox(rect, text, messageType);
        }

        /// <summary>
        /// Draws an help box with the passed rect and text.
        /// </summary>
        /// <param name="inputRect"></param>
        /// <param name="helpBoxMessage"></param>
        /// <param name="textHeight">The computed height of the description.</param>
        /// <param name="messageType"></param>
        public static void DrawHelpBox(Rect inputRect, string helpBoxMessage, out float textHeight, MessageType messageType = MessageType.Info)
        {
            DrawHelpBox(ref inputRect, ref helpBoxMessage, out textHeight, ref messageType);
        }

        /// <summary>
        /// Draws an help box with the passed rect and text.
        /// </summary>
        /// <param name="inputRect"></param>
        /// <param name="helpBoxMessage"></param>
        /// <param name="textHeight">The computed height of the description.</param>
        /// <param name="messageType"></param>
        public static void DrawHelpBox(ref Rect inputRect, ref string helpBoxMessage, out float textHeight, ref MessageType messageType)
        {
            GUIStyle style = EditorStyles.helpBox;
            GUIContent descriptionWrapper = new GUIContent(helpBoxMessage);
            textHeight = style.CalcHeight(descriptionWrapper, inputRect.width);
            inputRect.height = textHeight;
            inputRect.y += LineHeight;
            DrawHelpBox(ref inputRect, ref helpBoxMessage, ref messageType);
            inputRect.y += textHeight;
        }

        #endregion
        
        #region Draw Labels
        
        /// <summary>
        /// Draw a label with the passed text
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="text">doesn't draw if text is null or empty</param>
        public static void DrawHeader(Rect rect, string text)
        {
            DrawHeader(ref rect, ref text);
        }

        /// <summary>
        /// Draw a label with the passed text
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="text">doesn't draw if text is null or empty</param>
        public static void DrawHeader(ref Rect rect, ref string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                EditorGUI.LabelField(rect, new GUIContent(text), EditorStyles.boldLabel);
            }
        }

        #endregion
    }
}
#endif