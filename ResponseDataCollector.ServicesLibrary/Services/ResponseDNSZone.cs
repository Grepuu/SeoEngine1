using System.Net;

namespace ResponseDataCollector.ServicesLibrary.Services;

public static class ResponseDNSZone
{
    public static string GetDNSZone(Uri uri)
    {
        return Dns.GetHostEntry(uri.Host).HostName;
    }
}
