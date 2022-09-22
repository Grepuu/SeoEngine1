using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ResponseDataCollector.ServicesLibrary.Services;

public class ResponseRobotsTxt
{
    public static async Task<string?> GetRobotsFile(Uri uri)
    {
        HttpClient _httpClient = new HttpClient();
        var _response = await _httpClient.GetAsync($"{uri}/robots.txt");

        if(_response.StatusCode == HttpStatusCode.OK)
        {
            return await _response.Content.ReadAsStringAsync();
        }
        return String.Empty;
    }
}
