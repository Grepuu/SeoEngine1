using HtmlAgilityPack;
using System.Xml.Linq;

namespace ResponseDataCollector.ServicesLibrary.Services;

public class ResponseSiteMap
{
    public static async Task<ICollection<SiteMapStruct>> GetSiteMapContent(Uri uri)
    {
        var _responseRtxt = await ResponseRobotsTxt.GetRobotsFile(uri);

        string[] _splittedResponse = _responseRtxt.Split(' ');

        var _nodes = new List<SiteMapStruct>();

        for (int i = 0; i < _splittedResponse.Length; i++)
        {
            if (_splittedResponse[i].Contains("Sitemap"))
            {
                var _sitemapUrl = _splittedResponse[i + 1][.._splittedResponse[i + 1].LastIndexOf("\n")];

                var _content = await GetXmlContent(_sitemapUrl);

                foreach (var item in _content.Descendants("loc"))
                {
                    if(item.InnerHtml.Contains(".xml"))
                    {
                        var _t = GetXmlContent(item.InnerHtml).Result.Descendants("loc");

                        var _urls = new List<string>();
                        foreach (var xmlContent in _t)
                        {
                            _urls.Add(xmlContent.InnerHtml);
                        }

                        _nodes.Add(new SiteMapStruct
                        {
                            Parent = item.InnerHtml,
                            Urls = _urls
                        });
                    }
                    else
                    {
                        _nodes.Add(new SiteMapStruct
                        {
                            Parent = "",
                            Urls = new List<string> { item.InnerHtml }
                        });
                    }
                }

                break;
            }
        }
        return _nodes;
    }

    public static async Task<string> GetSiteMap(Uri uri)
    {
        var _responseRtxt = await ResponseRobotsTxt.GetRobotsFile(uri);

        string[] _splittedResponse = _responseRtxt.Split(' ');

        for (int i = 0; i < _splittedResponse.Length; i++)
        {
            if (_splittedResponse[i].Contains("Sitemap"))
            {
                var _sitemapUrl = _splittedResponse[i + 1][.._splittedResponse[i + 1].LastIndexOf("\n")];

                return GetXmlContent(_sitemapUrl).Result.OuterHtml;
            }
        }

        return string.Empty;
    }

    private static async Task<HtmlNode> GetXmlContent(string url)
    {
        HtmlDocument _doc = new();
        _doc.LoadHtml(await new HttpClient().GetAsync(url).Result.Content.ReadAsStringAsync());

        return _doc.DocumentNode;
    }

    public struct SiteMapStruct
    {
        public string Parent { get; set; }

        public List<string> Urls { get; set; }
    }
}
