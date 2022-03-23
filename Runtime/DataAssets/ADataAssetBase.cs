using System;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    public abstract class ADataAssetBase : ScriptableObject, IContainerBase
    {
        public abstract Type ContainerType { get; }

        public abstract object GetValueRaw();

        public abstract void Access(GenericAccessDelegate access);

        public abstract bool Check(PredicateGenericAccessDelegate access);

        public abstract void Set(object newValue);
        
        protected virtual void OnValidate()
        {
            
        }
    }
}
