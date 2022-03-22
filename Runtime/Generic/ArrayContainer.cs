using System;

namespace CippSharp.Core.Containers
{
   /// <summary>
   /// Consider this like an abstract class 
   /// </summary>
   /// <typeparam name="T"></typeparam>
   [Serializable]
   public class ArrayContainer<T> : AArrayContainer<T>
   {
//      [SerializeField] public T[] array = new T[0];
//
//      public T this[int index]
//      {
//         get { return array[index]; }
//         set { array[index] = value; }
//      }
//      
//      public override Type ContainerType => typeof(T[]);
      
      public ArrayContainer()
      {
         this.value = new T[0];
      }

      public ArrayContainer(T element)
      {
         this.value = new[] {element};
      }
      
      private ArrayContainer(T[] array)
      {
         this.value = array;
      }

//      public T[] GetElements()
//      {
//         return array.ToArray();
//      }

      public int Length => Count;

      #region Operators

      public static implicit operator T[](ArrayContainer<T> container)
      {
         return container.array;
      }
      
      public static implicit operator ArrayContainer<T>(T[] array)
      {
         return new ArrayContainer<T>(array);
      }
      
      #endregion
      
//      IEnumerator IEnumerable.GetEnumerator()
//      {
//         return array.GetEnumerator();
//      }
   }
}
