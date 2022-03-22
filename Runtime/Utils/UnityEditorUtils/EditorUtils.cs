#if UNITY_EDITOR
//
// Author: Alessandro Salani (Cippo)
//
using System;
using Object = UnityEngine.Object;

namespace CippSharp.Core.Containers
{
	public static class EditorUtils
	{
		[Obsolete("2021/08/14 → moved to EditorObjectUtils. This will be removed in future versions.")]
		public static void SetDirty(Object target)
		{
			return;
		}

		[Obsolete("2021/08/14 → moved to SerializedObjectUtils. This will be removed in future versions.")]
		public static Object[] GetTargetObjects<T>(T serializedObject)
		{
			return null;
		}
	}
}
#endif
