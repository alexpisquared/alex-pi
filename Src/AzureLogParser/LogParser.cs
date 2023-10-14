using System.Windows;
using AlexPiApi.Services;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Db.OneBase.Model;
using static System.Diagnostics.Trace;

namespace AzureLogParser;

public class LogParser
{
  public async Task<(string logRaw, string usage2)> DoCRUD(char crud, string constr)
  {
    try
    {
      if (DateTime.Now == DateTime.Today) //nogo: @wo
      {
        var client = new SecretClient(new Uri("https://demopockv.vault.azure.net/"), new DefaultAzureCredential());
        KeyVaultSecret secret = client.GetSecret("ChtBlobStorageConnectionString");
        var connectionString = secret.Value;
      }

      var poorMansLogger = new PoorMansLogger(constr);

      //// Create a file    await poorMansLogger.CreateFileAsync(containerName, fileName, content);
      //// Read the file    var readContent = await poorMansLogger.ReadFileAsync(containerName, fileName);
      //// Update the file  await poorMansLogger.UpdateFileAsync(containerName, fileName, newContent);

      if (DateTime.Now == DateTime.Today)
        await poorMansLogger.AppendToFileAsync($"{DateTime.Now:MM-dd HH:mm}  -----  a call from the NEW(!) inspector's start  -----\r\n");

      if (crud == 'c')
      {
        await poorMansLogger.CreateFileAsync("Creating...");
        return ("", "");
      }

      if (crud == 'r')
      {
        var logRaw = await poorMansLogger.ReadFileAsync(); // _log; //
        WriteLine($"{logRaw}");

        var WebEventLogs = new List<WebEventLog>();
        foreach (var line in logRaw.Split("\r\n").ToList().Where(r => r.Contains(", FirstVisitId = ")).ToList())
        {
          try
          {
            var WebEventLog = new WebEventLog
            {
              DoneAt = DateTime.Parse(line.Split(new string[] { "DoneAt = ", ", WebsiteUser =" }, StringSplitOptions.RemoveEmptyEntries)[1]),
              EventName = line.Split(new string[] { "EventName = ", "DoneAt =" }, StringSplitOptions.RemoveEmptyEntries)[1],
              BrowserSignature = line.Split(new string[] { ", FirstVisitId = ", ", Id = " }, StringSplitOptions.RemoveEmptyEntries)[1]
            };

            if (WebEventLog.DoneAt.ToLocalTime() > new DateTime(2023, 10, 9, 22, 50, 0))
              WebEventLogs.Add(WebEventLog);
          }
          catch (Exception ex)
          {
            WriteLine($"{ex.Message}  {line}");
          }
        }

        var usage2 = "";
        // select unique BrowserSignature With Max and Min Of DoneAt and the count records per BrowserSignature of from WebEventLogs
        WebEventLogs.GroupBy(r => r.BrowserSignature).Select(g => new { g.Key, Max = g.Max(r => r.DoneAt), Min = g.Min(r => r.DoneAt), Count = g.Count() }).OrderBy(r => r.Max).ToList().ForEach(r => usage2 += $" {r.Min.ToLocalTime():yy-MM-dd} .. {r.Max.ToLocalTime():MM-dd HH:mm} {r.Max - r.Min,15}{r.Count,4} \t {Whoser(r.Key)}\n");

        return (logRaw, usage2);
      }

      if (crud == 'u')
      {
        await poorMansLogger.UpdateFileAsync($"-- Updated with this at {DateTime.Now} --");
        return ("", "");
      }

      if (crud == 'd')
      {
        await poorMansLogger.DeleteFileAsync();
        return ("", "");
      }

      if (crud == 'a')
      {
        await poorMansLogger.AppendToFileAsync($"++ Appending with this at {DateTime.Now}\n");
        return ("", "");
      }

      return ("Really???????", "");
    }
    catch (Exception ex)
    {
      return (ex.Message, "");
    }
  }

  string Whoser(string sign)
  {
    var rv = sign switch
    {
      "Nothing" => "Nothing",
      "Wed Oct 11 2023 17:53:27 GMT-0400 (Eastern Daylight Time)" => "Razer1",
      "Wed Oct 11 2023 21:41:23 GMT-0400 (Eastern Daylight Time)" => "Pixel Chrome",
      "Wed Oct 11 2023 21:43:22 GMT-0400 (Eastern Daylight Saving Time)" => "Zoe IPhone",
      "Fri Oct 13 2023 16:31:36 GMT-0400 (Eastern Daylight Saving Time)" => "Alx IPhone",
      "Fri Oct 13 2023 18:54:17 GMT-0400 (Eastern Daylight Saving Time)" => "Mei IPhone",
      "Wed Oct 11 2023 21:44:23 GMT-0400 (Eastern Daylight Saving Time)" => "TTTimer ???",
      "Wed Oct 11 2023 17:52:19 GMT-0400 (Eastern Daylight Time)" => "localhost:4202",
      "Thu Oct 12 2023 15:31:50 GMT-0400 (Eastern Daylight Time)" => "Nuc2",
      "Thu Oct 12 2023 19:43:40 GMT-0400 (Eastern Daylight Time)" => "black tablet",
      "No LocalStorage engagement yet." => "No LocalStorage engagement yet",
      _ => null,
    };

    if (rv is null)
    {
      rv = $"\"{sign}\" => \"{DateTime.Now:yyMMdd.HHmmss}\",";
      Clipboard.SetText(rv);
    }

    return rv;
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
