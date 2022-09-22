using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseDataCollector.ServicesLibrary.Services;

public class ResponsePenknifeImgAltParam
{
    public static ImgStruct CheckImgAltCorrection(Uri uri)
    {
        var _nodes = new HtmlWeb().Load(uri).DocumentNode.Descendants("img");
        var _e = new ImgStruct();

        var _t = new List<string>();

        foreach (var item in _nodes)
        {
            if(string.IsNullOrEmpty(item.GetAttributeValue("Alt", "")))
            {
                _e.EmptyImgesAlt++;
                _t.Add(item.OuterHtml);
            }
        }

        _e.ErrorLines = _t;

        return _e;
    }

    public static ImgStruct CheckImgAltCorrection(string content)
    {
        HtmlDocument _doc = new HtmlDocument();
        _doc.LoadHtml(content);

        var _nodes = _doc.DocumentNode.Descendants("img");
        var _e = new ImgStruct();

        foreach (var item in _nodes)
        {
            if (string.IsNullOrEmpty(item.GetAttributeValue("Alt", "")))
            {
                _e.EmptyImgesAlt++;
                _e.ErrorLines.Add(item.OuterHtml);
            }
        }

        return _e;
    }

    public struct ImgStruct
    {
        public int EmptyImgesAlt { get; set; }

        public List<string> ErrorLines { get; set; }
    }
}
