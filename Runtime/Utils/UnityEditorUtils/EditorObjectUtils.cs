#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Containers
{
    public static class EditorObjectUtils
    {
	    #region Set Dirty
		
		/// <summary>
		/// Set target object dirty.
		/// </summary>
		/// <param name="target"></param>
		public static void SetDirty(Object target)
		{
			if (target == null)
			{
				return;
			}
			
			EditorUtility.SetDirty(target);
		}

		/// <summary>
		/// Set target objects dirty.
		/// </summary>
		/// <param name="targets"></param>
		public static void SetDirty(Object[] targets)
		{
			SetDirtyInternal(targets, null, false);
		}
		
		/// <summary>
		/// Set target objects dirty.
		/// </summary>
		/// <param name="targets"></param>
		/// <param name="debugContext"></param>
		public static void SetDirty(Object[] targets, Object debugContext)
		{
			SetDirtyInternal(targets, debugContext, true);
		}

		/// <summary>
		/// Set target objects dirty.
		/// </summary>
		/// <param name="targets"></param>
		/// <param name="debugContext"></param>
		/// <param name="debug"></param>
		public static void SetDirtyInternal(Object[] targets, Object debugContext, bool debug)
		{
			string logName = debug ? StringUtils.LogName(debugContext) : string.Empty;
			if (!Application.isEditor)
			{
				if (debug)
				{
					Debug.LogWarning(logName + nameof(SetDirty)+"() method works only in editor.", debugContext);
				}
				return;
			}
			
#if UNITY_EDITOR
			if (ArrayUtils.IsNullOrEmpty(targets))
			{
				return;
			}
			
			foreach (var o in targets)
			{
				EditorUtility.SetDirty(o);
			}
#endif
		}
		
		#endregion
    }
}
#endif

