using System.Collections.Generic;

namespace CippSharp.Core.Containers
{
    public static class LootTableExtensions
    {
        /// <summary>
        /// From any table of pairs with a float, get an element 
        /// </summary>
        /// <param name="container"></param>
        /// <param name="element"></param>
        /// <typeparam name="TPair"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public static bool DropElement<TPair, TKey>(this ICollectionContainer<List<TPair>, TPair> container, out TKey element) where TPair : PairContainer<TKey, float>
        {
            return ContainersLootTable.DropElement(container.Collection, out element);
        }

        /// <summary>
        /// From any collection of pairs with a loot, get an element
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="element"></param>
        /// <typeparam name="TPair"></typeparam>
        /// <typeparam name="TLoot"></typeparam>
        /// <returns></returns>
        public static bool DropElement<TPair, TLoot>(this ICollection<TPair> collection, out TLoot element) where TPair : ILootPair
        {
            return ContainersLootTable.DropElementFromLootPair(collection, out element);
        }
    }
}
