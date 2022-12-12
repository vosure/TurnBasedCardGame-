using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility
{
	public static class CollectionExtensions
	{
		public static T GetRandom<T>(this T[] arr)
		{
			return arr[Random.Range(0, arr.Length)];
		}

		public static T GetRandom<T>(this List<T> list)
		{
			return list[Random.Range(0, list.Count)];
		}
        
		public static T GetRandom<T>(this IEnumerable<T> list)
		{
			return list.ToArray().GetRandom();
		}
	}
}