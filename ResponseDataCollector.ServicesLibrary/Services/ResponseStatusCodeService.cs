using System.Net;

namespace ResponseDataCollector.ServicesLibrary.Services;

public static class ResponseStatusCodeService
{
    public static async Task<ResponseStatus> GetResponseStatus(Uri url)
    {
        HttpClient _httpClient = new HttpClient();

        var _response = await _httpClient.GetAsync(url);

        return new ResponseStatus
        {
            Status = _response.StatusCode,
            StatusCode = (int)_response.StatusCode
        };
    }

    public struct ResponseStatus
    {
        public HttpStatusCode Status;

        public int StatusCode;
    }
}
