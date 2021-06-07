using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace IbanNet.Extensions
{
    [DebuggerStepThrough]
    internal static class PartitionExtensions
    {
        /// <summary>
        /// Splits a given <paramref name="sequence" /> into partitions of specified <paramref name="size" />.
        /// </summary>
        /// <remarks>
        /// If the number of elements in the <paramref name="sequence" /> is not an exact multiple of <paramref name="size" />, the last partition of the returned partition set is smaller.
        /// </remarks>
        /// <typeparam name="TSource">The type of the sequence elements.</typeparam>
        /// <param name="sequence">The sequence to partition.</param>
        /// <param name="size">The size of each partition to split the <paramref name="sequence" /> into.</param>
        /// <returns>an enumerable of partitions</returns>
        public static IEnumerable<IEnumerable<TSource>> Partition<TSource>(this IEnumerable<TSource> sequence, int size)
        {
            if (sequence is null)
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
        /// Splits a given <paramref name="sequence" /> into partitions when encountering any of the <paramref name="chars" />.
        /// </summary>
        /// <param name="sequence">The sequence to partition.</param>
        /// <param name="chars">A list of markers to partition on.</param>
        /// <returns>an enumerable of partitions</returns>
        public static IEnumerable<string> PartitionOn(this string sequence, params char[] chars)
        {
            if (sequence is null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            if (chars is null || chars.Length == 0)
            {
                throw new ArgumentException(Resources.PartitionOn_At_least_one_character_to_partition_on_is_required, nameof(chars));
            }

            return PartitionOnIterator(sequence, chars.Contains);
        }

        /// <summary>
        /// Splits a given <paramref name="sequence" /> into partitions where the delegate <paramref name="when" /> returns true for a given character.
        /// </summary>
        /// <param name="sequence">The sequence to partition.</param>
        /// <param name="when">A delegate that determines when to partition.</param>
        /// <returns>an enumerable of partitions</returns>
#if USE_SPANS
        public static IEnumerable<string> PartitionOn(this ReadOnlySpan<char> sequence, Func<char, bool> when)
        {
#else
        public static IEnumerable<string> PartitionOn(this IEnumerable<char> sequence, Func<char, bool> when)
        {
            if (sequence is null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }
#endif

            if (when is null)
            {
                throw new ArgumentNullException(nameof(when));
            }

            return PartitionOnIterator(sequence, when);
        }

#if USE_SPANS
        private static IEnumerable<string> PartitionOnIterator(this ReadOnlySpan<char> sequence, Func<char, bool> when)
        {
            var partitions = new List<string>();
            var partition = new StringBuilder();

            int len = sequence.Length;
            for (var index = 0; index < len; index++)
            {
                char item = sequence[index];
                partition.Append(item);
                if (when(item))
                {
                    partitions.Add(partition.ToString());

                    partition.Clear();
                }
            }

            if (partition.Length > 0)
            {
                partitions.Add(partition.ToString());
            }

            return partitions;
        }
#else
        private static IEnumerable<string> PartitionOnIterator(this IEnumerable<char> sequence, Func<char, bool> when)
        {
            var partition = new StringBuilder();
            foreach (char item in sequence)
            {
                partition.Append(item);
                if (when(item))
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
#endif
    }
}
