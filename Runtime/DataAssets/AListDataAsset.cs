using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Purpose: abstract class for list-assets object to save array of 'things' as data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AListDataAsset<T> : ADataAsset<List<T>>, ICollectionContainer<List<T>, T>
    {
//        [SerializeField] protected List<T> m_list = new List<T>();

        #region ICollectionContainer Implementation
        
        public override Type ContainerType => typeof(List<T>);
        
        /// <summary>
        /// Stored Collection
        /// </summary>
        public virtual ICollection<T> Collection => value;

        /// <summary>
        /// this has minus 'l' as old name reference
        /// </summary>
        public virtual List<T> list
        {
            get => this.value;
            set => this.value = value;
        }
        
        /// <summary>
        /// Element at index
        /// </summary>
        /// <param name="index"></param>
        public virtual T this[int index]
        {
            get => this.value[index];
            set => this.value[index]= value;
        }

        /// <summary>
        /// Count of elements in this collection
        /// </summary>
        public virtual int Count => value?.Count ?? 0;

        public virtual void Clear()
        {
            if (value == null)
            {
                value = new List<T>();
            }
            
            value.Clear();
        }
        
        public virtual void Add(T element)
        {
            value.Add(element);
        }

        public virtual void AddRange(IEnumerable<T> enumerable)
        {
            value.AddRange(enumerable);
        }
        
        public virtual bool Contains(T element)
        {
            return value.Contains(element);
        }
        
        public virtual int IndexOf(T element)
        {
            return value.IndexOf(element);
        }

        public virtual bool Find(Predicate<T> predicate, out T result)
        {
            return ArrayUtils.Find(value, predicate, out result);
        }

        public virtual bool Remove(T element)
        {
            return value.Remove(element);
        }

        public virtual void RemoveAt(int index)
        {
            value.RemoveAt(index);
        }

        public virtual T[] ToArray()
        {
            return value.ToArray();
        }
        
        public virtual IEnumerator<T> GetEnumerator()
        {
            return value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return value.GetEnumerator();
        }
        
        #endregion
        
//        public T this[int index]
//        {
//            get { return m_list[index]; }
//            set { m_list[index] = value; }
//        }
//
//        public int Count
//        {
//            get {  return m_list?.Count ?? 0;}
//        }
//
//        public List<T> List()
//        {
//            return m_list;
//        }
//
//        public void Clear()
//        {
//            if (m_list == null)
//            {
//                m_list = new List<T>();
//            }
//            
//            m_list.Clear();
//        }
//        
//        public T GetRandomElement()
//        {
//            return ArrayUtils.RandomElement(m_list);
//        }
//        
//        public void Add(T element)
//        {
//            m_list.Add(element);
//        }
//
//        public void AddRange(IEnumerable<T> enumerable)
//        {
//            m_list.AddRange(enumerable);
//        }
//        
//        public static implicit operator List<T> (AListDataAsset<T> data)
//        {
//            return data.m_list;
//        }
//        
//        #region ICollectionContainer Implementation
//        
//        public override Type ContainerType => typeof(List<T>);
//        
//        /// <summary>
//        /// Stored Collection
//        /// </summary>
//        public virtual ICollection<T> Collection => value;
//
//        /// <summary>
//        /// this has minus 'l' as old name reference
//        /// </summary>
//        public virtual List<T> list
//        {
//            get => this.value;
//            set => this.value = value;
//        }
//        
//        /// <summary>
//        /// Element at index
//        /// </summary>
//        /// <param name="index"></param>
//        public virtual T this[int index]
//        {
//            get => this.value[index];
//            set => this.value[index]= value;
//        }
//
//        /// <summary>
//        /// Count of elements in this collection
//        /// </summary>
//        public virtual int Count => value?.Count ?? 0;
//
//        public virtual void Clear()
//        {
//            if (value == null)
//            {
//                value = new List<T>();
//            }
//            
//            value.Clear();
//        }
//        
//        public virtual void Add(T element)
//        {
//            value.Add(element);
//        }
//
//        public virtual void AddRange(IEnumerable<T> enumerable)
//        {
//            value.AddRange(enumerable);
//        }
//        
//        public virtual bool Contains(T element)
//        {
//            return value.Contains(element);
//        }
//        
//        public virtual int IndexOf(T element)
//        {
//            return value.IndexOf(element);
//        }
//
//        public virtual bool Find(Predicate<T> predicate, out T result)
//        {
//            return ArrayUtils.Find(value, predicate, out result);
//        }
//
//        public virtual bool Remove(T element)
//        {
//            return value.Remove(element);
//        }
//
//        public virtual void RemoveAt(int index)
//        {
//            value.RemoveAt(index);
//        }
//
//        public virtual T[] ToArray()
//        {
//            return value.ToArray();
//        }
//        
//        public virtual IEnumerator<T> GetEnumerator()
//        {
//            return value.GetEnumerator();
//        }
//
//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return value.GetEnumerator();
//        }
//        
//        #endregion
    }

//    /// <summary>
//    /// Smart extensions for <see cref="AListDataAsset"/>
//    /// </summary>
//    public static class AListDataAssetExtensions
//    {
//        /// <summary>
//        /// Is this ListDataAsset valid?
//        /// </summary>
//        /// <param name="asset"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static bool IsValid<T>(this AListDataAsset<T> asset)
//        {
//            return asset != null;
//        }
//
//        /// <summary>
//        /// Is this ListDataAsset null or empty?
//        /// </summary>
//        /// <param name="asset"></param>
//        /// <typeparam name="T"></typeparam>
//        /// <returns></returns>
//        public static bool IsNullOrEmpty<T>(this AListDataAsset<T> asset) 
//        {
//            return !IsValid<T>(asset) || ArrayUtils.IsNullOrEmpty((List<T>)asset);
//        }
//    }
}
