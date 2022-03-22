//
// Author: Alessandro Salani (Cippo)
//
#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;

namespace CippSharp.Core.Containers
{
    public static partial class EditorGUIUtils
    {
        /// <summary>
        /// Wrap of unity's default single line height.
        /// </summary>
        public static readonly float SingleLineHeight = EditorGUIUtility.singleLineHeight;
  
        /// <summary>
        /// Wrap of unity's default vertical spacing between lines.
        /// </summary>
        public static readonly float VerticalSpacing = EditorGUIUtility.standardVerticalSpacing;
  
        /// <summary>
        /// Sum of <see cref="SingleLineHeight"/> + <seealso cref="VerticalSpacing"/>.
        /// </summary>
        public static readonly float LineHeight = SingleLineHeight + VerticalSpacing;

        
        //Reflection
        private const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy; 
        
        /// <summary>
        /// As Unity draws Property Field Internal?
        /// </summary>
        public static readonly MethodInfo PropertyFieldInternalMethodInfo = typeof(EditorGUI).GetMethod("PropertyFieldInternal", flags);
        /// <summary>
        /// As Unity draws single line Property Field
        /// </summary>
        public static readonly MethodInfo DefaultPropertyFieldMethodInfo = typeof(EditorGUI).GetMethod("DefaultPropertyField", flags);
    }
}
#endif
