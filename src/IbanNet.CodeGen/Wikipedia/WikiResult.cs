namespace IbanNet.CodeGen.Wikipedia;

public record WikiResult(IEnumerable<WikiRecord> Records, ParseResult Parse);
