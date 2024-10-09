using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace IbanNet.CodeGen.Wikipedia;

public static class Loader
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

    public static WikiResult GetWikiData()
    {
#pragma warning disable S1075
        var uri = new Uri("https://en.wikipedia.org/w/api.php?format=json&action=parse&page=International_Bank_Account_Number&section=16", UriKind.Absolute);
#pragma warning restore S1075
        HttpWebRequest req = WebRequest.CreateHttp(uri);
        using WebResponse response = req.GetResponse();
        using var ms = new MemoryStream();
        response.GetResponseStream()!.CopyTo(ms);
        byte[] buffer = ms.ToArray();

        WikiResponse? wikiResponse = JsonSerializer.Deserialize<WikiResponse>(buffer, JsonSerializerOptions);
        if (wikiResponse is null)
        {
            throw new InvalidOperationException("Unexpected response.");
        }

        var tableRegex = new Regex("(<table[^\\>]*?>.*<\\/table>)", RegexOptions.Singleline);
        Match tableMatch = tableRegex.Match(wikiResponse.Parse.Text["*"]);

        var doc = new HtmlDocument();
        doc.LoadHtml($"<div>${tableMatch.Value}</div>");

        IEnumerable<WikiRecord> records = doc.DocumentNode.SelectNodes("//tr")
            .GroupBy(e => e.ParentNode)
            .Select(g => g.Where(e => e.Element("td") != null))
            .Select(rows => rows.Select(r =>
            {
                var cells = r.Elements("td").ToList();
                return new WikiRecord
                {
                    CountryCode = cells[3].SelectSingleNode(".//code").InnerText.Substring(0, 2).Trim(),
                    EnglishName = cells[0].SelectSingleNode(".//a").InnerText.Trim(),
                    Pattern = cells[2].InnerText.Trim().Replace(" ", "")
                };
            }))
            .SelectMany(x => x);

        return new WikiResult(records, wikiResponse.Parse);
    }
}
