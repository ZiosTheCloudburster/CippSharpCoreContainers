using System;
using UnityEngine;

namespace CippSharp.Core.Containers
{
    /// <summary>
    /// Purpose: consider this like an abstract class for typed resolvers.
    /// It's a delegate container. Customizable :D
    /// 
    /// Resolvers are 'other classes' that should handle specific methods
    /// or peculiar setups that may differs once by once  
    /// </summary>
    public abstract class AResolverAsset<T> : AResolverAssetBase, IContainer<Resolve<T>>
    {
        /// <summary>
        /// Resolve!
        /// </summary>
        /// <param name="parameter"></param>
        /// <typeparam name="K"></typeparam>
        /// <returns></returns>
        public abstract bool Resolve<K>(ref K parameter) where K : T;

        /// <summary>
        /// Delegate, Resolve!
        /// </summary>
        /// <returns></returns>
        public virtual Resolve<K> GetResolveDelegate<K>() where K : T
        {
            return (ref K p) => this.Resolve(ref p);
        }
        
        #region IContainerBase and IContainer Implementation
        
        /// <summary>
        /// The type of the stored value
        /// </summary>
        public override Type ContainerType => typeof(Resolve<T>);

        /// <summary>
        /// Retrieve the contained element
        /// </summary>
        /// <returns></returns>
        public override object GetValueRaw()
        {
            return GetResolveDelegate<T>();
        }
        
        /// <summary>
        /// The value is a delegate!
        /// </summary>
        /// <returns></returns>
        public Resolve<T> GetValue()
        {
            return GetResolveDelegate<T>();
        }
        
        /// <summary>
        /// Read/Write on data/value
        /// </summary>
        /// <param name="access"></param>
        public override void Access(GenericAccessDelegate access)
        {
            var d = (object)GetResolveDelegate<T>();
            access.Invoke(ref d); 
        }

        /// <summary>
        /// Read/Write on data/value
        /// </summary>
        /// <param name="access"></param>
        public void Access(AccessDelegate<Resolve<T>> access)
        {
            var d = GetResolveDelegate<T>();
            access.Invoke(ref d);
        }
        
        /// <summary>
        /// Predicate on data/value
        /// </summary>
        /// <param name="access"></param>
        public override bool Check(PredicateGenericAccessDelegate access)
        {
            var d = (object)GetResolveDelegate<T>();
            return access.Invoke(ref d); 
        }
        
        /// <summary>
        /// Predicate on data/value
        /// </summary>
        /// <param name="access"></param>
        public bool Check(PredicateAccessDelegate<Resolve<T>> access)
        {
            var d = GetResolveDelegate<T>();
            return access.Invoke(ref d);
        }
        

        public void Set(Resolve<T> newValue)
        {
            //Sorry, I don't think I'm ready to reverse engineering a class
            //just to do this during runtime
            Debug.LogError("Not supported!", this);
        }
        
        /// <summary>
        /// Set the contained element
        /// </summary>
        /// <param name="newValue"></param>
        public override void Set(object newValue)
        {
            Debug.LogError("Not supported!", this);
        }

        #endregion
        
        #region Cast Utils

        /// <summary>
        /// Cast data to K
        /// </summary>
        /// <returns></returns>
        public virtual K To<K>()
        {
            var d = GetResolveDelegate<T>();
            if (CastUtils.To<K>(d, out K tmp))
            {
                return tmp;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        /// <summary>
        /// Cast data to K. If it fails retrieve default value
        /// </summary>
        /// <returns></returns>
        public virtual K ToOrDefault<K>()
        {
            var d = GetResolveDelegate<T>();
            return CastUtils.ToOrDefault<K>(d);
        }
        
        #endregion
        
        #region Operators
        
        public static implicit operator Resolve<T>(AResolverAsset<T> dataContainer)
        {
            return dataContainer.GetResolveDelegate<T>();
        }
        
        #endregion
    }
}
