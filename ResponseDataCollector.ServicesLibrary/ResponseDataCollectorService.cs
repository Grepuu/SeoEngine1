using ResponseDataCollector.ServicesLibrary.Models;
using ResponseDataCollector.ServicesLibrary.Services;
using System.Net;
using System.Net.Sockets;

namespace ResponseDataCollector.ServicesLibrary;

public class ResponseDataCollectorService
{
    public ResponseDataModel Response { get; } = new ResponseDataModel();

    public ResponseDataCollectorService(Uri uri)
    {
        ResponseData(uri).GetAwaiter().GetResult();
    }

    public ResponseDataCollectorService(IPAddress iPAddress)
    {
        ResponseData(iPAddress).GetAwaiter().GetResult();
    }

    public ResponseDataCollectorService(string email)
    {
        ResponseData(email).GetAwaiter().GetResult();
    }

    private async Task ResponseData(Uri uri)
    {
        if (uri != null)
        {
            try
            {
                Response.Url = uri;
                Response.IPAddress = ResponseAddressIPService.GetIPAddress(uri).ToString();
                Response.ResponseTime = await ResponseTimeService.GetResponseTime(uri);
                Response.StatusCode = ResponseStatusCodeService.GetResponseStatus(uri).Result.Status;
                Response.DNSZone = ResponseDNSZone.GetDNSZone(uri);
                Response.Content = await GetContent(uri);
            }
            catch (UriFormatException e)
            {
                //throw new Exception(e.Message);
                Console.WriteLine(e.Message);
            }
            catch (SocketException e)
            {
                //throw new Exception(e.Message);
                Console.WriteLine(e.Message);
            }
        }
    }

    private async Task ResponseData(IPAddress address)
    {
        if (address != null)
        {
            try
            {
                var _uri = new Uri($"http://{Dns.GetHostEntry(address).HostName}");

                Response.Url = _uri;
                Response.IPAddress = address.ToString();
                Response.ResponseTime = await ResponseTimeService.GetResponseTime(_uri);
                Response.StatusCode = ResponseStatusCodeService.GetResponseStatus(_uri).Result.Status;
                Response.DNSZone = ResponseDNSZone.GetDNSZone(_uri);
                Response.Content = await GetContent(_uri);
            }
            catch (SocketException e)
            {
                //throw new Exception(e.Message);
                Console.WriteLine(e.Message);
            }
        }
    }

    private async Task ResponseData(string email)
    {
        if (email != null)
        {
            if (email.Contains('@'))
            {
                try
                {
                    var _uri = new Uri($"http://{email[(email.LastIndexOf('@') + 1)..]}");
                    Response.Url = _uri;
                    Response.IPAddress = ResponseAddressIPService.GetIPAddress(_uri).ToString();
                    Response.ResponseTime = await ResponseTimeService.GetResponseTime(_uri);
                    Response.StatusCode = ResponseStatusCodeService.GetResponseStatus(_uri).Result.Status;
                    Response.DNSZone = ResponseDNSZone.GetDNSZone(_uri);
                    Response.Content = await GetContent(_uri);
                }
                catch (UriFormatException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (SocketException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }

    private static async Task<ResponseContentModel> GetContent(Uri uri)
    {
        var _content = await new HttpClient().GetAsync(uri).Result.Content.ReadAsStringAsync();

        var _result = new ResponseContentModel
        {
            Content = _content,
            Footers = ResponsePenknifeMainTags.GetTagsAmountByName(_content, "footer"),
            Sections = ResponsePenknifeMainTags.GetTagsAmountByName(_content, "section"),
            Headers = ResponsePenknifeMainTags.GetTagsAmountByName(_content, "header"),
            Titles = ResponsePenknifeHeaderTags.GetHeadersAmount(_content),
            CSSAmount = ResponsePenknifeLinkTags.GetTagsAmount(_content),
            ScriptsAmount = ResponsePenknifeScriptTags.GetTagsAmount(_content),
            Paragraphs = ResponsePenknifeWebsiteText.GetWebsiteText(_content).Paragraphs,
            Words = ResponsePenknifeWebsiteText.GetWebsiteText(_content).WordsAmount,
            Images = ResponsePenknifeImgTags.GetTagsAmount(_content),
            MetaTags = ResponsePenknifeMetaTags.GetMetaTags(_content)
        };

        return _result;
    }
}
