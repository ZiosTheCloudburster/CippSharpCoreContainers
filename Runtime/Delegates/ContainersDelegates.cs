
namespace CippSharp.Core.Containers
{
    /// <summary>
    /// To access generic data
    /// </summary>
    /// <param name="data"></param>
    public delegate void GenericAccessDelegate(ref object data);
    /// <summary>
    /// To access generic data for a check
    /// </summary>
    /// <param name="data"></param>
    public delegate bool PredicateGenericAccessDelegate(ref object data);
    
    /// <summary>
    /// To access stored data.
    /// </summary>
    /// <param name="data"></param>
    public delegate void AccessDelegate<T>(ref T data);
    /// <summary>
    /// To access stored data for a check
    /// </summary>
    /// <param name="data"></param>
    public delegate bool PredicateAccessDelegate<T>(ref T data);
}
