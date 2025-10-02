using System.Text;

namespace IbanNet.CodeGen.Swift;

public class SwiftCsvReaderTests
{
    static SwiftCsvReaderTests()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    [Theory]
    [MemberData(nameof(GetTestCsvData))]
#pragma warning disable xUnit1026 // Theory methods should use all of their parameters
    public void Given_that_csv_is_valid_when_getting_records_it_should_return_all(string filename, string testCsvData)
#pragma warning restore xUnit1026 // Theory methods should use all of their parameters
    {
        using var reader = new StringReader(testCsvData);
        var sut = new SwiftCsvReader(reader);

        var csvRecords = sut.GetRecords<SwiftCsvRecord>()
            .ToList();
        csvRecords.Should()
            .NotBeNullOrEmpty()
            .And.HaveCountGreaterThan(75);

        SwiftCsvRecord record = csvRecords.Should()
            .ContainSingle(x => x.CountryCode == "NL", "it should have this item")
            .Which;
        record.Iban.Length.Should().Be(18);
    }

    public static IEnumerable<object[]> GetTestCsvData()
    {
        string dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            "..",
            "..",
            "..",
            "..",
            "..",
            "src",
            "IbanNet",
            "Registry",
            "Swift",
            "Files"
        );
        foreach (string registryPath in
                 Directory.GetFiles(dataFolder)
                     .Where(s => s.EndsWith(".txt"))
                     .OrderBy(s => s))
        {
            yield return [Path.GetFileName(registryPath), File.ReadAllText(registryPath, Encoding.GetEncoding(1252))];
        }
    }
}
