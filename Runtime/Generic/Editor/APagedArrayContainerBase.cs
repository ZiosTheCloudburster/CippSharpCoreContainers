#if UNITY_EDITOR
using CippSharp.Core;
using CippSharp.Core.Containers;
using UnityEditor;
using UnityEngine;

namespace CippSharpEditor.Core.Containers
{
    [CustomPropertyDrawer(typeof(APagedArrayContainerBase), true)]
    public class PagedArrayContainerDrawer : PropertyDrawer
    {
        //references
        protected const string arrayPropertyName = "array";
        protected SerializedProperty arrayProperty = null;
        protected const string elementsPerPageName = "elementsPerPage";
        protected SerializedProperty elementsPerPageProperty = null;

        protected const string inspectedElementsName = "inspectedElements";
        protected const string pageIndexFieldName = "pageIndex";
        protected const string setPageIndexMethodName = "SetPageIndex";
        protected SerializedProperty pageIndexProperty = null;
        protected SerializedProperty inspectedElementsProperty = null;

        //variables
        protected float height = 0;
        protected int pageIndex = 0;
        protected int length = 0;
        protected Rect r = new Rect();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            r = position;
            height = EditorGUIUtils.LineHeight;
            EditorGUIUtils.DrawFoldout(r, property, out r);
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;

                //Get Inspected Elements Property
                inspectedElementsProperty = property.FindPropertyRelative(inspectedElementsName);

                //Draw elements per page
                elementsPerPageProperty = property.FindPropertyRelative(elementsPerPageName);

                r.height = EditorGUI.GetPropertyHeight(elementsPerPageProperty);
                EditorGUI.PropertyField(r, elementsPerPageProperty);
                r.y += r.height + EditorGUIUtils.VerticalSpacing * 2;
                height += r.height + EditorGUIUtils.VerticalSpacing * 2;

                //Get Array Property
                arrayProperty = property.FindPropertyRelative(arrayPropertyName);
                length = arrayProperty.arraySize;

                //Get PageIndex Property
                pageIndexProperty = property.FindPropertyRelative(pageIndexFieldName);
                pageIndex = pageIndexProperty.intValue;

                //Start with computation
                int pagesLength = (Mathf.CeilToInt((float) length / (float) elementsPerPageProperty.intValue)) - 1;
                if (pageIndex > pagesLength)
                {
                    Debug.LogWarning("Pages index out of range! Last page will be drawn instead.");
                }

                //Draw feedback label 0
                EditorGUI.LabelField(r, $"Total array length: {arrayProperty.arraySize.ToString()}");
                r.y += EditorGUIUtils.LineHeight;
                height += EditorGUIUtils.LineHeight;

                //Draw int slider of page index
                pageIndex = Mathf.Clamp(pageIndex, 0, arrayProperty.arraySize);
                EditorGUI.BeginChangeCheck();
                r.height = EditorGUIUtils.SingleLineHeight;
                pageIndex = EditorGUI.IntSlider(r, pageIndex, 0, pagesLength);
                if (EditorGUI.EndChangeCheck())
                {
                    SerializedPropertyUtils.TryEditLastParentLevel(inspectedElementsProperty, SetPageIndex);
                }

                r.y += EditorGUIUtils.LineHeight;
                height += EditorGUIUtils.LineHeight;

                //Draw feedback label 1
                GUI.enabled = false;

                EditorGUI.LabelField(r, $"Displaying page: {pageIndex.ToString()}/{pagesLength.ToString()}.");
                r.y += EditorGUIUtils.LineHeight;
                height += EditorGUIUtils.LineHeight;

                //Draw inspected elements array
                r.height = EditorGUIUtils.GetPropertyHeight(inspectedElementsProperty);
                EditorGUI.PropertyField(r, inspectedElementsProperty, true);
                r.y += r.height + EditorGUIUtils.VerticalSpacing;
                height += r.height + EditorGUIUtils.VerticalSpacing;

                //Draw feedback label 2
                EditorGUI.indentLevel++;
                r.height = EditorGUIUtils.SingleLineHeight;
                EditorGUI.LabelField(r,
                    $"Displaying elements: {inspectedElementsProperty.arraySize.ToString()}/{elementsPerPageProperty.intValue.ToString()}.");
                r.y += EditorGUIUtils.LineHeight;
                height += EditorGUIUtils.LineHeight;
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
