
using System.Collections.Generic;
using System.Linq;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Get loot table element
    /// </summary>
    public static class ContainersLootTable
    {
        private struct LootPair : ILootPair
        {
            public object Loot { get; private set; }
            public float LootChance { get; private set; }

            public LootPair(object loot, float lootChance)
            {
                this.Loot = loot;
                this.LootChance = lootChance;
            }

            public LootPair(ILootPair pair)
            {
                this.Loot = pair.Loot;
                this.LootChance = pair.LootChance;
            }
        }

        /// <summary>
        /// Drop Key from KeyValuePair with 'float'
        /// </summary>
        /// <param name="table"></param>
        /// <param name="element"></param>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public static bool DropElement<TKey>(ICollection<KeyValuePair<TKey, float>> table, out TKey element)
        {
            element = default(TKey);
            if (ArrayUtils.IsNullOrEmpty(table))
            {
                return false;
            }

            int count = table.Count;
            if (count == 1)
            {
                element = table.First().Key;
                return true;
            }

            return DropElementInternal(table.Select(p => (ILootPair)new LootPair(p.Key, p.Value)).ToArray(), out element);
        }

        /// <summary>
        /// Drop Element from PairContainer with 'float'
        /// </summary>
        /// <param name="table"></param>
        /// <param name="element"></param>
        /// <typeparam name="TPair"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public static bool DropElement<TPair, TKey>(ICollection<TPair> table, out TKey element) where TPair : PairContainer<TKey, float>
        {
            element = default(TKey);
            if (ArrayUtils.IsNullOrEmpty(table))
            {
                return false;
            }

            int count = table.Count;
            if (count == 1)
            {
                element = table.First().Key;
                return true;
            }
            
            return DropElementInternal(table.Select(p => (ILootPair)new LootPair(p.Key, p.Value)).ToArray(), out element);
        }
        
        /// <summary>
        /// Drop Element from LootPair Interface
        /// </summary>
        /// <param name="table"></param>
        /// <param name="element"></param>
        /// <typeparam name="TPair"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool DropElementFromLootPair<TPair, T>(ICollection<TPair> table, out T element) where TPair : ILootPair
        {
            element = default(T);
            if (ArrayUtils.IsNullOrEmpty(table))
            {
                return false;
            }

            int count = table.Count;
            if (count == 1)
            {
                return CastUtils.To(table.First().Loot, out element);
            }
            
            return DropElementInternal(table.Select(p => (ILootPair)p).ToArray(), out element);
        }

        
        private static bool DropElementInternal<T>(ILootPair[] table, out T element)
        {
            var array = table.OrderByDescending(l => l.LootChance).ToArray();
            double total = array.Select(k => k.LootChance).Sum();
            float randomNumber = UnityEngine.Random.Range(0.0000f, (float) total);
            foreach (var pair in array)
            {
                //randomNumber <= weight
                float weight = pair.LootChance;
                if (randomNumber <= weight)
                {
                    //award item
                    return CastUtils.To(pair.Loot, out element);
                }
                else
                {
                    randomNumber -= weight;
                }
            }

            element = default;
            return false;
        }
        

//        
//        /// <summary>
//        /// Generic Drop Table
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="element"></param>
//        /// <returns></returns>
//        public static bool DropElement(ICollection<ILootPair> table, out object element)
//        {
//            element = null;
//            if (ArrayUtils.IsNullOrEmpty(table))
//            {
//                return false;
//            }
//
//            int count = table.Count;
//            if (count == 1)
//            {
//                element = table.FirstOrDefault();
//                return true;
//            }
//                 
//            var array = table.OrderByDescending(l => l.LootChance).ToArray();
//            double total = array.Select(k => k.LootChance).Sum();
//            float randomNumber = UnityEngine.Random.Range(0.0000f, (float) total);
//            foreach (var pair in array)
//            {
//                //randomNumber <= weight
//                float weight = pair.LootChance;
//                if (randomNumber <= weight)
//                {
//                    //award item
//                    element = pair.Loot;
//                    return true;
//                }
//                else
//                {
//                    randomNumber -= weight;
//                }
//            }
//
//            return false;
//        }

        
//        /// <summary>
//        /// Generic Drop Table
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="element"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static bool DropElement<T>(ICollection<ILootPair> table, out T element)
//        {
//            element = default(T);
//            return DropElement(table, out object o) && CastUtils.To(o, out element);
//        }
//        
        
//        /// <summary>
//        /// Generic Drop Table
//        /// </summary>
//        /// <param name="table"></param>
//        /// <param name="element"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static bool DropElement<T>(ICollection<T> table, out T element)
//        {
//            element = default(T);
//            if (ArrayUtils.IsNullOrEmpty(table))
//            {
//                return false;
//            }
//            
//            int count = table.Count;
//            if (count == 1)
//            {
//                element = table.FirstOrDefault();
//                return true;
//            }
//            
//            List<LootPair> pairs = new List<LootPair>();
//            foreach (var o in table)
//            {
//                if (o is ILootPair lootPair)
//                {
//                    pairs.Add(new LootPair(lootPair.Loot, lootPair.LootChance));
//                }
//            }
//
//            return DropElementInternal(pairs, out object uncasted) && CastUtils.To(uncasted, out element);
//        }
//
//        private static bool DropElementInternal(List<LootPair> table, out object element)
//        {
//
//        }

//        /// <summary>
//        /// From a loot table of pairs, get an element 
//        /// </summary>
//        /// <param name="table">made of T elements and floats values</param>
//        /// <param name="element"></param>
//        /// <typeparam name="TKey"></typeparam>
//        /// <returns></returns>
//        public static bool DropElement<TKey>(ICollection<KeyValuePair<TKey, float>> table, out TKey element)
//        {
//            if (table == null || table.Count == 0)
//            {
//                element = default(TKey);
//                return false;
//            }
//            
//            if (table.Count == 1)
//            {
//                element = table.FirstOrDefault().Key;
//                return true;
//            }
//            
//            var array = table.OrderByDescending(k => k.Key).ToArray();
//            double total = array.Select(k => k.Value).Sum();
//            float randomNumber = UnityEngine.Random.Range(0.0000f, (float) total);
//            
//            foreach (var pair in array)
//            {
//                //randomNumber <= weight
//                float weight = pair.Value;
//                if (randomNumber <= weight)
//                {
//                    //award item
//                    element = pair.Key;
//                    return true;
//                }
//                else
//                {
//                    randomNumber -= weight;
//                }
//            }
//            
//            element = default(TKey);
//            return false;    
//        }

//        /// <summary>
//        /// From a loot table of pairs, get an element 
//        /// </summary>
//        /// <param name="table">made of T elements and floats values </param>
//        /// <param name="element"></param>
//        /// /// <typeparam name="TPair"></typeparam>
//        /// <typeparam name="TKey"></typeparam>
//        /// <returns></returns>
//        public static bool DropElement<TPair, TKey>(ICollection<TPair> table, out TKey element) where TPair : PairContainer<TKey, float>
//        {
//            if (table == null || table.Count == 0)
//            {
//                element = default(TKey);
//                return false;
//            }
//            
//            if (table.Count == 1)
//            {
//                element = table.FirstOrDefault().Key;
//                return true;
//            }
//            
//            var array = table.OrderByDescending(k => k.Key).ToArray();
//            double total = array.Select(k => k.Value).Sum();
//            float randomNumber = UnityEngine.Random.Range(0.0000f, (float) total);
//            
//            foreach (var pair in array)
//            {
//                //randomNumber <= weight
//                float weight = pair.Value;
//                if (randomNumber <= weight)
//                {
//                    //award item
//                    element = pair.Key;
//                    return true;
//                }
//                else
//                {
//                    randomNumber -= weight;
//                }
//            }
//            
//            element = default(TKey);
//            return false;  
//        }
    }
}
