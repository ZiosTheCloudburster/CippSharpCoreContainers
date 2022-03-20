using System.Collections.Generic;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Purpose: abstract class for list-assets object to save array of 'things' as data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AListDataAsset<T> : ScriptableObject
    {
        [SerializeField] protected List<T> m_list = new List<T>();

        public T this[int index]
        {
            get { return m_list[index]; }
            set { m_list[index] = value; }
        }

        public int Count
        {
            get {  return m_list?.Count ?? 0;}
        }

        public List<T> List()
        {
            return m_list;
        }

        public void Clear()
        {
            if (m_list == null)
            {
                m_list = new List<T>();
            }
            
            m_list.Clear();
        }
        
        public T GetRandomElement()
        {
            return ArrayUtils.RandomElement(m_list);
        }
        
        public void Add(T element)
        {
            m_list.Add(element);
        }

        public void AddRange(IEnumerable<T> enumerable)
        {
            m_list.AddRange(enumerable);
        }
        
        public static implicit operator List<T> (AListDataAsset<T> data)
        {
            return data.m_list;
        }

        protected virtual void OnValidate()
        {
            
        }
    }

    /// <summary>
    /// Smart extensions for <see cref="AListDataAsset"/>
    /// </summary>
    public static class AListDataAssetExtensions
    {
        /// <summary>
        /// Is this ListDataAsset valid?
        /// </summary>
        /// <param name="asset"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsValid<T>(this AListDataAsset<T> asset)
        {
            return asset != null;
        }

        /// <summary>
        /// Is this ListDataAsset null or empty?
        /// </summary>
        /// <param name="asset"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this AListDataAsset<T> asset) 
        {
            return !IsValid<T>(asset) || ArrayUtils.IsNullOrEmpty((List<T>)asset);
        }
    }
}
