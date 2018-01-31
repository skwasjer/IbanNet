using System.Collections.Generic;

namespace IbanNet
{
	internal static class Extensions
	{
		/// <summary>
		/// Splits a given <paramref name="sequence"/> into partitions of specified <paramref name="size"/>.
		/// </summary>
		/// <remarks>
		/// If the number of elements in the <paramref name="sequence"/> is not an exact multiple of <paramref name="size"/>, the last partition of the returned partition set is smaller.
		/// </remarks>
		/// <typeparam name="T">The type of the sequence elements.</typeparam>
		/// <param name="sequence">The sequence to partition.</param>
		/// <param name="size">The size of each partition to split the <paramref name="sequence"/> into.</param>
		/// <returns>an enumerable of partitions</returns>
		public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> sequence, int size)
		{
			var partition = new List<T>(size);
			foreach (var item in sequence)
			{
				partition.Add(item);
				if (partition.Count == size)
				{
					yield return partition;
					partition = new List<T>(size);
				}
			}

			if (partition.Count > 0)
			{
				yield return partition;
			}
		}
	}
}
