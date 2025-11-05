#if NET8_0_OR_GREATER
using System.Runtime.InteropServices;

namespace IbanNet.Internal;

internal static class ListsMarshal
{
    internal static Span<T> AsSpan<T>(IEnumerable<T>? list)
    {
        return list switch
        {
            null => default,
            ICollection<T> { Count: 0 } => default,
            T[] arr => arr.AsSpan(),
            List<T> lst => CollectionsMarshal.AsSpan(lst),
            // Special case, JIT can optimize to a special 'single item list', in which case instead of iterating/copying we create a span over a single item array.
            IList<T> { Count: 1 } ilst => new Span<T>([ilst[0]]),
            // Unoptimized, we have to copy.
            _ => list.ToArray().AsSpan()
        };
    }
}
#endif
