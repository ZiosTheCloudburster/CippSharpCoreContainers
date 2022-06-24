
using System.Collections.Generic;
using System.Linq;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Get loot table element
    /// </summary>
    public static class ContainersLootTable
    {
        /// <summary>
        /// From a loot table of pairs, get an element 
        /// </summary>
        /// <param name="table">made of T elements and floats values </param>
        /// <param name="element"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public static bool DropElement<TKey>(ICollection<KeyValuePair<TKey, float>> table, out TKey element)
        {
            if (table == null || table.Count == 0)
            {
                element = default(TKey);
                return false;
            }
            
            if (table.Count == 1)
            {
                element = table.FirstOrDefault().Key;
                return true;
            }
            
            var array = table.OrderByDescending(k => k.Key).ToArray();
            double total = array.Select(k => k.Value).Sum();
            float randomNumber = UnityEngine.Random.Range(0.0000f, (float) total);
            
            foreach (var pair in array)
            {
                //randomNumber <= weight
                float weight = pair.Value;
                if (randomNumber <= weight)
                {
                    //award item
                    element = pair.Key;
                    return true;
                }
                else
                {
                    randomNumber -= weight;
                }
            }
            
            element = default(TKey);
            return false;    
        }

        /// <summary>
        /// From a loot table of pairs, get an element 
        /// </summary>
        /// <param name="table">made of T elements and floats values </param>
        /// <param name="element"></param>
        /// /// <typeparam name="TPair"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public static bool DropElement<TPair, TKey>(ICollection<TPair> table, out TKey element) where TPair : PairContainer<TKey, float>
        {
            if (table == null || table.Count == 0)
            {
                element = default(TKey);
                return false;
            }
            
            if (table.Count == 1)
            {
                element = table.FirstOrDefault().Key;
                return true;
            }
            
            var array = table.OrderByDescending(k => k.Key).ToArray();
            double total = array.Select(k => k.Value).Sum();
            float randomNumber = UnityEngine.Random.Range(0.0000f, (float) total);
            
            foreach (var pair in array)
            {
                //randomNumber <= weight
                float weight = pair.Value;
                if (randomNumber <= weight)
                {
                    //award item
                    element = pair.Key;
                    return true;
                }
                else
                {
                    randomNumber -= weight;
                }
            }
            
            element = default(TKey);
            return false;  
        }
    }
}
