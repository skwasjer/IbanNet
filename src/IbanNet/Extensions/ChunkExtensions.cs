using System.Diagnostics;

namespace IbanNet.Extensions;

[DebuggerStepThrough]
internal static class ChunkExtensions
{
#if !NET6_0_OR_GREATER
    /// <summary>
    /// Split the elements of a sequence into chunks of size at most <paramref name="size" />.
    /// </summary>
    /// <remarks>
    /// Every chunk except the last will be of size <paramref name="size" />.
    /// The last chunk will contain the remaining elements and may be of a smaller size.
    /// </remarks>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <param name="source">The source sequence.</param>
    /// <param name="size">The size of each chunk to split the <paramref name="source" /> into.</param>
    /// <returns>an enumerable of chunks</returns>
    public static IEnumerable<IEnumerable<TSource>> Chunk<TSource>(this IEnumerable<TSource> source, int size)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (size <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(size));
        }

        return ChunkIterator(source, size);
    }

    private static IEnumerable<IEnumerable<TSource>> ChunkIterator<TSource>(this IEnumerable<TSource> source, int size)
    {
        var chunks = new List<TSource>(size);
        foreach (TSource item in source)
        {
            chunks.Add(item);
            if (chunks.Count == size)
            {
                yield return chunks;

                chunks = new List<TSource>(size);
            }
        }

        if (chunks.Count > 0)
        {
            yield return chunks;
        }
    }
#endif

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

        return PartitionOn(sequence, chars.Contains);
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

        int len = sequence.Length;
        int startPos = 0;
        int count = 0;
        for (int index = 0; index < len; index++)
        {
            char item = sequence[index];
            count++;
            if (!when(item))
            {
                continue;
            }

            partitions.Add(sequence.Slice(startPos, count).ToString());
            startPos += count;
            count = 0;
        }

        if (count > 0)
        {
            partitions.Add(sequence.Slice(startPos, count).ToString());
        }

        return partitions;
    }
#else
    private static IEnumerable<string> PartitionOnIterator(this IEnumerable<char> sequence, Func<char, bool> when)
    {
        var partition = new System.Text.StringBuilder();
        foreach (char item in sequence)
        {
            partition.Append(item);
            if (!when(item))
            {
                continue;
            }

            yield return partition.ToString();

            partition.Clear();
        }

        if (partition.Length > 0)
        {
            yield return partition.ToString();
        }
    }
#endif
}
