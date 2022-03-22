
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
   
    /// <summary>
    /// To access stored data pairs
    /// </summary>
    /// <param name="data1"></param>
    /// <param name="data2"></param>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public delegate void AccessDelegate<T1, T2>(ref T1 data1, ref T2 data2);
    /// <summary>
    /// To access stored data for a check
    /// </summary>
    /// <param name="data1"></param>
    /// <param name="data2"></param>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public delegate bool PredicateAccessDelegate<T1, T2>(ref T1 data1, ref T2 data2);

//    /// <summary>
//    /// To
//    /// </summary>
//    /// <param name="element"></param>
//    /// <typeparam name="T"></typeparam>
//    public delegate T MatchAccessDelegate<T>(ref T element);
}
