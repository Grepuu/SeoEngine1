using HtmlAgilityPack;

namespace ResponseDataCollector.ServicesLibrary.Services;

public class ResponsePenknifeWebsiteText
{
    public static WebsiteTextStruct GetWebsiteText(Uri uri)
    {
        return GetData(new HtmlWeb().Load(uri));
    }

    public static WebsiteTextStruct GetWebsiteText(string content)
    {
        HtmlDocument _doc = new HtmlDocument();
        _doc.LoadHtml(content);

        return GetData(_doc);
    }

    private static WebsiteTextStruct GetData(HtmlDocument doc)
    {
        var _wts = new WebsiteTextStruct();
        _wts.Paragraphs = doc.DocumentNode.Descendants("p").Count();

        var _nodes = new List<HtmlNode>();
        _nodes.AddRange(doc.DocumentNode.SelectNodes("//text()"));

        foreach (var item in _nodes)
        {
            _wts.WordsAmount += item.InnerText.Split(' ').Length;
        }

        return _wts;
    }

    public struct WebsiteTextStruct
    {
        public int WordsAmount;

        public int Paragraphs;
    }
}
