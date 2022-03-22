#if UNITY_EDITOR
using System.Collections.Generic;
using CippSharp.Core;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    public static class SerializedObjectUtils
    {
        /// <summary>
        /// Wrap of <see cref="EditorGUILayoutUtils.DrawInspector"/>
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <param name="drawPropertyDelegate"></param>
        /// <returns></returns>
        public static bool DrawInspector(SerializedObject serializedObject, DrawSerializedPropertyDelegate drawPropertyDelegate)
        {
            return EditorGUILayoutUtils.DrawInspector(serializedObject, drawPropertyDelegate);
        }

	    /// <summary>
	    /// It retrieves all serialized properties from <param name="serializedObject"></param> iterator.
	    /// </summary>
	    /// <param name="serializedObject"></param>
	    /// <returns></returns>
	    public static SerializedProperty[] GetAllProperties(SerializedObject serializedObject)
	    {
		    return SerializedPropertyUtils.GetAllProperties(serializedObject);
	    }

	    /// <summary>
        /// Retrieve all targets objects from a <see cref="SerializedObject"/>
        /// </summary>
        /// <param name="serializedObject"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Object[] GetTargetObjects<T>(T serializedObject) where T : SerializedObject
        {
            if (serializedObject == null)
            {
                return null;
            }

            Object target = serializedObject.targetObject;
            Object[] targets = serializedObject.targetObjects;
            List<Object> allObjects = new List<Object>();
            if (target != null)
            {
                allObjects.Add(target);
            }
            
            if (ArrayUtils.IsNullOrEmpty(targets))
            {
                return allObjects.ToArray();
            }
            
            foreach (var o in targets)
            {
                if (o == null)
                {
                    continue;
                }

                if (allObjects.Contains(o))
                {
                    continue;
                }
					
                allObjects.Add(o);
            }

            return allObjects.ToArray();
        }

//	    #region We really need these?
//	    
//	    /// <summary>
//		/// Add a component to serialized object's targets. 
//		/// </summary>
//		/// <param name="serializedObject"></param>
//		/// <param name="checkIfComponentAlreadyExists"></param>
//		public static Object[] AddComponentToSerializedObjectTargets<T>(SerializedObject serializedObject, bool checkIfComponentAlreadyExists = true) where T : Component
//		{
//			List<Object> addedComponents = new List<Object>();
//			//Editor.targets aren't serializedObject.targetObjects
//			Object[] inspectedTargets = serializedObject.targetObjects;
//			for (int i = 0; i < inspectedTargets.Length; i++)
//			{
//				Object inspectedTarget = inspectedTargets[i];
//				if (inspectedTarget == null)
//				{
//					Debug.LogError("An inspected target is null.");
//					continue;
//				}
//				
//				Component inpsectedComponent = inspectedTarget as Component;
//				if (inpsectedComponent == null)
//				{
//					Debug.LogWarning("Inspected component " + inspectedTarget.name + " is not a "+ typeof(Component).Name+"." , inspectedTarget);
//					continue;
//				}
//				
//				T componentToAdd = inpsectedComponent.gameObject.GetComponent<T>();
//				if (checkIfComponentAlreadyExists)
//				{
//					if (componentToAdd == null)
//					{
//						componentToAdd = inpsectedComponent.gameObject.AddComponent<T>();
//					}
//				}
//				else
//				{
//					componentToAdd = inpsectedComponent.gameObject.AddComponent<T>();
//				}
//				
//				addedComponents.Add(componentToAdd);
//				
//				EditorUtility.SetDirty(inpsectedComponent.gameObject);
//			}
//
//			return addedComponents.ToArray();
//		}
//
//		/// <summary>
//		/// Remove a component from serialized object's targets.
//		/// This method targets the first component of T found.
//		/// </summary>
//		/// <param name="serializedObject"></param>
//		/// <typeparam name="T"></typeparam>
//		public static void RemoveComponentFromSerializedObjectTargets<T>(SerializedObject serializedObject) where T : Component
//		{
//			//Editor.targets aren't serializedObject.targetObjects
//			Object[] inspectedTargets = serializedObject.targetObjects;
//			for (int i = 0; i < inspectedTargets.Length; i++)
//			{
//				Object inspectedTarget = inspectedTargets[i];
//				if (inspectedTarget == null)
//				{
//					Debug.LogError("An inspected target is null.");
//					continue;
//				}
//					
//				Component inpsectedComponent = inspectedTarget as Component;
//				if (inpsectedComponent == null)
//				{
//					Debug.LogWarning("Inspected component " + inspectedTarget.name + " is not a "+ typeof(Component).Name+"." , inspectedTarget);
//					continue;
//				}
//
//				GameObject gameObject = inpsectedComponent.gameObject;
//				T componentToRemove = inpsectedComponent.GetComponent<T>();
//				if (componentToRemove != null)
//				{
//					EditorObjectUtils.SafeDestroy(componentToRemove);
//				}
//				
//				EditorUtility.SetDirty(gameObject);
//			}
//		}
//	    #endregion
    }
}
#endif