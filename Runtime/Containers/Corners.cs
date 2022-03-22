using System;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Purpose holds a 4 length Vector3 array.
    /// Each of stored Vector3 indicates a point.
    /// </summary>
    [Serializable]
    public class CornersHolder : AArrayContainer<Vector3>, ISerializationCallbackReceiver
    {
        /// <summary>
        /// Reference to old stuffs
        /// </summary>
        public Vector3[] corners
        {
            get => value;
            set => this.value = value;
        }

//        public Vector3 this[int index]
//        {
//            get => corners[index];
//            set => corners[index] = value;
//        }
        
        public override int Count => 4;

        /// <summary>
        /// Use <see cref="AArrayContainer.Count"/> instead.
        /// </summary>
        [Obsolete("Use Count instead.")]
        public int Length => Count;

        public CornersHolder()
        {
            this.value = new Vector3[4];            
        }

        /// <summary>
        /// Ensure the length of the corners
        /// </summary>
        public void OnBeforeSerialize()
        {
            if (ArrayUtils.IsNullOrEmpty(value))
            {
                this.value = new Vector3[4];
            }
            else if (this.value.Length < 4)
            {
                Array.Resize(ref value, 4);
            }
        }

        public void OnAfterDeserialize()
        {
            
        }
    }
}
