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

		/// <summary>Returns elements from a sequence until a specified condition is true.</summary>
		/// <param name="source">A sequence to return elements from.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <typeparam name="TSource">The type of the elements of <paramref name="source" />.</typeparam>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> that contains the elements from the input sequence that occur before and including the element at which the test no longer passes.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		/// <paramref name="source" /> or <paramref name="predicate" /> is <see langword="null" />.</exception>
		public static IEnumerable<TSource> TakeUntil<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source));
			}
			if (predicate == null)
			{
				throw new ArgumentNullException(nameof(predicate));
			}

			return TakeUntilIterator(source, predicate);
		}

		private static IEnumerable<TSource> TakeUntilIterator<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
		{
			foreach (TSource item in source)
			{
				yield return item;
				if (predicate(item))
				{
					break;
				}
			}
		}
	}
}
