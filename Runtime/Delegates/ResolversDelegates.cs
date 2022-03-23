
namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Resolver Delegate
    /// </summary>
    /// <param name="parameter"></param>
    /// <typeparam name="T"></typeparam>
    public delegate bool Resolve<T>(ref T parameter);
}