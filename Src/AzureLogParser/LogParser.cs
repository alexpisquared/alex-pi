using AlexPiApi.Services;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Db.OneBase.Model;
using static System.Diagnostics.Trace;
namespace AzureLogParser;
public class LogParser
{
  readonly UserMap _userMap = new();
  public async Task<(string logRaw, List<WebEventLog> webEventLogs, List<WebsiteUser> websiteUsers)> DoCRUD(char crud, string connectionString)
  {
    var webEventLogs = new List<WebEventLog>();
    var websiteUsers = new List<WebsiteUser>();

    try
    {
      if (DateTime.Now == DateTime.Today) //nogo: @wo //tu: @ho!!!
      {
        var client = new SecretClient(new Uri("https://demopockv.vault.azure.net/"), new DefaultAzureCredential());
        KeyVaultSecret secret = client.GetSecret("ChtBlobStorageConnectionString");
        connectionString = secret.Value;
      }

      var poorMansLogger = new PoorMansLogger(connectionString);

      if (DateTime.Now == DateTime.Today)
        await poorMansLogger.AppendToFileAsync($"{DateTime.Now:MM-dd HH:mm}  -----  a call from the NEW(!) inspector's start  -----\r\n");

      if (crud == 'c')
      {
        await poorMansLogger.CreateFileAsync("Creating...");
      }
      else if (crud == 'r')
      {
        var logRaw = await poorMansLogger.ReadFileAsync(); // _log; //
        WriteLine($"{logRaw}");

        foreach (var line in logRaw.Split("\r\n").ToList().Where(r => r.Contains(", FirstVisitId = ")).ToList())
        {
          try
          {
            var webEventLog = new WebEventLog
            {
              DoneAt = DateTime.Parse(line.Split(new string[] { "DoneAt = ", ", WebsiteUser =" }, StringSplitOptions.RemoveEmptyEntries)[1]).ToLocalTime(),
              EventName = line.Split(new string[] { "EventName = ", ", DoneAt =" }, StringSplitOptions.RemoveEmptyEntries)[1],
              BrowserSignature = line.Split(new string[] { "BrowserSignature = ", ", FirstVisitId = " }, StringSplitOptions.RemoveEmptyEntries)[1],
              FirstVisitId = line.Split(new string[] { ", FirstVisitId = ", ", Id = " }, StringSplitOptions.RemoveEmptyEntries)[1],
              Nickname = NickMapper(line.Split(new string[] { ", FirstVisitId = ", ", Id = " }, StringSplitOptions.RemoveEmptyEntries)[1]),
            };

            webEventLog.Sub = $"{webEventLog.BrowserSignature}|°|°|°|°|°|°|°|°|°|°|°|.".Split(new string[] { "║", "│", "|", ", Nickname = " }, StringSplitOptions.RemoveEmptyEntries);

            //if (webEventLog.DoneAt > new DateTime(2023, 10, 15))
            webEventLogs.Add(webEventLog);
          }
          catch (Exception ex)
          {
            WriteLine($"{ex.Message}  {line}");
          }
        }

        // select unique FirstVisitId With Max and Min Of DoneAt and the count records per FirstVisitId of from webEventLogs
        webEventLogs.GroupBy(r => r.FirstVisitId).Select(g => new WebsiteUser
        {
          Id = 0,
          MemberSinceKey = g.Key,
          Nickname = NickMapper(g.Key),
          Note = $"{g.Max(r => r.DoneAt) - g.Min(r => r.DoneAt),15}{g.Count(),4}",
          CreatedAt = g.Min(r => r.DoneAt),
          LastVisitAt = g.Max(r => r.DoneAt),
          Spread = g.Max(r => r.DoneAt) - g.Min(r => r.DoneAt),
          TotalVisits = g.Count()
        }).OrderBy(r => r.LastVisitAt).ToList().
          ForEach(websiteUsers.Add);

        return (logRaw, webEventLogs, websiteUsers);
      }
      else if (crud == 'u')
      {
        await poorMansLogger.UpdateFileAsync($"-- Updated with this at {DateTime.Now} --\n");
      }
      else if (crud == 'd')
      {
        await poorMansLogger.DeleteFileAsync();
      }
      else if (crud == 'a')
      {
        await poorMansLogger.AppendToFileAsync($"++ Appending with this at {DateTime.Now}\n");
      }

      return ("Really???????", webEventLogs, websiteUsers);
    }
    catch (Exception ex)
    {
      return (ex.Message, webEventLogs, websiteUsers);
    }
  }
  string NickMapper(string firstVisitStr) => _userMap.GetOrCreateUsernameFromId(firstVisitStr.Replace(", Nickname = ", ""));

  internal void UpdateIfDifferent(string v1, string v2, int displayIndex) => _userMap.UpdateIfDifferent(v1, v2, displayIndex);

  const string _log = """
10-08 13:46    0  WebEventLogsController.Post(WebEventLog { BrowserSignature = Linux; Android 10; K 117.0.**Safari/537.36**en-GB**CPU:8**ANGLE (Qualcomm, Adreno (TM) 630, OpenGL ES 3.2)**Google Inc. (Qualcomm)., Id = 0, WebsiteUserId = 0, EventName = home undefined, DoneAt = 10/8/2023 1:46:40 PM, WebsiteUser =  }) ■▄▀■
10-08 13:46    1  WebEventLogsController.Post(WebEventLog { BrowserSignature = Linux; Android 10; K 117.0.**Safari/537.36**en-GB**CPU:8**ANGLE (Qualcomm, Adreno (TM) 630, OpenGL ES 3.2)**Google Inc. (Qualcomm)., Id = 0, WebsiteUserId = 0, EventName = ttmr, DoneAt = 10/8/2023 1:46:43 PM, WebsiteUser =  }) ■▄▀■
10-08 13:50    2  WebEventLogsController.Post(WebEventLog { BrowserSignature = Windows NT 10.0; Win64; x64**117.0.2045.47**en-US,en,uk,ru**CPU:12**ANGLE (Intel, Intel(R) UHD Graphics 630 Direct3D11 vs_5_0 ps_5_0, D3D11)**Google Inc. (Intel)., Id = 0, WebsiteUserId = 0, EventName = home undefined, DoneAt = 10/8/2023 1:50:28 PM, WebsiteUser =  }) ■▄▀■
""";
}
