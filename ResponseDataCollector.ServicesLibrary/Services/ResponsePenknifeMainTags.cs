using HtmlAgilityPack;

namespace ResponseDataCollector.ServicesLibrary.Services;

public class ResponsePenknifeMainTags
{
    public static int GetTagsAmountByName(Uri uri, string name)
    {
        return new HtmlWeb().Load(uri).DocumentNode.Descendants($"{name}").Count();
    }

    public static int GetTagsAmountByName(string content, string name)
    {
        HtmlDocument _doc = new HtmlDocument();
        _doc.LoadHtml(content);

        return _doc.DocumentNode.Descendants($"{name}").Count();
    }
}
