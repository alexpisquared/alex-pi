using AlexPiApi.Services;
using Db.OneBase.Model;
using static System.Console;

namespace AzureLogParser;

public class LogParser
{
  public static async Task Main2(string constr)
  {
    //var client = new SecretClient(new Uri("https://demopockv.vault.azure.net/"), new DefaultAzureCredential());
    //KeyVaultSecret secret = client.GetSecret("ChtBlobStorageConnectionString");
    //string connectionString = secret.Value;

    var connectionString = constr; // secret.Value !!!!!!!!!!!!!!!!!!!!!!!!!!;
    var poorMansLogger = new PoorMansLogger(connectionString);

    //// Create a file
    //WriteLine("Creating file...");
    //await poorMansLogger.CreateFileAsync(containerName, fileName, content);

    //// Read the file
    //WriteLine("Reading file...");
    //var readContent = await poorMansLogger.ReadFileAsync(containerName, fileName);
    //WriteLine($"Content: {readContent}");

    //// Update the file
    //WriteLine("Updating file...");
    //var newContent = "Hello, Azure!";
    //await poorMansLogger.UpdateFileAsync(containerName, fileName, newContent);

    await poorMansLogger.AppendToFileAsync($"{DateTime.Now:MM-dd HH:mm}  -----  a call from the inspector's start  -----\r\n");

    do
    {
      var log = // _log; //
                      await poorMansLogger.ReadFileAsync();

      ForegroundColor = ConsoleColor.DarkYellow;
      WriteLine($"{log}");
      ForegroundColor = ConsoleColor.Gray;

      var lines = log.Split("\r\n");
      var WebEventLogs = new List<WebEventLog>();
      var WebEventLogLines = lines.ToList().Where(r => r.Contains("WebEventLog { BrowserSignature =")).ToList();
      foreach (var line in WebEventLogLines)
      {
        try
        {
          var WebEventLog = new WebEventLog();
          var parts = line.Split(new string[] { "WebEventLog { BrowserSignature = ", ", Id = " }, StringSplitOptions.RemoveEmptyEntries);
          var whens = line.Split(new string[] { "DoneAt = ", ", WebsiteUser =" }, StringSplitOptions.RemoveEmptyEntries);

          //WebEventLog.EventName = eventName;
          WebEventLog.DoneAt = DateTime.Parse(whens[1]);
          WebEventLog.BrowserSignature = parts[1];

          WebEventLogs.Add(WebEventLog);
        }
        catch (Exception ex)
        {
          ForegroundColor = ConsoleColor.Red;
          WriteLine($"{ex.Message}  {line}");
          ResetColor();
        }
      }

      ForegroundColor = ConsoleColor.DarkGreen;
      //tmi: foreach (var wel in WebEventLogs) WriteLine(wel.ToString2);

      //foreach (var browserSignature in WebEventLogs.Select(r => r.BrowserSignature).Distinct().ToList()) WriteLine(browserSignature);

      ForegroundColor = ConsoleColor.DarkYellow;
      // select unique BrowserSignature With Max Of DoneAt from WebEventLogs
      //WebEventLogs.GroupBy(r => r.BrowserSignature).Select(g => g.OrderByDescending(r => r.DoneAt).First()).ToList().ForEach(r => WriteLine($" {r.DoneAt}  {r.BrowserSignature}"));

      ForegroundColor = ConsoleColor.DarkCyan;
      // select unique BrowserSignature With Max and Min Of DoneAt and the count records per BrowserSignature of from WebEventLogs
      WebEventLogs.GroupBy(r => r.BrowserSignature).Select(g => new { g.Key, Max = g.Max(r => r.DoneAt), Min = g.Min(r => r.DoneAt), Count = g.Count() }).OrderBy(r => r.Max).ToList().ForEach(r => WriteLine($" {r.Min.ToLocalTime()}  {r.Max.ToLocalTime()} {r.Max - r.Min,15}{r.Count,4}  {r.Key}"));

      ForegroundColor = ConsoleColor.Green;  /**/ WriteLine($"Any key to repeat ... Escape to quit");
      ResetColor();

    } while (ReadKey().Key != ConsoleKey.Escape);
  }

