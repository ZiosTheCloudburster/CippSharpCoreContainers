#if UNITY_EDITOR
using UnityEngine;

namespace CippSharp.Core.Containers.Samples
{
    /// <summary>
    /// Paged Arrays are intended for BIG DATA arrays that you want to display in inspector... someway.
    /// </summary>
    internal class PagedArraySamples : MonoBehaviour
    {
        [SerializeField] 
        public EncodedPagedBytesArray encodedPagedBytesArray = new EncodedPagedBytesArray(GetRandomBytesArray(464688));

        /// <summary>
        /// Generates a random bytes array
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static byte[] GetRandomBytesArray(int length)
        {
            byte[] newArray = new byte[length];
            System.Random random = new System.Random();
            random.NextBytes(newArray);
            return newArray;
        }
    }
}
#endif
