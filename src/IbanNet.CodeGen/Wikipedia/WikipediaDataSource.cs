using System.Text;
using IbanNet.CodeGen.Swift;
using IbanNet.Registry.Patterns;

namespace IbanNet.CodeGen.Wikipedia;

internal sealed class WikipediaDataSource : IRegistryDataSource
{
    public bool IsDataSource(string path)
    {
        string? dir = Path.GetDirectoryName(path);
        string? fn = Path.GetFileName(path);
        string? ext = Path.GetExtension(fn);
        return dir is not null
         && fn is not null
         && ext == ".json"
         && fn.StartsWith("api-result-", StringComparison.InvariantCulture)
         && dir.Contains("Wikipedia");
    }

    public SwiftCsvRecord[] GetCountryDefinitions(string text)
    {
        var tokenizer = new WikipediaPatternTokenizer();
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(text));
        WikiResult wikiData = Loader.Read(stream);
        return wikiData.Records.Select(r =>
            {
                int bbanLength = tokenizer.Tokenize(r.Pattern).Sum(t => t.MaxLength);
                return new SwiftCsvRecord
                {
                    CountryCode = r.CountryCode,
                    EnglishName = r.EnglishName,
                    Iban = new IbanCsvData
                    {
                        Pattern = r.Pattern,
                        Length = bbanLength + 4,
                        Tokenizer = new PrependCountryCodeWikipediaPatternTokenizer(r.CountryCode)
                    },
                    Bban = new BbanCsvData
                    {
                        Pattern = r.Pattern,
                        Length = bbanLength,
                        Tokenizer = tokenizer
                    }
                };
            })
            .ToArray();
    }

    private sealed class PrependCountryCodeWikipediaPatternTokenizer(string countryCode) : WikipediaPatternTokenizer
    {
        public override IEnumerable<PatternToken> Tokenize(IEnumerable<char> input)
        {
            yield return new PatternToken(countryCode);
            yield return new PatternToken(AsciiCategory.Digit, 2);

            foreach (PatternToken token in base.Tokenize(input))
            {
                yield return token;
            }
        }
    }
}
