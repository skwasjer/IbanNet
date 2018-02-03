using System;
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
		/// <typeparam name="TSource">The type of the sequence elements.</typeparam>
		/// <param name="sequence">The sequence to partition.</param>
		/// <param name="size">The size of each partition to split the <paramref name="sequence"/> into.</param>
		/// <returns>an enumerable of partitions</returns>
		public static IEnumerable<IEnumerable<TSource>> Partition<TSource>(this IEnumerable<TSource> sequence, int size)
		{
			if (sequence == null)
			{
				throw new ArgumentNullException(nameof(sequence));
			}

			if (size <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(size));
			}

			return PartitionIterator(sequence, size);
		}

		private static IEnumerable<IEnumerable<TSource>> PartitionIterator<TSource>(this IEnumerable<TSource> sequence, int size)
		{
			var partition = new List<TSource>(size);
			foreach (var item in sequence)
			{
				partition.Add(item);
				if (partition.Count == size)
				{
					yield return partition;
					partition = new List<TSource>(size);
				}
			}

			if (partition.Count > 0)
			{
				yield return partition;
			}
		}
	}
}
