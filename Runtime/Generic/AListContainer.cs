using System;
using System.Collections.Generic;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Abstract class for generic list containers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public abstract class AListContainer<T> : Container<List<T>>
    {
        public override Type ContainerType => typeof(List<T>);
    }
}
