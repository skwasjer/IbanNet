using CsvHelper;
using CsvHelper.Configuration;

namespace IbanNet.CodeGen.Swift;

internal sealed class TransposingCsvTextReader : StringReader
{
    public TransposingCsvTextReader(TextReader reader, CsvConfiguration configuration)
        : base(Transpose(reader, configuration))
    {
    }

    private static string Transpose(TextReader source, CsvConfiguration configuration)
    {
        using var csvReader = new CsvReader(source, configuration);
        var records = new List<string[]>();
        while (csvReader.Read())
        {
            if (csvReader.Context.Parser.Record is not null)
            {
                records.Add(csvReader.Context.Parser.Record);
            }
        }

        // Skip column and row headers.
        string[][] transposedRecords = Transpose(records);
        using var sw = new StringWriter();
        using var csvWriter = new CsvWriter(sw, configuration);
        for (int x = 0; x < transposedRecords.Length; x += 1)
        {
            for (int y = 0; y < transposedRecords[x].Length; y += 1)
            {
                csvWriter.WriteField(transposedRecords[x][y]);
            }

            csvWriter.NextRecord();
        }

        return sw.ToString();
    }

    /// <summary>
    /// Transposes the input <paramref name="input" /> to a 2D-array.
    /// </summary>
    private static string[][] Transpose(IReadOnlyList<string[]> input)
    {
        string[][] buffer = Enumerable.Repeat(1, input[0].Length)
            .Select(_ => new string[input.Count])
            .ToArray();

        for (int i = 0; i < input.Count; i++)
        {
            string[] line = input[i];
            for (int j = 0; j < line.Length; j++)
            {
                buffer[j][i] = input[i][j];
            }
        }

        return buffer;
    }
}
