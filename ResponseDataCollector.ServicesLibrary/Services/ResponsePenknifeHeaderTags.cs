using HtmlAgilityPack;

namespace ResponseDataCollector.ServicesLibrary.Services;

public static class ResponsePenknifeHeaderTags
{
    public static int[] GetHeadersAmount(Uri uri)
    {
        HtmlWeb _hw = new HtmlWeb();
        HtmlDocument _doc = _hw.Load(uri);

        int[] _arr = {
            _doc.DocumentNode.Descendants($"h1").Count(),
            _doc.DocumentNode.Descendants($"h2").Count(),
            _doc.DocumentNode.Descendants($"h3").Count(),
            _doc.DocumentNode.Descendants($"h4").Count(),
            _doc.DocumentNode.Descendants($"h5").Count()
        };

        return _arr;
    }

    public static int[] GetHeadersAmount(string content)
    {
        HtmlDocument _doc = new HtmlDocument();
        _doc.LoadHtml(content);

        int[] _arr = {
            _doc.DocumentNode.Descendants($"h1").Count(),
            _doc.DocumentNode.Descendants($"h2").Count(),
            _doc.DocumentNode.Descendants($"h3").Count(),
            _doc.DocumentNode.Descendants($"h4").Count(),
            _doc.DocumentNode.Descendants($"h5").Count()
        };

        return _arr;
    }
}
