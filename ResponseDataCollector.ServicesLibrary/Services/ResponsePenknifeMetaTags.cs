using HtmlAgilityPack;
using System.Text.Json.Nodes;

namespace ResponseDataCollector.ServicesLibrary.Services;

public static class ResponsePenknifeMetaTags
{
    public static JsonArray GetMetaTags(Uri uri)
    {
        return GetTags(new HtmlWeb().Load(uri));
    }

    public static JsonArray GetMetaTags(string content)
    {
        HtmlDocument _doc = new();
        _doc.LoadHtml(content);

        return GetTags(_doc);
    }

    private static JsonArray GetTags(HtmlDocument document, bool getRawData = false)
    {
        var _result = new JsonArray();

        if (getRawData)
        {
            foreach (var item in document.DocumentNode.SelectNodes("//meta"))
            {
                _result.Add(item.OuterHtml);
            }
        }
        else
        {
            var _dic = new Dictionary<string, string>();

            foreach (var item in document.DocumentNode.Descendants("meta"))
            {


                if (!string.IsNullOrEmpty(item.GetAttributeValue("name", "")) && !_dic.Any(x => x.Key == item.GetAttributeValue("name", "")))
                {
                    if (item.GetAttributeValue("name", "") == "charset")
                    {
                        _dic.Add("charset", item.GetAttributeValue("charset", "-"));
                    }
                    else
                    {
                        _dic.Add(item.GetAttributeValue("name", ""), item.GetAttributeValue("content", "-"));
                    }
                }
            }

            _result.Add(_dic);
        }

        return _result;
    }
}
