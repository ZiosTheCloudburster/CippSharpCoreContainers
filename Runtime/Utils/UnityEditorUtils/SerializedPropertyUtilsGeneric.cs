#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace CippSharp.Core.Containers
{
	/// <summary>
	/// Holds generics 'get-set' of value of a SerializedProperty
	/// </summary>
    public static partial class SerializedPropertyUtils
    {
        #region Generic Get / Set Value of Property
        
	    /// <summary>
	    /// Some years ago the gradient value wasn't exposed!
	    /// </summary>
        private static BindingFlags gradientValueFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
	    /// <summary>
	    /// Some years ago the gradient value wasn't exposed!
	    /// </summary>
        private static PropertyInfo gradientValuePropertyInfo = typeof(SerializedProperty).GetProperty("gradientValue", gradientValueFlags, null, typeof(Gradient), new Type[0], null);
       
	    #region Get Value

	    #region Get List
	    
	    /// <summary>
	    /// Retrieve an array of 'T' from serialized property array.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <typeparam name="T"></typeparam>
	    /// <returns></returns>
	    public static List<T> GetList<T>(SerializedProperty property)
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
			
		    List<T> objects = new List<T>();
		    for (int i = 0; i < property.arraySize; i++)
		    {
			    SerializedProperty element = property.GetArrayElementAtIndex(i);
			    try
			    {
				    objects.Add(GetValue<T>(element));
			    }
			    catch (Exception e)
			    {
				    Debug.LogError(e.Message);
			    }
		    }
		    return objects;
	    }
        
	    #endregion
	    
        /// <summary>
		/// Retrieve value from serialized property relative if possible.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="propertyRelative"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static T GetValue<T>(SerializedProperty property, string propertyRelative)
		{
			SerializedProperty foundProperty = property.FindPropertyRelative(propertyRelative);
			return GetValue<T>(foundProperty);
		}

		/// <summary>
		/// Retrieve value from serialized property if possible.
		/// </summary>
		/// <param name="property"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public static T GetValue<T>(SerializedProperty property)
		{
			Type type = typeof(T);

			// First, do special Type checks
			if (type.IsEnum)
				return (T) (object) property.intValue;

			// Next, check for literal UnityEngine struct-types
			// @note: ->object->ValueT double-casts because C# is too dumb to realize that that the ValueT in each situation is the exact type needed.
			// 	e.g. `return thisSP.colorValue` spits _error CS0029: Cannot implicitly convert type `UnityEngine.Color' to `ValueT'_
			// 	and `return (ValueT)thisSP.colorValue;` spits _error CS0030: Cannot convert type `UnityEngine.Color' to `ValueT'_
			if (typeof(Color).IsAssignableFrom(type))
				return (T) (object) property.colorValue;
			else if (typeof(LayerMask).IsAssignableFrom(type))
				return (T) (object) property.intValue;
			else if (typeof(Vector2).IsAssignableFrom(type))
				return (T) (object) property.vector2Value;
			else if (typeof(Vector3).IsAssignableFrom(type))
				return (T) (object) property.vector3Value;
			else if (typeof(Vector4).IsAssignableFrom(type))
				return (T) (object) property.vector4Value;
			else if (typeof(Rect).IsAssignableFrom(type))
				return (T) (object) property.rectValue;
			else if (typeof(AnimationCurve).IsAssignableFrom(type))
				return (T) (object) property.animationCurveValue;
			else if (typeof(Bounds).IsAssignableFrom(type))
				return (T) (object) property.boundsValue;
			else if (typeof(Gradient).IsAssignableFrom(type))
				return (T) (object) SafeGetGradientValue(property);
			else if (typeof(Quaternion).IsAssignableFrom(type))
				return (T) (object) property.quaternionValue;

			// Next, check if derived from UnityEngine.Object base class
			if (typeof(UnityEngine.Object).IsAssignableFrom(type))
				return (T) (object) property.objectReferenceValue;

			// Finally, check for native type-families
			if (typeof(int).IsAssignableFrom(type))
				return (T) (object) property.intValue;
			else if (typeof(bool).IsAssignableFrom(type))
				return (T) (object) property.boolValue;
			else if (typeof(float).IsAssignableFrom(type))
				return (T) (object) property.floatValue;
			else if (typeof(string).IsAssignableFrom(type))
				return (T) (object) property.stringValue;
			else if (typeof(char).IsAssignableFrom(type))
				return (T) (object) property.intValue;
			
			// And if all fails, throw an exception.
			throw new NotImplementedException("Unimplemented propertyType " + property.propertyType + ".");
		}
        
	    /// <summary>
	    /// Access to SerializedProperty's internal gradientValue property getter,
	    /// in a manner that'll only soft break (returning null) if the property changes
	    /// or disappears in future Unity revs.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <returns></returns>
	    private static Gradient SafeGetGradientValue(SerializedProperty property)
	    {
		    if (gradientValuePropertyInfo == null)
		    {
			    Debug.LogError("Gradient Property Info is null!");
			    return null;
		    }
		
		    Gradient gradientValue = gradientValuePropertyInfo.GetValue(property, null) as Gradient;
		    return gradientValue;
	    }
	    
        #endregion

        #region Set

	    #region Set Values
	    
        /// <summary>
        /// Overwrite array of T values.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="objects"></param>
        /// <typeparam name="T"></typeparam>
        public static void SetValues<T>(SerializedProperty property, T[] objects)
        {
            if (property == null)
            {
                Debug.LogError(PropertyIsNullError);
                return;
            }
			
            if (!property.isArray)
            {
                Debug.LogError(PropertyIsNotArrayError);
                return;
            }

            int propertyArraySize = (property.arraySize < 0 ? 0 : property.arraySize);
            int delta = objects.Length - propertyArraySize;
			
            //Increase or decrease elements for the array property.
            if (delta > 0)
            {
                for (int i = 0; i < Mathf.Abs(delta); i++)
                {
                    property.InsertArrayElementAtIndex(0);
                }
            }
            else if (delta < 0)
            {
                for (int i = 0; i < Mathf.Abs(delta); i++)
                {
                    property.DeleteArrayElementAtIndex(0);
                }
            }
			
            //Property array size and objects length must match.
            property.arraySize = objects.Length;
			
            for (int i = 0; i < objects.Length; i++)
            {
                SerializedProperty element = property.GetArrayElementAtIndex(i);
                try
                {
                    SetValue(element, objects[i]);
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                }
            }
        }
	    
	    #endregion
	    
        /// <summary>
		/// Set value to serialized property relative if possible.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="propertyRelative"></param>
		/// <param name="value"></param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static void SetValue<T>(SerializedProperty property, string propertyRelative, T value)
		{
			SerializedProperty foundProperty = property.FindPropertyRelative(propertyRelative);
			SetValue(foundProperty, value);
		}
		
		/// <summary>
		/// Set value to serialized property if possible.
		/// </summary>
		/// <param name="property"></param>
		/// <param name="value"></param>
		/// <typeparam name="T"></typeparam>
		/// <exception cref="NotImplementedException"></exception>
		public static void SetValue<T>(SerializedProperty property, T value)
		{
			Type type = typeof(T);

			// First, do special Type checks
			if (type.IsEnum)
			{
				property.intValue = (int)(object)value;
				return;
			}

			// Next, check for literal UnityEngine struct-types
			// @note: ->object->ValueT double-casts because C# is too dumb to realize that that the ValueT in each situation is the exact type needed.
			// 	e.g. `return thisSP.colorValue` spits _error CS0029: Cannot implicitly convert type `UnityEngine.Color' to `ValueT'_
			// 	and `return (ValueT)thisSP.colorValue;` spits _error CS0030: Cannot convert type `UnityEngine.Color' to `ValueT'_
			if (typeof(Color).IsAssignableFrom(type))
			{
				property.colorValue = (Color)(object)value;
				return;
			}
			else if (typeof(LayerMask).IsAssignableFrom(type))
			{
				property.intValue = (int)(object)value;
				return;
			}
			else if (typeof(Vector2).IsAssignableFrom(type))
			{
				property.vector2Value = (Vector2)(object)value;
				return;
			}
			else if (typeof(Vector3).IsAssignableFrom(type))
			{
				property.vector3Value = (Vector3)(object)value;
				return;
			}
			else if (typeof(Vector4).IsAssignableFrom(type))
			{
				property.vector4Value = (Vector4)(object)value;
				return;
			}
			else if (typeof(Rect).IsAssignableFrom(type))
			{
				property.rectValue = (Rect)(object)value;
				return;
			}
			else if (typeof(AnimationCurve).IsAssignableFrom(type))
			{
				property.animationCurveValue = (AnimationCurve)(object)value;
				return;
			}
			else if (typeof(Bounds).IsAssignableFrom(type))
			{
				property.boundsValue = (Bounds)(object)value;
				return;
			}
			else if (typeof(Gradient).IsAssignableFrom(type))
			{
				SafeSetGradientValue(property, (Gradient)(object)value);
				return;
			}
			else if (typeof(Quaternion).IsAssignableFrom(type))
			{
				property.quaternionValue = (Quaternion)(object)value;
				return;
			}

			// Next, check if derived from UnityEngine.Object base class
			if (typeof(UnityEngine.Object).IsAssignableFrom(type))
			{
				property.objectReferenceValue = (UnityEngine.Object)(object)value;
				return;
			}

			// Finally, check for native type-families
			if (typeof(int).IsAssignableFrom(type))
			{
				property.intValue = (int)(object)(value);
				return;
			}
			else if (typeof(bool).IsAssignableFrom(type))
			{
				property.boolValue = (bool)(object)value;
				return;
			}
			else if (typeof(float).IsAssignableFrom(type))
			{
				property.floatValue = (float)(object)value;
				return;
			}
			else if (typeof(string).IsAssignableFrom(type))
			{
				property.stringValue = (string)(object)value;
				return;
			}
			else if (typeof(char).IsAssignableFrom(type))
			{
				property.intValue = (int)(object)(value);
				return;
			}
			
			// And if all fails, throw an exception.
			throw new NotImplementedException("Unimplemented propertyType " + property.propertyType + ".");
		}

	    /// <summary>
	    /// Access to SerializedProperty's internal gradientValue property getter,
	    /// in a manner that'll only soft break (returning null) if the property changes
	    /// or disappears in future Unity revs.
	    /// </summary>
	    /// <param name="property"></param>
	    /// <param name="newGradient"></param>
	    /// <returns></returns>
	    private static void SafeSetGradientValue(SerializedProperty property, Gradient newGradient)
	    {
		    if (gradientValuePropertyInfo == null)
		    {
			    Debug.LogError("Gradient Property Info is null!");
			    return;
		    }
		
		    gradientValuePropertyInfo.SetValue(property, newGradient, null);
	    }

        #endregion

        #endregion
    }
}
#endif
