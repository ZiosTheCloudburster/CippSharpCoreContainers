#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = System.Object;


namespace CippSharp.Core.Containers
{
    internal static class SerializedPropertyUtils
    {
        #region Edit Parent Level

        /// <summary>
        /// References the last parent of a property and regardless of method called it folds back 'exposed' values by reflection.
        /// Use this for get-set behaviour of inspector nest structure.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="callback"></param>
        /// <param name="debug"></param>
        /// <returns></returns>
        public static bool TryEditLastParentLevel(SerializedProperty property, GenericRefAction callback, bool debug = false)
        {
            if (property == null)
            {
                return false;
            }

            string name = property.name;
            string path = property.propertyPath;
            string revisedPath = path.Replace(name, string.Empty);
            
            try
            {
                bool value = true;
                List<object> tmpParents = new List<object>();
                
                Object targetObject = property.serializedObject.targetObject;
                object contextObject = targetObject;
                
                string[] splitResults = revisedPath.Split(new []{'.'}, StringSplitOptions.RemoveEmptyEntries);
                List<string> aggregatedSplitResults = new List<string>();
                if (debug)
                {
                    Debug.Log($"Length {splitResults.Length.ToString()}");
                }

                #region Unfold Parents
                
                int i = 0;
                
                while (i < splitResults.Length)
                {
                    string fieldName = splitResults[i];
                    if (string.IsNullOrEmpty(fieldName))
                    {
                        if (debug)
                        {
                            Debug.Log($"{i.ToString()} --> {fieldName}");
                        }

                        //Do Nothing
                    }
                    else if (fieldName == EditorConstants.Array && i + 1 < splitResults.Length && splitResults[i + 1].Contains("data"))
                    {
                        #region Array Element Property
                        
                        string data = splitResults[i + 1];
                        if (int.TryParse(data.Replace("data[", string.Empty).Replace("]", string.Empty), out int w))
                        {
                            if (ArrayUtils.IsArray(contextObject) && ArrayUtils.TryCast(contextObject, out object[] array))
                            {
                                if (ArrayUtils.TryGetValue(array, w, out object element))
                                {
                                    object previousContextObject = ((object)(Array)array);
                                    tmpParents.Add(previousContextObject);
                                    contextObject = element;

                                    if (debug)
                                    {
                                        Debug.Log($"{i.ToString()} --> {fieldName}");
                                        Debug.Log($"{(i + 1).ToString()} --> {data}");
                                    }

                                    aggregatedSplitResults.Add(fieldName + "." + data);

                                    i += 2;
                                    continue;
                                }
                                else
                                {
                                    value = false;
                                    break;
                                }
                            }
                            else
                            {
                                value = false;
                                break;
                            }
                        }
                        else
                        {
                            value = false;
                            break;
                        }
                        
                        #endregion
                    }
                    else if (ReflectionUtils.HasField(contextObject, fieldName, out FieldInfo field))
                    {
                        if (debug)
                        {
                            Debug.Log($"{i.ToString()} --> {fieldName}");
                        }

                        aggregatedSplitResults.Add(fieldName);
                        var previousContextObject = contextObject;
                        tmpParents.Add(previousContextObject);
                        contextObject = field.GetValue(previousContextObject);

                    }
                    else
                    {
                        value = false;
                        break;
                    }
                    
                    i++;
                }
                
                #endregion
                
                callback?.Invoke(ref contextObject);
                tmpParents.Add(contextObject);
	            
                if (debug)
                {
                    Debug.Log($"Parents Length {tmpParents.Count.ToString()} / Split Results Length {splitResults.Length.ToString()} / Aggregated Split Results Length {aggregatedSplitResults.Count.ToString()}");
                }

                splitResults = aggregatedSplitResults.ToArray();
                i = splitResults.Length - 1;
                object previousContext = (i >= 0) ? tmpParents[i] : targetObject;
	            
                #region Fold Parents

	            if (i >= 0)
	            {
		            while (i >= 0)
		            {
			            string fieldName = splitResults[i];
			            if (string.IsNullOrEmpty(fieldName))
			            {
				            if (debug)
				            {
					            Debug.Log($"Reverse loop {i.ToString()} --> {fieldName}");
				            }
			            }
			            else if (fieldName.Contains(EditorConstants.Array) && fieldName.Contains("data"))
			            {
				            #region Array Element Property

				            string data = fieldName;
				            string parsingString = data.Replace(EditorConstants.Array, string.Empty)
					            .Replace(".", string.Empty).Replace("data[", string.Empty).Replace("]", string.Empty);
				            if (int.TryParse(parsingString, out int w))
				            {
					            if (ArrayUtils.IsArray(previousContext) &&
					                ArrayUtils.TryCast(previousContext, out object[] array))
					            {
						            object element = tmpParents[i + 1];
						            if (ArrayUtils.TrySetValue(array, w, element))
						            {
							            Array destinationArray = Array.CreateInstance(element.GetType(), array.Length);
							            Array.Copy(array, destinationArray, array.Length);
							            tmpParents[i] = destinationArray;
							            if (debug)
							            {
								            Debug.Log($"Reverse loop {i.ToString()} --> {fieldName}");
							            }
						            }
						            else
						            {
							            value = false;
							            break;
						            }
					            }
					            else
					            {
						            value = false;
						            break;
					            }
				            }
				            else
				            {
					            value = false;
					            break;
				            }

				            #endregion
			            }
			            else if (ReflectionUtils.HasField(previousContext, fieldName, out FieldInfo field))
			            {
				            if (debug)
				            {
					            Debug.Log($"Reverse loop {i.ToString()} --> {fieldName}");
				            }

				            field.SetValue(previousContext, tmpParents[i + 1]);
			            }

			            i--;
			            if (i < 0)
			            {
				            break;
			            }

			            previousContext = tmpParents[i];
		            }
	            }
	            else
	            {
		            value = true;
		            
		            if (debug)
		            {
			            Debug.Log($"{i.ToString()} is less than 0");
		            }
	            }

	            #endregion

                return value;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to reach parent level of property {property.name}. Caught Exception: {e.Message}.");
                return false;
            }
        }

        #endregion
    }
}
#endif

