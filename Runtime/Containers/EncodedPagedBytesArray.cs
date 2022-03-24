using System;
#if UNITY_EDITOR
using CippSharpEditor.Core.Containers;
#endif
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CippSharp.Core.Containers
{
    [Serializable]
    public class EncodedPagedBytesArray : PagedArrayContainer<byte>
    {
        #region Custom Editor
#if UNITY_EDITOR
        [SerializeField, TextArea(1, 3)] internal string encoded = "";

        protected override void OnSetPageIndex(int newPageIndex, byte[] inspectedElements)
        {
            base.OnSetPageIndex(newPageIndex, inspectedElements);

            if (!ArrayUtils.IsNullOrEmpty(inspectedElements))
            {
                encoded = StringUtils.EncodeBytes(inspectedElements);
            }
        }
#endif
        #endregion

        /// <summary>
        /// Get Encoded Bytes as string
        /// </summary>
        /// <param name="newPageIndex"></param>
        /// <returns></returns>
        public string GetEncodedBytesAsString(int newPageIndex)
        {
            var elements = GetInspectedElementsAt(newPageIndex);
            return ArrayUtils.IsNullOrEmpty(elements) ? string.Empty : StringUtils.EncodeBytes(elements);
        }

        public EncodedPagedBytesArray()
        {
            this.value = new byte[0];
        }

        public EncodedPagedBytesArray(byte[] array)
        {
            this.value = array;
        }
        
        #region Custom Editor
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(EncodedPagedBytesArray), true)]
        public class EncodedPagedBytesArrayDrawer : APagedArrayContainerDrawer
        {
            /// <summary>
            /// Simply add the new property to draw
            /// </summary>
            /// <param name="position"></param>
            /// <param name="property"></param>
            /// <param name="label"></param>
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                base.OnGUI(position, property, label);

                //continue from base
                if (property.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    GUI.enabled = false;
                    SerializedProperty encodedProperty = property.FindPropertyRelative(nameof(encoded));
                    r.height = EditorGUI.GetPropertyHeight(encodedProperty);
                    EditorGUI.PropertyField(r, encodedProperty);
                    r.y += r.height + EditorGUIUtils.VerticalSpacing;
                    h += r.height + EditorGUIUtils.VerticalSpacing;
                    GUI.enabled = true;
                    EditorGUI.indentLevel--;
                }
            }
        }
#endif
        #endregion
    }
}
