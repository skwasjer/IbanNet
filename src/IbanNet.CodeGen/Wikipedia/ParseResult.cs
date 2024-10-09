namespace IbanNet.CodeGen.Wikipedia;

public sealed record ParseResult
{
    public int PageId { get; init; }
    public int RevId { get; init; }
    public Dictionary<string, string> Text { get; init; } = [];
}
