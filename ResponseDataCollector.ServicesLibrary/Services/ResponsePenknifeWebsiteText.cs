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

    public static AnylyzedTextStruct CheckText(Uri uri, string text)
    {
        var _allText = new HtmlWeb().Load(uri).DocumentNode;

        var _t = _allText.SelectNodes("//text()");

        var _x = string.Empty;

        foreach (var t in _t)
        {
            _x += t.InnerText;
        }

        return new AnylyzedTextStruct
        {
            Count = _t.Where(x => x.InnerText.ToLower().Contains(text.ToLower())).Count(),
            Text = text,
            Rarity = (int)Math.Round((double)(100 * _t.Where(x => x.InnerText.ToLower().Contains(text.ToLower())).Count()) / _t.Count()),
        };
    }

    private static WebsiteTextStruct GetData(HtmlDocument doc)
    {
        var _wts = default(WebsiteTextStruct);
        _wts.Paragraphs = doc.DocumentNode.Descendants("p").Count();

        var _nodes = new List<HtmlNode>();
        _nodes.AddRange(doc.DocumentNode.SelectNodes("//text()"));

        foreach (var item in _nodes)
        {
            _wts.WordsAmount += item.InnerText.Split(' ').Length;
        }

        return _wts;
    }
}

public struct WebsiteTextStruct
{
    public int WordsAmount { get; set; }

    public int Paragraphs { get; set; }
}

public struct AnylyzedTextStruct
{
    public string Text { get; set; }

    public int Count { get; set; }

    public int Rarity { get; set; }
}