using System.Diagnostics;

namespace ResponseDataCollector.ServicesLibrary.Services;

public class ResponseTimeService
{
    public static async Task<long> GetResponseTime(Uri uri)
    {
        Stopwatch _sw = Stopwatch.StartNew();

        HttpClient _httpClient = new HttpClient();
        await _httpClient.GetAsync(uri);

        _sw.Stop();
        return _sw.ElapsedMilliseconds;
    }
}
