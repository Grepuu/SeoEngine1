using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ResponseDataCollector.ServicesLibrary.Services;

public class ResponsePenknifeImages
{
    public static ICollection<ImageTagsStruct> GetImages(Uri uri)
    {
        var _nodes = new HtmlWeb().Load(uri).DocumentNode.Descendants("img");

        var _images = new List<ImageTagsStruct>();

        foreach (var item in _nodes)
        {
            _images.Add(new ImageTagsStruct
            {
                Alt = item.GetAttributeValue("Alt","null"),
                Src = item.GetAttributeValue("Src","null"),
                Type = item.GetAttributeValue("Type","null")
            });
        }

        return _images;
    }

    public static ICollection<ImageTagsStruct> GetImages(string content)
    {
        HtmlDocument _doc = new HtmlDocument();
        _doc.LoadHtml(content);

        var _nodes = _doc.DocumentNode.Descendants("img");

        var _images = new List<ImageTagsStruct>();

        foreach (var item in _nodes)
        {
            _images.Add(new ImageTagsStruct
            {
                Alt = item.GetAttributeValue("Alt", "null"),
                Src = item.GetAttributeValue("Src", "null"),
                Type = item.GetAttributeValue("Type", "null")
            });
        }

        return _images;
    }
}

public struct ImageTagsStruct
{
    public string Type { get; set; }
    public string Src { get; set; }
    public string Alt { get; set; }
}