#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using CippSharp.Core;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    public static partial class SerializedPropertyUtils
    {
        #region Get Property Backing Field Name

        /// <summary>
        /// Retrieve property backing field name;
        /// </summary>
        /// <param name="originalPropertyName">of a property exposed with [field:]</param>
        /// <returns></returns>
        public static string GetPropertyBackingFieldName(string originalPropertyName)
        {
            return $"<{originalPropertyName}>{k_BackingField}";
        }

        /// <summary>
        /// Retrieve property original name;
        /// </summary>
        /// <param name="backingFieldName">of a property exposed with [field:]</param>
        /// <returns></returns>
        public static string GetPropertyNameFromPropertyBackingFieldName(string backingFieldName)
        {
            return backingFieldName.TrimStart(new[] {'<'}).Replace(k_BackingField, string.Empty).TrimEnd(new[] {'>'});
        }
		
        #endregion
        
        /// <summary>
        /// It retrieves all serialized properties from <param name="serializedObject"></param> iterator.
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <returns></returns>
        public static SerializedProperty[] GetAllProperties(SerializedObject serializedObject)
        {
            if (serializedObject == null)
            {
                Debug.LogError("Passed "+nameof(serializedObject)+" is null!");
                return null;
            }
		    
            List<SerializedProperty> properties = new List<SerializedProperty>();
            SerializedProperty iterator = serializedObject.GetIterator();
            for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                using (new EditorGUI.DisabledScope(EditorConstants.ScriptSerializedPropertyName == iterator.propertyPath))
                {
                    properties.Add(iterator.Copy());
                }
            }
            return properties.ToArray();
        }
        
        /// <summary>
        /// Retrieve a serialized property that is an array as an array of serialized properties.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static SerializedProperty[] ToArray(SerializedProperty property)
        {
            if (property == null)
            {
                Debug.LogError(PropertyIsNullError);
                return null;
            }
			
            if (!property.isArray)
            {
                Debug.LogError(PropertyIsNotArrayError);
                return null;
            }

            List<SerializedProperty> elements = new List<SerializedProperty>();
            for (int i = 0; i < property.arraySize; i++)
            {
                elements.Add(property.GetArrayElementAtIndex(i));
            }

            return elements.ToArray();
        }
        
//        /// <summary>
//        /// Retrieve a brother property of the interested one.
//        /// </summary>
//        /// <param name="property"></param>
//        /// <param name="brotherPropertyName"></param>
//        /// <returns></returns>
//        public static SerializedProperty FindBrotherProperty(SerializedProperty property, string brotherPropertyName)
//        {
//            try
//            {
//                string propertyPath = property.propertyPath;
//                SerializedObject serializedObject = property.serializedObject;
//                propertyPath = StringUtils.ReplaceLastOccurrence(propertyPath, property.name, brotherPropertyName);
//                return serializedObject.FindProperty(propertyPath);
//            }
//            catch (Exception e)
//            {
//                Debug.LogError(e.Message);
//                return null;
//            }
//        }
        
        
        
    }
}
#endif
