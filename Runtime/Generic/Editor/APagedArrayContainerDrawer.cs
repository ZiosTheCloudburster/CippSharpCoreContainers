#if UNITY_EDITOR
using CippSharp.Core.Containers;
using UnityEditor;
using UnityEngine;

namespace CippSharpEditor.Core.Containers
{
    [CustomPropertyDrawer(typeof(APagedArrayContainerBase), true)]
    public class APagedArrayContainerDrawer : PropertyDrawer
    {
        //references
        protected const string arrayPropertyName = "value";
        protected SerializedProperty arrayProperty = null;
        protected const string elementsPerPageName = "elementsPerPage";
        protected SerializedProperty elementsPerPageProperty = null;

        protected const string inspectedElementsName = "inspectedElements";
        protected const string pageIndexFieldName = "pageIndex";
        protected const string setPageIndexMethodName = "SetPageIndex";
        protected SerializedProperty pageIndexProperty = null;
        protected SerializedProperty inspectedElementsProperty = null;

        //variables
        protected float h = 0;
        protected int length = 0;
        protected int elementsPerPage = 0;
        protected int clampedElementsPerPage = 0;
        protected int pageIndex = 0;
        protected int pagesCount = 0;
        protected Rect r = new Rect();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            h = EditorGUIUtils.LineHeight;
            if (property.isExpanded)
            {
                h += r.height + EditorGUIUtils.VerticalSpacing * 2;
                
                h += EditorGUIUtils.LineHeight;
                h += EditorGUIUtils.LineHeight;
                h += EditorGUIUtils.LineHeight;
                
                inspectedElementsProperty = property.FindPropertyRelative(inspectedElementsName);
                h += EditorGUIUtils.GetPropertyHeight(inspectedElementsProperty) + EditorGUIUtils.VerticalSpacing;
                
                h += EditorGUIUtils.LineHeight;
            }
            return h;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            r = position;
//            h = EditorGUIUtils.LineHeight;
            EditorGUIUtils.DrawFoldout(r, property, out r);
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;

                //Get Stuffs
                //Get Array Property
                arrayProperty = property.FindPropertyRelative(arrayPropertyName);
                length = arrayProperty.arraySize;

                //Get elements per page
                elementsPerPageProperty = property.FindPropertyRelative(elementsPerPageName);
                elementsPerPage = elementsPerPageProperty.intValue;
                
                //Get PageIndex Property
                pageIndexProperty = property.FindPropertyRelative(pageIndexFieldName);
                pageIndex = pageIndexProperty.intValue;
                
                //Get Inspected Elements Property
                inspectedElementsProperty = property.FindPropertyRelative(inspectedElementsName);
                
                int arrayIndex = pageIndex * elementsPerPage;
                //in this case pages count work as 'max reachable index'
                pagesCount = (Mathf.CeilToInt(length / (float)elementsPerPage)) - 1;
                clampedElementsPerPage = arrayIndex + elementsPerPage < length ? elementsPerPage : Mathf.Clamp(elementsPerPage, 0, length - arrayIndex);

                if (pageIndex > pagesCount)
                {
                    Debug.LogWarning("Pages index out of range! Last page will be drawn instead.");
                }
                
                //Begin Drawing Properties
                //Draw elements per page
                r.height = EditorGUI.GetPropertyHeight(elementsPerPageProperty);
                EditorGUI.PropertyField(r, elementsPerPageProperty);
                r.y += r.height + EditorGUIUtils.VerticalSpacing * 2;

                //Draw feedback label 0
                EditorGUI.LabelField(r, $"Total array length: {length.ToString()}");
                r.y += EditorGUIUtils.LineHeight;

                //Draw int slider of page index
                //clamp page index between 0 and actual length
                //(inspected elements array cannot have his length major)
                pageIndex = Mathf.Clamp(pageIndex, 0, length);
                EditorGUI.BeginChangeCheck();
                r.height = EditorGUIUtils.SingleLineHeight;
                pageIndex = EditorGUI.IntSlider(r, pageIndex, 0, pagesCount);
                if (EditorGUI.EndChangeCheck())
                {
                    SerializedPropertyUtils.TryEditLastParentLevel(inspectedElementsProperty, SetPageIndex);
                    EditorUtility.SetDirty(property.serializedObject.targetObject);
                }
                r.y += EditorGUIUtils.LineHeight;
                

                //Draw feedback label 1
                GUI.enabled = false;
                EditorGUI.LabelField(r, $"Displaying page: {pageIndex.ToString()}/{pagesCount.ToString()}.");
                r.y += EditorGUIUtils.LineHeight;
               

                //Draw inspected elements array
                r.height = EditorGUIUtils.GetPropertyHeight(inspectedElementsProperty);
                EditorGUI.PropertyField(r, inspectedElementsProperty, true);
                r.y += r.height + EditorGUIUtils.VerticalSpacing;
               

                //Draw feedback label 2
                EditorGUI.indentLevel++;
                r.height = EditorGUIUtils.SingleLineHeight;
                EditorGUI.LabelField(r,
                    $"Displaying elements: {clampedElementsPerPage.ToString()}/{elementsPerPageProperty.intValue.ToString()}.");
                r.y += EditorGUIUtils.LineHeight;
               
                EditorGUI.indentLevel--;

                GUI.enabled = true;
                
                DrawAdditionalChildrenProperties(ref r, property);

                EditorGUI.indentLevel--;
            }
        }
        
        private void SetPageIndex(ref object context)
        {
            ReflectionUtils.TryCallMethod(context, setPageIndexMethodName, out object @null, new object[] {pageIndex});
        }
        
        protected virtual void DrawAdditionalChildrenProperties(ref Rect rect, SerializedProperty rootProperty)
        {
            
        }
    }
}
#endif
