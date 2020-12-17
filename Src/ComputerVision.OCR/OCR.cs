using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ComputerVision.OCR
{
  public class OCRSample
  {
    readonly string _subsnKey, _endpoint;

    public OCRSample(string keys)
    {
      _subsnKey = keys.Split(' ')[0];
      _endpoint = keys.Split(' ')[2];
    }

    public async Task<string> OCRFromUrlAsync(string remoteImageUrl)
    {
      if (!Uri.IsWellFormedUriString(remoteImageUrl, UriKind.Absolute))
      {
        return $"\nInvalid remote image url:\n{remoteImageUrl} \n";
      }

      try
      {

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subsnKey);
        //Assemble the URI and content header for the REST API request
        var requestParameters = "language=unk&detectOrientation=true";
        var uri = $"{_endpoint}/vision/v2.0/ocr?{requestParameters}";
        var requestBody = " {\"url\":\"" + remoteImageUrl + "\"}";
        var content = new StringContent(requestBody);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        // Post the request and display the result
        var response = await client.PostAsync(uri, content);
        var contentString = await response.Content.ReadAsStringAsync();

        Debug.WriteLine($"\nResponse:\n{JToken.Parse(contentString)}\n");
        return JToken.Parse(contentString).ToString();
      }
      catch (Exception e) { return $"Error in OCRFromUrlAsync(): { e.Message}"; }
    }
    public async Task<string> OCRFromFileAsync(string imageFilePath)
    {
      if (!File.Exists(imageFilePath))
      {
        return $"\nInvalid file path: {imageFilePath}";
      }
      try
      {
        var client = new HttpClient();

        // Request headers.
        client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _subsnKey);

        // Request parameters. 
        // The language parameter doesn't specify a language, so the method detects it automatically.
        // The detectOrientation parameter is set to true, so the method detects and and corrects text orientation before detecting text.
        var requestParameters = "language=unk&detectOrientation=true";

        //Assemble the URI and content header for the REST API request
        var uri = $"{_endpoint}/vision/v2.0/ocr?{requestParameters}";
        // Read the contents of the specified local image into a byte array.
        var byteData = GetImageAsByteArray(imageFilePath);

        // Add the byte array as an octet stream to the request body.
        using (var content = new ByteArrayContent(byteData))
        {
          // This particular example uses the "application/octet-stream" content type.
          // The other content types you can use are "application/json" (used in OCRFromUrlAsync) and "multipart/form-data".
          content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

          // Asynchronously call the REST API method.
          var response = await client.PostAsync(uri, content);

          // Asynchronously get the JSON response.
          var contentString = await response.Content.ReadAsStringAsync();

          Debug.WriteLine($"\nResponse:\n{JToken.Parse(contentString)}\n");
          return JToken.Parse(contentString).ToString();
        }
      }
      catch (Exception e) { return $"Error in OCRFromUrlAsync(): { e.Message}"; }
    }

    static byte[] GetImageAsByteArray(string imageFilePath)
    {
      // Open a read-only file stream for the specified file.
      using (var fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
      {
        // Read the file's contents into a byte array.
        var binaryReader = new BinaryReader(fileStream);
        return binaryReader.ReadBytes((int)fileStream.Length);
      }
    }
  }

  class Program_Demo
  {
    static void Main___demo()
    {
      // See this repo's readme.md for info on how to get these images. Alternatively, you can just set the path to any appropriate image on your machine.
      const string
        imgFile = @"Images\printed_text.jpg",
        imgURL = "https://github.com/Azure-Samples/cognitive-services-sample-data-files/raw/master/ComputerVision/Images/printed_text.jpg";

      new OCRSample("").OCRFromUrlAsync(imgURL).Wait(5000);
      new OCRSample("").OCRFromFileAsync(imgFile).Wait(5000);
    }
  }
}
