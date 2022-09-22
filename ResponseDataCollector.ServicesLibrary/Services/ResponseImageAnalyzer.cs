using HtmlAgilityPack;

namespace ResponseDataCollector.ServicesLibrary.Services;

public class ResponseImageAnalyzer
{
    /// <summary>
    /// Analyze image from Uri.
    /// </summary>
    /// <param name="uri">Uri to get html content.</param>
    /// <returns>Collection of data from analyzed images.</returns>
    public static ICollection<ImageStruct> AnalyzeImage(Uri uri)
    {
        var _nodes = new HtmlWeb().Load(uri).DocumentNode.Descendants("img");

        var _images = new List<ImageStruct>();

        foreach (var item in _nodes)
        {
            _images.Add(new ImageStruct
            {
                Src = item.GetAttributeValue("src", string.Empty),
                Extension = Path.GetExtension(item.GetAttributeValue("src", string.Empty)),
                Height = item.GetAttributeValue<int>("Height", 0),
                Width = item.GetAttributeValue<int>("Width", 0),
            });
        }

        return _images;
    }

    /// <summary>
    /// Analyze image from Uri.
    /// </summary>
    /// <param name="content">html string / file content.</param>
    /// <returns>Collection of data from analyzed images.</returns>
    public static string AnalyzeImage(string content)
    {
        HtmlDocument _doc = new HtmlDocument();
        _doc.LoadHtml(content);

        var _nodes = _doc.DocumentNode.Descendants("img");

        var _images = new List<ImageStruct>();

        foreach (var item in _nodes)
        {
            _images.Add(new ImageStruct
            {
                Src = item.GetAttributeValue("src", string.Empty),
                Extension = Path.GetExtension(item.GetAttributeValue("src", string.Empty)),
                Height = item.GetAttributeValue<int>("Height", 0),
                Width = item.GetAttributeValue<int>("Width", 0),
            });
        }

        return string.Empty;
    }
}

public struct ImageStruct
{
    public string Src { get; set; }

    public string Extension { get; set; }

    public int Height { get; set; }

    public int Width { get; set; }
}
