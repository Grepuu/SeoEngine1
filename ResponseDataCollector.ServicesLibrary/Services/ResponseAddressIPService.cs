using System.Net;

namespace ResponseDataCollector.ServicesLibrary.Services;

public static class ResponseAddressIPService
{
    public static IPAddress GetIPAddress(Uri uri)
    {
        return Dns.GetHostAddresses(uri.Host).FirstOrDefault();
    }
}
