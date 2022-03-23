using Serializable = System.SerializableAttribute;

namespace CippSharp.Core.Containers
{
    [Serializable]
    public abstract class SerializedDictionaryBase : AContainerBase
    {
        /// <summary>
        /// Error message if dictionaries have duplicates
        /// </summary>
        protected const string DictionariesDuplicatesError = "Dictionaries cannot have duplicated keys.";

        /// <summary>
        /// Warning message for duplicates
        /// </summary>
        protected const string DictionariesDuplicatesWarning = "WARNING: you keys are containing a duplicate. This is not allowed in dictionaries.";

        /// <summary>
        /// Warning message for duplicates
        /// </summary>
        protected const string DictionariesKeyAlreadyPresentWarning = "Key already present in the dictionary.";
        
        public abstract void Clear();

        public abstract bool IsNullOrEmpty();
    }
}