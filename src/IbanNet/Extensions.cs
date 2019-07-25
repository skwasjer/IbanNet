using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
			foreach (TSource item in sequence)
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

		/// <summary>
		/// Splits a given <paramref name="sequence"/> into partitions when encountering any of the <paramref name="markers"/>.
		/// </summary>
		/// <param name="sequence">The sequence to partition.</param>
		/// <param name="markers">A list of markers to partition on.</param>
		/// <returns>an enumerable of partitions</returns>
		public static IEnumerable<string> PartitionOn(this string sequence, params char[] markers)
		{
			if (sequence == null)
			{
				throw new ArgumentNullException(nameof(sequence));
			}

			if (markers == null || markers.Length == 0)
			{
				throw new ArgumentException("At least one marker is required.", nameof(markers));
			}

			return PartitionOnIterator(sequence, markers);
		}

		private static IEnumerable<string> PartitionOnIterator(this string sequence, params char[] markers)
		{
			var partition = new StringBuilder();
			foreach (char item in sequence)
			{
				partition.Append(item);
				if (markers.Contains(item))
				{
					yield return partition.ToString();
					partition.Clear();
				}
			}

			if (partition.Length > 0)
			{
				yield return partition.ToString();
			}
		}

		public static string GetCountryCode(this string value)
		{
			if (value == null || value.Length < 2)
			{
				return null;
			}

			return value.Substring(0, 2).ToUpperInvariant();
		}
	}
}
