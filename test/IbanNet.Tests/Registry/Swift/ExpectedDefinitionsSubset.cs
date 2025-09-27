using System.Collections;
using IbanNet.Registry.Patterns;

namespace IbanNet.Registry.Swift;

internal sealed class ExpectedDefinitionsSubset : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var tokenizer = new SwiftPatternTokenizer();
        yield return
        [
            new IbanCountry("AD")
            {
                DisplayName = "Andorra",
                NativeName = "Andorra",
                EnglishName = "Andorra",
                Iban = new PatternDescriptor(new TestPattern("AD2!n4!n4!n12!c", tokenizer)) { Example = "AD1200012030200359100100" },
                Bban = new PatternDescriptor(new TestPattern("4!n4!n12!c", tokenizer), 4) { Example = "00012030200359100100" },
                Bank = new PatternDescriptor(new TestPattern("4!n", tokenizer), 4) { Example = "0001" },
                Branch = new PatternDescriptor(new TestPattern("4!n", tokenizer), 8) { Example = "2030" },
                Sepa = new SepaInfo { IsMember = true },
                DomesticAccountNumberExample = "2030200359100100",
                LastUpdatedDate = new DateTimeOffset(2021, 3, 1, 0, 0, 0, TimeSpan.Zero),
                EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
            }
        ];

        yield return
        [
            new IbanCountry("NL")
            {
                NativeName = "Nederland",
                EnglishName = "Netherlands (The)",
                Iban = new PatternDescriptor(new TestPattern("NL2!n4!a10!n", tokenizer)) { Example = "NL91ABNA0417164300" },
                Bban = new PatternDescriptor(new TestPattern("4!a10!n", tokenizer), 4) { Example = "ABNA0417164300" },
                Bank = new PatternDescriptor(new TestPattern("4!a", tokenizer), 4) { Example = "ABNA" },
                Sepa = new SepaInfo { IsMember = true },
                DomesticAccountNumberExample = "041 71 64 300",
                LastUpdatedDate = new DateTimeOffset(2020, 9, 1, 0, 0, 0, TimeSpan.Zero),
                EffectiveDate = new DateTimeOffset(2007, 4, 1, 0, 0, 0, TimeSpan.Zero)
            }
        ];

        yield return
        [
            new IbanCountry("SO")
            {
                NativeName = "الصومال",
                EnglishName = "Somalia",
                Iban = new PatternDescriptor(new TestPattern("SO2!n4!n3!n12!n", tokenizer)) { Example = "SO211000001001000100141" },
                Bban = new PatternDescriptor(new TestPattern("4!n3!n12!n", tokenizer), 4) { Example = "1000001001000100141" },
                Bank = new PatternDescriptor(new TestPattern("4!n", tokenizer), 4) { Example = "1000" },
                Branch = new PatternDescriptor(new TestPattern("3!n", tokenizer), 8) { Example = "001" },
                Sepa = new SepaInfo { IsMember = false },
                DomesticAccountNumberExample = "001000100141",
                LastUpdatedDate = new DateTimeOffset(2023, 2, 1, 0, 0, 0, TimeSpan.Zero),
                EffectiveDate = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero)
            }
        ];

        yield return
        [
            new IbanCountry("XK")
            {
                DisplayName = "Kosovë",
                NativeName = "Kosovë",
                EnglishName = "Kosovo",
                Iban = new PatternDescriptor(new TestPattern("XK2!n4!n10!n2!n", tokenizer)) { Example = "XK051212012345678906" },
                Bban = new PatternDescriptor(new TestPattern("4!n10!n2!n", tokenizer), 4) { Example = "1212012345678906" },
                Bank = new PatternDescriptor(new TestPattern("2!n", tokenizer), 4) { Example = "12" },
                Branch = new PatternDescriptor(new TestPattern("2!n", tokenizer), 6) { Example = "12" },
                Sepa = new SepaInfo { IsMember = false },
                DomesticAccountNumberExample = "1212 0123456789 06",
                LastUpdatedDate = new DateTimeOffset(2016, 9, 1, 0, 0, 0, TimeSpan.Zero),
                EffectiveDate = new DateTimeOffset(2014, 9, 1, 0, 0, 0, TimeSpan.Zero)
            }
        ];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
