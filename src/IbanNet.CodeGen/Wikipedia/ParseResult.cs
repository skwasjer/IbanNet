namespace IbanNet.CodeGen.Wikipedia;

public class ParseResult
{
    public int PageId { get; set; }
    public int RevId { get; set; }
    public Dictionary<string, string> Text { get; set; }
}
