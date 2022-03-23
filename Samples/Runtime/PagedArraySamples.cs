#if UNITY_EDITOR
using UnityEngine;

namespace CippSharp.Core.Containers.Samples
{
    /// <summary>
    /// Paged Arrays are intended for BIG DATA arrays that you want to display in inspector... someway.
    /// </summary>
    public class PagedArraySamples : MonoBehaviour
    {
        [SerializeField] 
        public EncodedPagedBytesArray encodedPagedBytesArray = new EncodedPagedBytesArray(new byte[464688]);
    }
}
#endif