using Microsoft.AspNetCore.Mvc;
using ResponseDataCollector.ServicesLibrary;
using ResponseDataCollector.ServicesLibrary.Services;

namespace ResponseDataCollector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentCollectorController : ControllerBase
    {
        private readonly ILogger<ContentCollectorController> _logger;

        public ContentCollectorController(ILogger<ContentCollectorController> logger) => _logger = logger;


        [HttpGet("GetContent")]
        public IActionResult GetContent(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                var _response = new ResponseDataCollectorService(uri);
                
                return Ok(_response.Response.Content.Content);
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetStyles")]
        public IActionResult GetStyles(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                return Ok(ResponsePenknifeLinkTags.GetTagsAmount(uri));
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetScripts")]
        public IActionResult GetScripts(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                return Ok(ResponsePenknifeScriptTags.GetTagsAmount(uri));
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetImages")]
        public IActionResult GetImages(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                return Ok(ResponsePenknifeImgTags.GetTagsAmount(uri));
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetWords")]
        public IActionResult GetWords(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                return Ok(ResponsePenknifeWebsiteText.GetWebsiteText(uri).WordsAmount);
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetAnalyzedWord")]
        public IActionResult GetWords(string url, string text)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                return Ok(ResponsePenknifeWebsiteText.CheckText(uri,text));
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetParagraphs")]
        public IActionResult GetParagraphs(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                return Ok(ResponsePenknifeWebsiteText.GetWebsiteText(uri).Paragraphs);
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetHeaders")]
        public IActionResult GetHeaders(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                return Ok(ResponsePenknifeMainTags.GetTagsAmountByName(uri,"header"));
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetSections")]
        public IActionResult GetSections(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                return Ok(ResponsePenknifeMainTags.GetTagsAmountByName(uri, "section"));
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetFooters")]
        public IActionResult GetFooters(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                return Ok(ResponsePenknifeMainTags.GetTagsAmountByName(uri, "footer"));
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetTitles")]
        public IActionResult GetTitles(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                return Ok(ResponsePenknifeHeaderTags.GetHeadersAmount(uri));
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetCertificate")]
        public IActionResult GetCert(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                return Ok(ResponseCertificate.GetCertificate(uri));
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetImagesContent")]
        public IActionResult GetImagesContent(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                return Ok(ResponsePenknifeImages.GetImages(uri));
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetImagesErrors")]
        public IActionResult GetImagesErrors(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                return Ok(ResponsePenknifeImgAltParam.CheckImgAltCorrection(uri));
            }
            return BadRequest("url is not valid!");
        }

        [HttpGet("GetImagesAnalyze")]
        public IActionResult GetImagesAnalyze(string url)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out Uri? uri))
            {
                return Ok(ResponseImageAnalyzer.AnalyzeImage(uri));
            }
            return BadRequest("url is not valid!");
        }
    }
}