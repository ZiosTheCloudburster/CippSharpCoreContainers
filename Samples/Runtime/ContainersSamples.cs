using System;
using System.Collections.Generic;
using UnityEngine;

namespace CippSharp.Core.Containers.Samples
{
    public class ContainersSamples : MonoBehaviour
    {
        [Serializable]
        public struct CustomData
        {
            public float a;
            public float b;
            public float c;
        }
        
        [Serializable]
        public class CustomDataContainer : Container<CustomData>
        {
            
        }
        
        [Serializable]
        public class CustomDataListContainer : Container<List<CustomData>>
        {
            
        }
        
        [Serializable]
        public class SpecificCustomDataListContainer : AListContainer<CustomData>
        {
            
        }

        [Header("Settings:")]
        public CustomData data0 = new CustomData();
        public CustomDataContainer dataContainer0 = new CustomDataContainer();
        public CustomDataListContainer dataListContainer0 = new CustomDataListContainer();
        public SpecificCustomDataListContainer specificDataListContainer0 = new SpecificCustomDataListContainer();
    }
}
