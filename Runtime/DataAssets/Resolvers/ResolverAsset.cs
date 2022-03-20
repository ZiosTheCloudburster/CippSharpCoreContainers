using UnityEngine;

namespace CippSharp.Core
{
    /// <summary>
    /// Purpose: abstract class for resolvers.
    /// </summary>
    public abstract class ResolverBase : ScriptableObject
    {
        
    }
    
    /// <summary>
    /// Purpose: abstract class for typed resolvers.
    /// 
    /// Resolvers are 'other classes' that should handle specific methods
    /// or peculiar setups that may differs once by once
    /// </summary>
    public abstract class ResolverAsset<T> : ResolverBase
    {
        /// <summary>
        /// Resolve!
        /// </summary>
        /// <param name="parameter"></param>
        /// <typeparam name="K"></typeparam>
        /// <returns></returns>
        public abstract bool Resolve<K>(ref K parameter) where K : T;
    }
    
    #region Extensions
    
    public static class ResolverAssetExtensions
    {
        /// <summary>
        /// Try Resolve!
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="parameter"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryResolve<T>(this ResolverAsset<T> asset, ref T parameter)
        {
            try
            {
                return asset.Resolve(ref parameter);
            }
            catch 
            {
                //Ignored
                return false;
            }
        }

        /// <summary>
        /// Try Resolve not directly referred
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="parameter"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryResolve<T>(this ResolverAsset<T> asset, T parameter)
        {
            try
            {
                return asset.Resolve(ref parameter);
            }
            catch 
            {
                //Ignored
                return false;
            }
        }
    }
    
    #endregion
}
