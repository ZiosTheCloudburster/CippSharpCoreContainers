#if UNITY_EDITOR

namespace CippSharp.Core.Containers
{
    public static partial class SerializedPropertyUtils
    {
        public const string k_BackingField = "k__BackingField";
        
        private const string PropertyIsNullError = "Property is null.";
        private const string PropertyIsNotArrayError = "Property isn't an array.";
    }
}
#endif

