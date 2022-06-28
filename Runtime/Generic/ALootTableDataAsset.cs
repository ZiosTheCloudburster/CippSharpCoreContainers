
namespace CippSharp.Core.Containers
{
    public abstract class ALootTableDataAsset<TPair, TResult> : AListDataAsset<TPair> where TPair : ILootPair
    {
        /// <summary>
        /// Drop Element
        /// </summary>
        /// <returns></returns>
        public virtual TResult DropElement()
        {
            value.DropElement<TPair, TResult>(out var element);
            return element;
        }
    }
}
