#if UNITY_EDITOR
namespace CippSharp.Core.Containers
{
    internal static class EditorConstants
    {
        /// <summary>
        /// Unity's default name for script property.
        /// </summary>
        public const string ScriptSerializedPropertyName = "m_Script";

        /// <summary>
        /// UnityEditor's default name for inspected objects local identfier. 
        /// </summary>
        public const string LocalIdentfierInFilePropertyName = "m_LocalIdentfierInFile";

        /// <summary>
        /// Unity serialized property array element part path.
        /// </summary>
        public const string Array = "Array";

    }
}
#endif