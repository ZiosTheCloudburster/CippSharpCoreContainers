namespace CippSharp.Core.Containers
{
    public static class ResolverAssetExtensions
    {
        /// <summary>
        /// Try Resolve!
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="parameter">passed as reference</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryResolve<T>(this AResolverAsset<T> asset, ref T parameter)
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
        /// Try Resolve! (indirect)
        /// </summary>
        /// <param name="asset"></param>
        /// <param name="parameter">not passed as reference</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool TryResolve<T>(this AResolverAsset<T> asset, T parameter)
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
}
