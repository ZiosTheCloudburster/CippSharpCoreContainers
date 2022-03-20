
using System;

namespace CippSharp.Core.Containers
{
    public abstract class AContainerBase : IContainerBase
    {
        public abstract Type ContainerType { get; }

        public abstract object GetValueRaw();

        public abstract void Access(GenericAccessDelegate access);

        public abstract bool Check(PredicateGenericAccessDelegate access);

        public abstract void Set(object newValue);
    }
}
