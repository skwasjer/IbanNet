using IbanNet.CodeGen.Swift.Patches;

namespace IbanNet.CodeGen.Swift;

internal sealed class SwiftDataSource : IRegistryDataSource
{
    public bool IsDataSource(string path)
    {
        string? fn = Path.GetFileName(path);
        string? ext = Path.GetExtension(fn);
        return fn is not null
         && fn.StartsWith("swift_iban_registry", StringComparison.InvariantCulture)
         && fn.IndexOf(".r", StringComparison.Ordinal) != -1
         && ext == ".txt";
    }

    public SwiftCsvRecord[] GetCountryDefinitions(string text)
    {
        var tokenizer = new SwiftPatternTokenizer();
        using var csv = new SwiftCsvReader(new StringReader(text));
        return csv.GetRecords<SwiftCsvRecord>()
            .Select(RecordPatcher.ApplyAll)
            .Select(record =>
            {
                record.Iban.Tokenizer = tokenizer;
                record.Bban.Tokenizer = tokenizer;
                record.Bank.Tokenizer = tokenizer;
                record.Branch.Tokenizer = tokenizer;
                return record;
            })
            .ToArray();
    }
}
