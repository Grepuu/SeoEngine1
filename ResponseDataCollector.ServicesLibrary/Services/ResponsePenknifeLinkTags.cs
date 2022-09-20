using HtmlAgilityPack;

namespace ResponseDataCollector.ServicesLibrary.Services;

public class ResponsePenknifeLinkTags
{
    public static int GetTagsAmount(Uri uri)
    {
        return new HtmlWeb().Load(uri).DocumentNode.Descendants("link").Count();
    }

    public static int GetTagsAmount(string content)
    {
        HtmlDocument _doc = new HtmlDocument();
        _doc.LoadHtml(content);

        return _doc.DocumentNode.Descendants("link").Count();
    }
}
