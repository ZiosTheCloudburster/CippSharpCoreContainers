using System;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Abstract class for generic array containers
    /// </summary>
    [Serializable]
    public abstract class AArrayContainer<T> : Container<T[]>
    {
        public override Type ContainerType => typeof(T[]);
    }
}