  const string _log = @"
10-07 17:36    0  WebEventLogsController.Post(WebEventLog { BrowserSignature = Linux; Android 10; K 117.0.**Safari/537.36**en-CA,zh-CN,zh,uk-UA,uk,en-GB,en-US,en**CPU:8**ANGLE (Qualcomm, Adreno (TM) 630, OpenGL ES 3.2)**Google Inc. (Qualcomm)., Id = 0, WebsiteUserId = 0, EventName = home undefined, DoneAt = 10/7/2023 5:36:43 PM, WebsiteUser =  }) ■▄▀■
10-07 17:40    1  WebEventLogsController.Post(WebEventLog { BrowserSignature = Linux; Android 10; K 117.0.**Safari/537.36**en-CA,zh-CN,zh,uk-UA,uk,en-GB,en-US,en**CPU:8**ANGLE (Qualcomm, Adreno (TM) 630, OpenGL ES 3.2)**Google Inc. (Qualcomm)., Id = 0, WebsiteUserId = 0, EventName = home undefined, DoneAt = 10/7/2023 5:40:47 PM, WebsiteUser =  }) ■▄▀■
10-07 17:43    2  WebEventLogsController.Post(WebEventLog { BrowserSignature = Linux; Android 10; K 117.0.**Safari/537.36**en-CA,zh-CN,zh,uk-UA,uk,en-GB,en-US,en**CPU:8**ANGLE (Qualcomm, Adreno (TM) 630, OpenGL ES 3.2)**Google Inc. (Qualcomm)., Id = 0, WebsiteUserId = 0, EventName = home undefined, DoneAt = 10/7/2023 5:43:47 PM, WebsiteUser =  }) ■▄▀■
10-07 19:36    0  WebEventLogsController.Post(WebEventLog { BrowserSignature = Linux; Android 10; K 117.0.**Safari/537.36**en-CA,zh-CN,zh,uk-UA,uk,en-GB,en-US,en**CPU:8**ANGLE (Qualcomm, Adreno (TM) 630, OpenGL ES 3.2)**Google Inc. (Qualcomm)., Id = 0, WebsiteUserId = 0, EventName = home undefined, DoneAt = 10/7/2023 7:36:41 PM, WebsiteUser =  }) ■▄▀■
10-07 20:05    1  WebEventLogsController.Post(WebEventLog { BrowserSignature = Windows NT 10.0; Win64; x64**117.0.2045.47**en-US,en,uk,ru**CPU:12**ANGLE (Intel, Intel(R) UHD Graphics 630 Direct3D11 vs_5_0 ps_5_0, D3D11)**Google Inc. (Intel)., Id = 0, WebsiteUserId = 0, EventName = ocr-, DoneAt = 10/7/2023 8:05:03 PM, WebsiteUser =  }) ■▄▀■
10-07 20:05    2  WebEventLogsController.Post(WebEventLog { BrowserSignature = Windows NT 10.0; Win64; x64**117.0.2045.47**en-US,en,uk,ru**CPU:12**ANGLE (Intel, Intel(R) UHD Graphics 630 Direct3D11 vs_5_0 ps_5_0, D3D11)**Google Inc. (Intel)., Id = 0, WebsiteUserId = 0, EventName = ocr-, DoneAt = 10/7/2023 8:05:05 PM, WebsiteUser =  }) ■▄▀■
10-07 20:05    3  WebEventLogsController.Post(WebEventLog { BrowserSignature = Windows NT 10.0; Win64; x64**117.0.2045.47**en-US,en,uk,ru**CPU:12**ANGLE (Intel, Intel(R) UHD Graphics 630 Direct3D11 vs_5_0 ps_5_0, D3D11)**Google Inc. (Intel)., Id = 0, WebsiteUserId = 0, EventName = ocr-, DoneAt = 10/7/2023 8:05:17 PM, WebsiteUser =  }) ■▄▀■
10-07 20:05    4  WebEventLogsController.Post(WebEventLog { BrowserSignature = Windows NT 10.0; Win64; x64**117.0.2045.47**en-US,en,uk,ru**CPU:12**ANGLE (Intel, Intel(R) UHD Graphics 630 Direct3D11 vs_5_0 ps_5_0, D3D11)**Google Inc. (Intel)., Id = 0, WebsiteUserId = 0, EventName = dsgn, DoneAt = 10/7/2023 8:05:40 PM, WebsiteUser =  }) ■▄▀■
10-07 22:53    0  WebEventLogsController.Post(WebEventLog { BrowserSignature = Linux; Android 10; K 117.0.**Safari/537.36**en-CA,zh-CN,zh,uk-UA,uk,en-GB,en-US,en**CPU:8**ANGLE (Qualcomm, Adreno (TM) 630, OpenGL ES 3.2)**Google Inc. (Qualcomm)., Id = 0, WebsiteUserId = 0, EventName = home undefined, DoneAt = 10/7/2023 10:53:12 PM, WebsiteUser =  }) ■▄▀■
10-08 02:28    0  WebEventLogsController.Post(WebEventLog { BrowserSignature = Windows NT 10.0; Win64; x64**117.0.2045.47**en-US,en,uk,ru**CPU:12**ANGLE (Intel, Intel(R) UHD Graphics 630 Direct3D11 vs_5_0 ps_5_0, D3D11)**Google Inc. (Intel)., Id = 0, WebsiteUserId = 0, EventName = ocr-, DoneAt = 10/8/2023 2:28:43 AM, WebsiteUser =  }) ■▄▀■
10-08 02:30    1  WebEventLogsController.Post(WebEventLog { BrowserSignature = Windows NT 10.0; Win64; x64**117.0.2045.47**en-US,en,uk,ru**CPU:12**ANGLE (Intel, Intel(R) UHD Graphics 630 Direct3D11 vs_5_0 ps_5_0, D3D11)**Google Inc. (Intel)., Id = 0, WebsiteUserId = 0, EventName = ocr-, DoneAt = 10/8/2023 2:30:50 AM, WebsiteUser =  }) ■▄▀■
10-08 02:30    2  WebEventLogsController.Post(WebEventLog { BrowserSignature = Windows NT 10.0; Win64; x64**117.0.2045.47**en-US,en,uk,ru**CPU:12**ANGLE (Intel, Intel(R) UHD Graphics 630 Direct3D11 vs_5_0 ps_5_0, D3D11)**Google Inc. (Intel)., Id = 0, WebsiteUserId = 0, EventName = home undefined, DoneAt = 10/8/2023 2:30:59 AM, WebsiteUser =  }) ■▄▀■
10-08 02:31    3  WebEventLogsController.Post(WebEventLog { BrowserSignature = Windows NT 10.0; Win64; x64**117.0.2045.47**en-US,en,uk,ru**CPU:12**ANGLE (Intel, Intel(R) UHD Graphics 630 Direct3D11 vs_5_0 ps_5_0, D3D11)**Google Inc. (Intel)., Id = 0, WebsiteUserId = 0, EventName = home undefined, DoneAt = 10/8/2023 2:31:01 AM, WebsiteUser =  }) ■▄▀■
10-08 08:37    0  AlexPiApi.Controllers.GuestbookMsgsController  RAZER1  Get()
10-08 08:46:48  -----  a call from the inspector  -----
10-08 13:01    0  WebEventLogsController.Post(WebEventLog { BrowserSignature = Windows NT 10.0; Win64; x64**117.0.2045.47**en-US,en,uk,ru**CPU:12**ANGLE (Intel, Intel(R) UHD Graphics 630 Direct3D11 vs_5_0 ps_5_0, D3D11)**Google Inc. (Intel)., Id = 0, WebsiteUserId = 0, EventName = ocr-, DoneAt = 10/8/2023 1:01:18 PM, WebsiteUser =  }) ■▄▀■
10-08 13:21    1  WebEventLogsController.Post(WebEventLog { BrowserSignature = Windows NT 10.0; Win64; x64**117.0.2045.47**en-US,en,uk,ru**CPU:12**ANGLE (Intel, Intel(R) UHD Graphics 630 Direct3D11 vs_5_0 ps_5_0, D3D11)**Google Inc. (Intel)., Id = 0, WebsiteUserId = 0, EventName = ocr-, DoneAt = 10/8/2023 1:21:26 PM, WebsiteUser =  }) ■▄▀■
10-08 13:21    2  WebEventLogsController.Post(WebEventLog { BrowserSignature = Windows NT 10.0; Win64; x64**117.0.2045.47**en-US,en,uk,ru**CPU:12**ANGLE (Intel, Intel(R) UHD Graphics 630 Direct3D11 vs_5_0 ps_5_0, D3D11)**Google Inc. (Intel)., Id = 0, WebsiteUserId = 0, EventName = ocr-, DoneAt = 10/8/2023 1:21:52 PM, WebsiteUser =  }) ■▄▀■
10-08 13:22    3  WebEventLogsController.Post(WebEventLog { BrowserSignature = Windows NT 10.0; Win64; x64**117.0.2045.47**en-US,en,uk,ru**CPU:12**ANGLE (Intel, Intel(R) UHD Graphics 630 Direct3D11 vs_5_0 ps_5_0, D3D11)**Google Inc. (Intel)., Id = 0, WebsiteUserId = 0, EventName = ocr-, DoneAt = 10/8/2023 1:22:01 PM, WebsiteUser =  }) ■▄▀■
10-08 13:22    4  WebEventLogsController.Post(WebEventLog { BrowserSignature = Windows NT 10.0; Win64; x64**117.0.2045.47**en-US,en,uk,ru**CPU:12**ANGLE (Intel, Intel(R) UHD Graphics 630 Direct3D11 vs_5_0 ps_5_0, D3D11)**Google Inc. (Intel)., Id = 0, WebsiteUserId = 0, EventName = ttmr, DoneAt = 10/8/2023 1:22:03 PM, WebsiteUser =  }) ■▄▀■
10-08 13:46    0  WebEventLogsController.Post(WebEventLog { BrowserSignature = Linux; Android 10; K 117.0.**Safari/537.36**en-GB**CPU:8**ANGLE (Qualcomm, Adreno (TM) 630, OpenGL ES 3.2)**Google Inc. (Qualcomm)., Id = 0, WebsiteUserId = 0, EventName = home undefined, DoneAt = 10/8/2023 1:46:40 PM, WebsiteUser =  }) ■▄▀■
10-08 13:46    1  WebEventLogsController.Post(WebEventLog { BrowserSignature = Linux; Android 10; K 117.0.**Safari/537.36**en-GB**CPU:8**ANGLE (Qualcomm, Adreno (TM) 630, OpenGL ES 3.2)**Google Inc. (Qualcomm)., Id = 0, WebsiteUserId = 0, EventName = ttmr, DoneAt = 10/8/2023 1:46:43 PM, WebsiteUser =  }) ■▄▀■
10-08 13:50    2  WebEventLogsController.Post(WebEventLog { BrowserSignature = Windows NT 10.0; Win64; x64**117.0.2045.47**en-US,en,uk,ru**CPU:12**ANGLE (Intel, Intel(R) UHD Graphics 630 Direct3D11 vs_5_0 ps_5_0, D3D11)**Google Inc. (Intel)., Id = 0, WebsiteUserId = 0, EventName = home undefined, DoneAt = 10/8/2023 1:50:28 PM, WebsiteUser =  }) ■▄▀■";
}
