using System.Collections.Generic;

namespace CippSharp.Core
{
	public interface ISimplePair<T, K>
	{
		T Key { get; }
		K Value { get; }

		KeyValuePair<T, K> ToKeyValuePair();
	}

	public static class SimplePairUtils
	{
		
	}
}
