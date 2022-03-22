using Serializable = System.SerializableAttribute;

namespace CippSharp.Core.Containers
{
    [Serializable]
    public abstract class SerializedDictionaryBase : AContainerBase
    {
        public abstract void Clear();

        public abstract bool IsNullOrEmpty();
    }
}