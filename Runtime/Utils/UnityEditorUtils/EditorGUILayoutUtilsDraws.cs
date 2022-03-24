#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Containers
{
    public static partial class EditorGUILayoutUtils
    {
	    #region Draw Array

	    /// <summary>
	    /// Draws an array only if it isn't null or empty.
	    /// </summary>
	    /// <param name="arrayProperty"></param>
	    public static void DrawArrayIfNotEmpty(SerializedProperty arrayProperty)
	    {
		    if (arrayProperty != null && arrayProperty.isArray && arrayProperty.arraySize > 0)
		    {
			    EditorGUILayout.PropertyField(arrayProperty, arrayProperty.isExpanded && arrayProperty.hasChildren);
		    }
	    }

	    /// <summary>
	    /// Draws a readonly array only if it isn't null or empty.
	    /// </summary>
	    /// <param name="arrayProperty"></param>
	    public static void DrawNotEditableArrayIfNotEmpty(SerializedProperty arrayProperty)
	    {
		    bool enabled = GUI.enabled;
		    GUI.enabled = false;
		    if (arrayProperty != null && arrayProperty.isArray && arrayProperty.arraySize > 0)
		    {
			    EditorGUILayout.PropertyField(arrayProperty, arrayProperty.isExpanded && arrayProperty.hasChildren);
		    }
		    GUI.enabled = enabled;
	    }
	    
	    #endregion
	    
        #region Draw Array Page
        
        /// <summary>
		/// Display array per pages to avoid high usage to show all elements at once.
		/// Return the index of displayed page.
		/// </summary>
		/// <param name="label"></param>
		/// <param name="currentPage">.</param>
		/// <param name="arrayProperty"></param>
		/// <param name="elementsPerPage"></param>
		/// <param name="readOnly"></param>
		public static int DrawArrayPage(string label, int currentPage, SerializedProperty arrayProperty, int elementsPerPage = 10, bool readOnly = true)
		{
			if (arrayProperty == null || !arrayProperty.isArray)
			{
				Debug.LogError(PropertyIsNotArrayError);
				return currentPage;
			}

			if (arrayProperty.isArray && arrayProperty.arraySize < 1)
			{
				Debug.LogWarning(PropertyIsNotValidArrayWarning);
				return currentPage;
			}
			
			GUILayout.Space(15);
			arrayProperty.isExpanded = EditorGUILayout.Foldout(arrayProperty.isExpanded, label, EditorStyles.foldout);

			if (!arrayProperty.isExpanded)
			{
				return currentPage;
			}
			
			int pagesLength = (Mathf.CeilToInt((float) arrayProperty.arraySize / (float) elementsPerPage))-1;
			if (currentPage > pagesLength)
			{
				Debug.LogWarning("Pages index out of range! Last page will be drawn instead.");
			}
			
			EditorGUI.indentLevel++;
			int pagesIndex = Mathf.Clamp(currentPage, 0, pagesLength);
			pagesIndex = EditorGUILayout.IntSlider(pagesIndex, 0, pagesLength);
			
			EditorGUILayout.LabelField($"Displaying page: {pagesIndex.ToString()}/{pagesLength.ToString()}.");
			EditorGUI.indentLevel++;
			bool guiEnabled = GUI.enabled;
			GUI.enabled = !readOnly;
			int startingArrayElementsIndex = pagesIndex * elementsPerPage;
			int displayedElementsCount = 0;
			for (int i = 0; i < elementsPerPage; i++)
			{
				int arrayElementIndex = startingArrayElementsIndex + i;
				if (arrayElementIndex >= arrayProperty.arraySize)
				{
					continue;
				}
				
				SerializedProperty element = arrayProperty.GetArrayElementAtIndex(arrayElementIndex);
				DrawProperty(element);
				displayedElementsCount++;
			}
			GUI.enabled = guiEnabled;
			
			EditorGUILayout.LabelField($"Displaying elements: {displayedElementsCount.ToString()}/{elementsPerPage.ToString()}");
			
			EditorGUI.indentLevel--;
			EditorGUI.indentLevel--;
			return pagesIndex;
		}
        
        #endregion

	    #region Draw Button

	    /// <summary>
	    /// Draws a minibutton
	    /// </summary>
	    /// <param name="name"></param>
	    public static void DrawMiniButton(string name)
	    {
		    if (GUILayout.Button(name, EditorStyles.miniButton))
		    {
			    
		    }
	    }

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
        
	    #region Draw Drag 'n' Drop Area
	
	    /// <summary>
	    /// Display a drag 'n' drop area with a callback 
	    /// </summary>
	    /// <param name="hint"></param>
	    /// <param name="onDrag">for drag 'n' dropped objects</param>
	    /// <param name="width"></param>
	    /// <param name="height"></param>
	    public static void DragNDropArea(string hint, Action<Object[]> onDrag, float width = 0, float height = 20)
	    {
		    GUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
		    Rect rect = GUILayoutUtility.GetRect(width, height, GUILayout.ExpandWidth(true));
		    GUI.Box(rect, hint, EditorStyles.miniButton);
		    if (rect.Contains(Event.current.mousePosition))
		    {
			    if (Event.current.type == EventType.DragUpdated)
			    {
				    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
				    Event.current.Use ();
			    }   
			    else if (Event.current.type == EventType.DragPerform)
			    {
				    Object[] objects = DragAndDrop.objectReferences;
				    onDrag.Invoke(objects);
				    Event.current.Use ();
			    }
		    }
		    GUILayout.Space(EditorGUIUtility.standardVerticalSpacing);
	    }
		
	    /// <summary>
	    /// Display a drag 'n' drop area with onDragPerform callback 
	    /// </summary>
	    /// <param name="onDrag"></param>
	    /// <param name="height"></param>
	    public static void DragNDropArea(Action onDrag, float height = 45)
	    {
		    Rect rect = GUILayoutUtility.GetRect(0, height, GUILayout.ExpandWidth(true));
		    GUI.Box(rect,"Drag and Drop files to this Box!");
		    if (rect.Contains(Event.current.mousePosition))
		    {
			    if (Event.current.type == EventType.DragUpdated)
			    {
				    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
				    Event.current.Use ();
			    }   
			    else if (Event.current.type == EventType.DragPerform)
			    {
				    onDrag.Invoke();
				    Event.current.Use ();
			    }
		    }
		    GUILayout.Space(5);
	    }
		
	    #endregion
	    
	    #region Draw Enum

	    /// <summary>
	    /// Draws an enum.
	    /// </summary>
	    /// <param name="displayedName"></param>
	    /// <param name="enum"></param>
	    /// <returns></returns>
	    public static int DrawEnum(string displayedName, Enum @enum)
	    {
		    return Convert.ToInt32(EditorGUILayout.EnumPopup(displayedName, @enum, EditorStyles.popup));
	    }
	    
	    /// <summary>
	    /// Draws an enum.
	    /// </summary>
	    /// <param name="displayedName"></param>
	    /// <param name="enum"></param>
	    /// <returns></returns>
	    public static int DrawEnum(ref string displayedName, ref Enum @enum)
	    {
		    return Convert.ToInt32(EditorGUILayout.EnumPopup(displayedName, @enum, EditorStyles.popup));
	    }

	    #endregion
	    
	    #region Draw Labels

		/// <summary>
		/// Draw an help box with the passed text.
		/// </summary>
		/// <param name="text"></param>
		public static void DrawHelpBox(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				EditorGUILayout.HelpBox(text, MessageType.Info);
			}
		}
		
		/// <summary>
		/// Draw a warning box with the passed text.
		/// </summary>
		/// <param name="text"></param>
		public static void DrawWarningBox(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				EditorGUILayout.HelpBox(text, MessageType.Warning);
			}
		}

		/// <summary>
		/// Draw an error box with the passed text.
		/// </summary>
		/// <param name="text"></param>
		public static void DrawErrorBox(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				EditorGUILayout.HelpBox(text, MessageType.Error);
			}
		}

		/// <summary>
		/// Draw a label with the passed text
		/// </summary>
		/// <param name="text"></param>
		public static void DrawLabel(string text)
		{
			if (!string.IsNullOrEmpty(text))
			{
				EditorGUILayout.LabelField(new GUIContent(text), EditorStyles.label);
			}
		}

		/// <summary>
		/// Draw a not editable label with a purpose text inside
		/// </summary>
		/// <param name="property"></param>
		public static void DrawLabel(SerializedProperty property)
		{
			if (property.propertyType != SerializedPropertyType.String)
			{
				return;
			}
			
			DrawNotEditableProperty(property);
		}

		/// <summary>
		/// Draw a label with the passed text
		/// </summary>
		/// <param name="text"></param>
		/// <param name="space"></param>
		public static void DrawHeader(string text, int space = 3)
		{
			if (!string.IsNullOrEmpty(text))
			{
				GUILayout.Space(space);
				EditorGUILayout.LabelField(new GUIContent(text), EditorStyles.boldLabel);
			}
		}

		/// <summary>
		/// Draw an header and then a foldout with the same text.
		/// </summary>
		/// <param name="text"></param>
		/// <param name="foldout"></param>
		/// <param name="space"></param>
		/// <returns></returns>
		public static bool DrawHeaderWithFoldout(string text, bool foldout, int space = 10)
		{
			DrawHeader(text, space);
			return EditorGUILayout.Foldout(foldout, text);
		}
		
		#endregion

	    #region Draw Object Field
		
	    /// <summary>
	    /// It draws the property only if its different from null in a not-editable way.
	    /// </summary>
	    /// <param name="displayedName"></param>
	    /// <param name="target"></param>
	    public static void DrawNotEditableObjectField(string displayedName, Object target)
	    {	
		    bool guiEnabled = GUI.enabled;
		    GUI.enabled = false;
		    EditorGUILayout.ObjectField(displayedName, target, typeof(Object), true);
		    GUI.enabled = guiEnabled;
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

	    #region Draw Script Reference Field
	    
	    /// <summary>
	    /// Draw a reference to the editor monoscript asset.
	    /// </summary>
	    /// <param name="customEditor"></param>
	    public static void DrawScriptReferenceField(Editor customEditor)
	    {
		    bool enabled = GUI.enabled;
		    GUI.enabled = false;
		    SerializedProperty m_Script = new SerializedObject(customEditor).FindProperty(EditorConstants.ScriptSerializedPropertyName);
		    EditorGUILayout.PropertyField(m_Script, new GUIContent(nameof(Editor)), m_Script.hasChildren);
		    GUI.enabled = enabled;
	    }

	    #endregion

	    #region Draw Texture

	    /// <summary>
	    /// Draws a texture
	    /// </summary>
	    /// <param name="texture"></param>
	    public static void DrawTexture(Texture texture)
	    {
		    if (texture == null)
		    {
			    return;
		    }
			
		    Rect rect = GUILayoutUtility.GetRect(texture.width*0.5f, texture.height*0.5f);
		    GUI.DrawTexture(rect, texture);
	    }

	    #endregion
    }
}
#endif
