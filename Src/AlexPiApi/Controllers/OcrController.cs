using ComputerVision.OCR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Buffers.Text;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace AlexPi.WebApi.AnalytReport.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OcrController : ControllerBase
  {
    readonly ILogger<OcrController> _logger;
    readonly IConfiguration _configuration;

    public OcrController(ILogger<OcrController> logger, IConfiguration configuration) => (_logger, _configuration) = (logger, configuration);

    // GET: api/Ocr/5
    [HttpGet("{imgurl}", Name = "Get")]
    public async Task<string> Get(/*[FromQuery(Name = "imgurl")]*/string imgurl)
    {
      var url = Encoding.UTF8.GetString(Convert.FromBase64String(imgurl)); // slash issue fixed by double encoding
      Debug.WriteLine($"\n\n    Escaped URL:\n{imgurl}\n  UnEscaped URL:\n{url}");

      if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
      {
        return $"\nInvalid remote image url:\n{url}\n";
      }

      var rv = await new OCRSample(_configuration["CognSvcRsc0-Key-001"]).OCRFromUrlAsync(url);
      _logger.LogInformation(rv);
      Debug.WriteLine(rv);
      return rv;
    }

    // GET: api/Ocr                 [HttpGet] public async Task<string> GetAll() => await OCRSample.OCRFromUrlAsync("https://github.com/Azure-Samples/cognitive-services-sample-data-files/raw/master/ComputerVision/Images/printed_text.jpg");
    // POST: api/Ocr                [HttpPost] public void Post([FromBody] string value) { }
    // PUT: api/Ocr/5               [HttpPut("{imgurl}")] public void Put(string imgurl, [FromBody] string value) { }
    // DELETE: api/ApiWithActions/5 [HttpDelete("{imgurl}")] public void Delete(string imgurl) { }
  }
}
/*  
Escaped URL:
https:%2F%2Fgithub.com%2FAzure-Samples%2Fcognitive-services-sample-data-files%2Fraw%2Fmaster%2FComputerVision%2FImages%2Fprinted_text.jpg
UnEscaped URL:
https://github.com/Azure-Samples/cognitive-services-sample-data-files/raw/master/ComputerVision/Images/printed_text.jpg

Response:
{
  "language": "en",
  "textAngle": -0.22689280275926216,
  "orientation": "Up",
  "regions": [
    {
      "boundingBox": "45,94,1143,243",
      "lines": [
        {
          "boundingBox": "79,94,1109,81",
          "words": [
            {
              "boundingBox": "79,104,318,71",
              "text": "Nutrition"
            },
            {
              "boundingBox": "423,95,208,63",
              "text": "Facts"
            },
            {
              "boundingBox": "675,101,198,41",
              "text": "Amount"
            },
            {
              "boundingBox": "887,99,88,38",
              "text": "Per"
            },
            {
              "boundingBox": "990,94,198,48",
              "text": "Serving"
            }
          ]
        },
        {
          "boundingBox": "220,171,334,51",
          "words": [
            {
              "boundingBox": "220,190,91,32",
              "text": "see:"
            },
            {
              "boundingBox": "365,178,71,38",
              "text": "bar"
            },
            {
              "boundingBox": "447,171,107,51",
              "text": "(40g)"
            }
          ]
        },
        {
          "boundingBox": "654,205,321,50",
          "words": [
            {
              "boundingBox": "654,208,122,41",
              "text": "Total"
            },
            {
              "boundingBox": "791,207,82,40",
              "text": "Fat"
            },
            {
              "boundingBox": "892,205,83,50",
              "text": "13g"
            }
          ]
        },
        {
          "boundingBox": "45,230,473,56",
          "words": [
            {
              "boundingBox": "45,246,149,40",
              "text": "Servng"
            },
            {
              "boundingBox": "206,240,70,37",
              "text": "Per"
            },
            {
              "boundingBox": "286,234,191,45",
              "text": "Package:"
            },
            {
              "boundingBox": "492,230,26,38",
              "text": "4"
            }
          ]
        },
        {
          "boundingBox": "682,287,433,50",
          "words": [
            {
              "boundingBox": "682,287,222,43",
              "text": "Saturated"
            },
            {
              "boundingBox": "978,288,16,40",
              "text": "t"
            },
            {
              "boundingBox": "1014,287,70,41",
              "text": "1.5"
            },
            {
              "boundingBox": "1088,296,27,41",
              "text": "g"
            }
          ]
        }
      ]
    },
    {
      "boundingBox": "11,347,474,312",
      "lines": [
        {
          "boundingBox": "11,347,474,50",
          "words": [
            {
              "boundingBox": "11,354,183,41",
              "text": "Amount"
            },
            {
              "boundingBox": "204,352,84,39",
              "text": "Per"
            },
            {
              "boundingBox": "299,347,186,50",
              "text": "Serving"
            }
          ]
        },
        {
          "boundingBox": "19,436,256,43",
          "words": [
            {
              "boundingBox": "19,437,159,42",
              "text": "alories"
            },
            {
              "boundingBox": "198,436,77,41",
              "text": "190"
            }
          ]
        },
        {
          "boundingBox": "32,517,397,43",
          "words": [
            {
              "boundingBox": "32,518,101,42",
              "text": "ories"
            },
            {
              "boundingBox": "146,517,99,42",
              "text": "from"
            },
            {
              "boundingBox": "259,517,72,42",
              "text": "Fat"
            },
            {
              "boundingBox": "348,517,81,43",
              "text": "110"
            }
          ]
        },
        {
          "boundingBox": "72,618,394,41",
          "words": [
            {
              "boundingBox": "72,619,14,32",
              "text": "t"
            },
            {
              "boundingBox": "92,618,82,41",
              "text": "Daily"
            },
            {
              "boundingBox": "184,619,103,34",
              "text": "Values"
            },
            {
              "boundingBox": "297,627,54,27",
              "text": "are"
            },
            {
              "boundingBox": "362,621,104,34",
              "text": "based"
            }
          ]
        }
      ]
    },
    {
      "boundingBox": "598,362,437,208",
      "lines": [
        {
          "boundingBox": "673,362,289,53",
          "words": [
            {
              "boundingBox": "673,363,126,42",
              "text": "Trans"
            },
            {
              "boundingBox": "815,362,73,42",
              "text": "Fat"
            },
            {
              "boundingBox": "904,362,58,53",
              "text": "Og"
            }
          ]
        },
        {
          "boundingBox": "613,438,422,60",
          "words": [
            {
              "boundingBox": "613,438,297,44",
              "text": "Cholesterol"
            },
            {
              "boundingBox": "927,440,108,58",
              "text": "Omg"
            }
          ]
        },
        {
          "boundingBox": "598,517,358,53",
          "words": [
            {
              "boundingBox": "598,517,198,46",
              "text": "Sodium"
            },
            {
              "boundingBox": "815,520,141,50",
              "text": "20mq"
            }
          ]
        }
      ]
    }
  ]
}

{
  "language": "en",
  "textAngle": -0.22689280275926216,
  "orientation": "Up",
  "regions": [
    {
      "boundingBox": "45,94,1143,243",
      "lines": [
        {
          "boundingBox": "79,94,1109,81",
          "words": [
            {
              "boundingBox": "79,104,318,71",
              "text": "Nutrition"
            },
            {
              "boundingBox": "423,95,208,63",
              "text": "Facts"
            },
            {
              "boundingBox": "675,101,198,41",
              "text": "Amount"
            },
            {
              "boundingBox": "887,99,88,38",
              "text": "Per"
            },
            {
              "boundingBox": "990,94,198,48",
              "text": "Serving"
            }
          ]
        },
        {
          "boundingBox": "220,171,334,51",
          "words": [
            {
              "boundingBox": "220,190,91,32",
              "text": "see:"
            },
            {
              "boundingBox": "365,178,71,38",
              "text": "bar"
            },
            {
              "boundingBox": "447,171,107,51",
              "text": "(40g)"
            }
          ]
        },
        {
          "boundingBox": "654,205,321,50",
          "words": [
            {
              "boundingBox": "654,208,122,41",
              "text": "Total"
            },
            {
              "boundingBox": "791,207,82,40",
              "text": "Fat"
            },
            {
              "boundingBox": "892,205,83,50",
              "text": "13g"
            }
          ]
        },
        {
          "boundingBox": "45,230,473,56",
          "words": [
            {
              "boundingBox": "45,246,149,40",
              "text": "Servng"
            },
            {
              "boundingBox": "206,240,70,37",
              "text": "Per"
            },
            {
              "boundingBox": "286,234,191,45",
              "text": "Package:"
            },
            {
              "boundingBox": "492,230,26,38",
              "text": "4"
            }
          ]
        },
        {
          "boundingBox": "682,287,433,50",
          "words": [
            {
              "boundingBox": "682,287,222,43",
              "text": "Saturated"
            },
            {
              "boundingBox": "978,288,16,40",
              "text": "t"
            },
            {
              "boundingBox": "1014,287,70,41",
              "text": "1.5"
            },
            {
              "boundingBox": "1088,296,27,41",
              "text": "g"
            }
          ]
        }
      ]
    },
    {
      "boundingBox": "11,347,474,312",
      "lines": [
        {
          "boundingBox": "11,347,474,50",
          "words": [
            {
              "boundingBox": "11,354,183,41",
              "text": "Amount"
            },
            {
              "boundingBox": "204,352,84,39",
              "text": "Per"
            },
            {
              "boundingBox": "299,347,186,50",
              "text": "Serving"
            }
          ]
        },
        {
          "boundingBox": "19,436,256,43",
          "words": [
            {
              "boundingBox": "19,437,159,42",
              "text": "alories"
            },
            {
              "boundingBox": "198,436,77,41",
              "text": "190"
            }
          ]
        },
        {
          "boundingBox": "32,517,397,43",
          "words": [
            {
              "boundingBox": "32,518,101,42",
              "text": "ories"
            },
            {
              "boundingBox": "146,517,99,42",
              "text": "from"
            },
            {
              "boundingBox": "259,517,72,42",
              "text": "Fat"
            },
            {
              "boundingBox": "348,517,81,43",
              "text": "110"
            }
          ]
        },
        {
          "boundingBox": "72,618,394,41",
          "words": [
            {
              "boundingBox": "72,619,14,32",
              "text": "t"
            },
            {
              "boundingBox": "92,618,82,41",
              "text": "Daily"
            },
            {
              "boundingBox": "184,619,103,34",
              "text": "Values"
            },
            {
              "boundingBox": "297,627,54,27",
              "text": "are"
            },
            {
              "boundingBox": "362,621,104,34",
              "text": "based"
            }
          ]
        }
      ]
    },
    {
      "boundingBox": "598,362,437,208",
      "lines": [
        {
          "boundingBox": "673,362,289,53",
          "words": [
            {
              "boundingBox": "673,363,126,42",
              "text": "Trans"
            },
            {
              "boundingBox": "815,362,73,42",
              "text": "Fat"
            },
            {
              "boundingBox": "904,362,58,53",
              "text": "Og"
            }
          ]
        },
        {
          "boundingBox": "613,438,422,60",
          "words": [
            {
              "boundingBox": "613,438,297,44",
              "text": "Cholesterol"
            },
            {
              "boundingBox": "927,440,108,58",
              "text": "Omg"
            }
          ]
        },
        {
          "boundingBox": "598,517,358,53",
          "words": [
            {
              "boundingBox": "598,517,198,46",
              "text": "Sodium"
            },
            {
              "boundingBox": "815,520,141,50",
              "text": "20mq"
            }
          ]
        }
      ]
    }
  ]
}
*/
