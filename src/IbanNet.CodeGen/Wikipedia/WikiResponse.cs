namespace IbanNet.CodeGen.Wikipedia;

public sealed record WikiResponse
{
    public ParseResult Parse { get; init; } = default!;
}
