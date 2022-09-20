using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ResponseDataCollector.ServicesLibrary.Models;

public class ResponseDataModel
{
    public Uri Url { get; set; }

    public string IPAddress { get; set; }

    public long ResponseTime { get; set; }

    public HttpStatusCode StatusCode { get; set; }

    public string DNSZone { get; set; }

    public ResponseContentModel Content { get; set; }
}
