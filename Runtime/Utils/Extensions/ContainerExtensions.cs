using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Containers
{
    public static class ContainerExtensions
    {
        /// <summary>
        /// Is this container valid?
        /// </summary>
        /// <param name="container"></param>
        /// <returns></returns>
        public static bool IsValid(this IContainerBase container)
        {
            return container != null;
        }

        /// <summary>
        /// Try Access on this container for data
        /// </summary>
        /// <param name="container"></param>
        /// <param name="access"></param>
        /// <param name="debug"></param>
        /// <typeparam name="T"></typeparam>
        public static bool TryAccess<T>(this IContainer<T> container, AccessDelegate<T> access, bool debug = false)
        {
            Object context = debug ? container as Object : null;
            if (IsValid(container))
            {
                try
                {
                    container.Access(access);
                    return true;
                }
                catch (Exception e)
                {
                    if (debug)
                    {
                        string logName = StringUtils.LogName(context);
                        Debug.LogError(logName + $"Caught exception: {e.Message}.", context);
                    }
                    return false;
                }
            }
            else
            {
                if (debug)
                {
                    Debug.LogError($"{nameof(context)} is null!", context);
                }

                return false;
            }
        }
    }
}
