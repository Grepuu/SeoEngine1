using Microsoft.AspNetCore.Mvc;
using ResponseDataCollector.ServicesLibrary;
using ResponseDataCollector.ServicesLibrary.Services;
using System.Net;

namespace ResponseDataCollector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataCollectorController : ControllerBase
    {
        private ILogger<DataCollectorController> _logger;

        public DataCollectorController(ILogger<DataCollectorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("CollectDataByUrl")]
        public IActionResult CollectDataByUrl(string url)
        {
            if(Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                var _response = new ResponseDataCollectorService(uri);
                return Ok(_response.Response);
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("CollectDataByIpAddress")]
        public IActionResult CollectDataByIPAddress(string ipAddress)
        {
            if(IPAddress.TryParse(ipAddress, out IPAddress? address))
            {
                var _response = new ResponseDataCollectorService(address);
                return Ok(_response.Response);
            }
            return BadRequest("IP Address is not valid!");
        }

        [HttpGet("CollectDataByIpEmail")]
        public IActionResult CollectDataByEmail(string email)
        {
            if (email.Contains('@') && Uri.TryCreate($"http://{email[(email.LastIndexOf('@') + 1)..]}", UriKind.Absolute, out Uri? uri))
            {
                var _response = new ResponseDataCollectorService(email);
                return Ok(_response.Response);
            }            
            return BadRequest("E-mail is not valid!");
        }

        [HttpGet("GetIpAddress")]
        public IActionResult GetIpAddress (string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                return Ok(ResponseAddressIPService.GetIPAddress(uri).ToString());
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetDNSZone")]
        public IActionResult GetDNSZone(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                return Ok(ResponseDNSZone.GetDNSZone(uri));
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetStatusCode")]
        public IActionResult GetStatusCode(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                var _status = ResponseStatusCodeService.GetResponseStatus(uri).Result;
                return Ok($"{_status.StatusCode} ({_status.Status})");
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetRobots")]
        public IActionResult GetRobotsFile(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                var _result = ResponseRobotsTxt.GetRobotsFile(uri).Result;
                if(!string.IsNullOrEmpty(_result))
                {
                    return Ok(_result);
                }
                return NotFound();
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetSiteMapContent")]
        public IActionResult GetSiteMapContent(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                var _result = ResponseSiteMap.GetSiteMapContent(uri).Result;
                if (_result.Any())
                {
                    return Ok(_result);
                }
                return NotFound();
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetSiteMap")]
        public IActionResult GetSiteMap(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                var _result = ResponseSiteMap.GetSiteMap(uri).Result;
                if (_result.Any())
                {
                    return Ok(_result);
                }
                return NotFound();
            }
            return BadRequest("url is not valid!");
        }
    }
}
