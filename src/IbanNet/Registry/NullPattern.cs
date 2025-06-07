using IbanNet.Registry.Patterns;

namespace IbanNet.Registry;

internal class NullPattern : Pattern
{
    public static readonly Pattern Instance = new NullPattern();

    private NullPattern()
        : base([])
    {
    }
}
