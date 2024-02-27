using Db.OneBase.Model;

namespace AzureLogParser;
public class LogParser
{
  readonly string _key;
  string? _logRaw;

  public LogParser(string key) => _key = key;
  readonly UniMapper _userMap = new(@"C:\Users\alexp\source\repos\alex-pi\AzureLogParser\UserMap.json");
  readonly UniMapper _wareMap = new(@"C:\Users\alexp\source\repos\alex-pi\AzureLogParser\WareMap.json");

  public async Task<string> LogRaw()
  {
    if (_logRaw is null)
    {
      _logRaw = await new PoorMansLogger(_key).ReadFileAsync();
      //tmi: WriteLine($"{_logRaw}");
    }

    return _logRaw;
  }
  public async Task<(string logRaw, List<WebEventLog> webEventLogs, List<WebsiteUser> websiteUsers)> DoCRUD(char crud)
  {
    var webEventLogs = new List<WebEventLog>();
    var websiteUsers = new List<WebsiteUser>();

    try
    {
      if (DateTime.Now == DateTime.Today) //nogo: @wo //tu: @ho!!!
      {
        var client = new SecretClient(new Uri("https://demopockv.vault.azure.net/"), new DefaultAzureCredential());
        KeyVaultSecret secret = client.GetSecret("ChtBlobStorage_key");
        var connectionString = secret.Value;
      }

      var poorMansLogger = new PoorMansLogger(_key);

      if (DateTime.Now == DateTime.Today)
        await poorMansLogger.AppendToFileAsync($"{DateTime.Now:MM-dd HH:mm}  -----  a call from the NEW(!) inspector's start  -----\r\n");

      if (crud == 'c')
      {
        await poorMansLogger.CreateFileAsync("Creating...");
      }
      else if (crud == 'u')
      {
        await poorMansLogger.UpdateFileAsync($"-- Updated with this at {DateTime.Now} --\n");
      }
      else if (crud == 'd')
      {
        var lr = await LogRaw();
        _ = MiscServices.SaveBlob(lr ?? "Nothing here", $@"C:\Users\alexp\source\repos\alex-pi\AzureLogParser\Data\AzureTttLog.{DateTime.Now:yyMMdd-HHmmss}.txt");
        await poorMansLogger.DeleteFileAsync();
      }
      else if (crud == 'a')
      {
        await poorMansLogger.AppendToFileAsync($"++ Appending with this at {DateTime.Now} ++\n");
      }
      else if (crud == 'r')
      {
        return await AddLogLinesToLists(await LogRaw(), webEventLogs, websiteUsers);
      }

      return ("Really???????", webEventLogs, websiteUsers);
    }
    catch (Exception ex)
    {
      return (ex.Message, webEventLogs, websiteUsers);
    }
  }
  public async Task<(string logRaw, List<WebEventLog> webEventLogs, List<WebsiteUser> websiteUsers)> AddLogLinesToLists(string logRaw)
  {
    var webEventLogs = new List<WebEventLog>();
    var websiteUsers = new List<WebsiteUser>();

    return await AddLogLinesToLists(logRaw, webEventLogs, websiteUsers);
  }

  async Task<(string logRaw, List<WebEventLog> webEventLogs, List<WebsiteUser> websiteUsers)> AddLogLinesToLists(string logRaw, List<WebEventLog> webEventLogs, List<WebsiteUser> websiteUsers)
  {
    foreach (var line in logRaw.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList().Where(r => r.Contains(", FirstVisitId = ")).ToList())
    {
      try
      {
        var log = new WebEventLog
        {
          DoneAt = DateTime.Parse(line.Split(new string[] { "DoneAt = ", ", WebsiteUser =" }, StringSplitOptions.RemoveEmptyEntries)[1]).ToLocalTime(),
          EventName = line.Split(new string[] { "EventName = ", ", DoneAt =" }, StringSplitOptions.RemoveEmptyEntries)[1],
          BrowserSignature = line.Split(new string[] { "BrowserSignature = ", ", FirstVisitId = " }, StringSplitOptions.RemoveEmptyEntries)[1],
          FirstVisitId = line.Split(new string[] { ", FirstVisitId = ", ", Id = " }, StringSplitOptions.RemoveEmptyEntries)[1],
          NickUser = NickMapperUser(line.Split(new string[] { ", FirstVisitId = ", ", Id = " }, StringSplitOptions.RemoveEmptyEntries)[1]),
        };

        log.Sub = $"{log.BrowserSignature}|°|°|°|°|°|°|°|°|°|°|°|.".Split(new string[] { "║", "│", "|", ", NickUser = " }, StringSplitOptions.RemoveEmptyEntries);

        var r = new
        {
          Hardware = log.Sub[0],
          MozillaVer = log.Sub[01],
          Versions = log.Sub[02],
          CPUCORES = log.Sub[03].TrimStart('0'),
          Platform = log.Sub[04],
          Languages = log.Sub[05],
          Resolution = log.Sub[06],
        };
        var eg = new EventtGroup
        {
          Hardware = r.Hardware,
          MozillaV = r.MozillaVer,
          Versions = r.Versions,
          CpuCores = r.CPUCORES,
          Platform = r.Platform,
          Language = r.Languages,
          Resolute = r.Resolution
        };

        log.NickWare = NickMapperWare(eg.PseudoKey);

        //if (log.DoneAt > new DateTime(2023, 10, 15))
        webEventLogs.Add(log);
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
      Nickname = NickMapperUser(g.Key),
      Note = $"{g.Max(r => r.DoneAt) - g.Min(r => r.DoneAt),15}{g.Count(),4}",
      CreatedAt = g.Min(r => r.DoneAt),
      LastVisitAt = g.Max(r => r.DoneAt),
      TotalVisits = g.Count()
    }).OrderBy(r => r.LastVisitAt)
    .ToList()
    .ForEach(websiteUsers.Add);

    return (await LogRaw(), webEventLogs, websiteUsers);
  }

  public async Task<bool> UpdateIfNewUser(ICollectionView? users) => users != null && await _userMap.UpdateIfNewAsync(users);
  public async Task<bool> UpdateIfNewWare(ICollectionView? wares) => wares != null && await _wareMap.UpdateIfNewAsync(wares);

  string NickMapperUser(string key) => _userMap.GetOrCreateFromId(key.Replace(", NickUser = ", ""));
  public string NickMapperWare(string key) => _wareMap.GetOrCreateFromId(key);

  //internal void UpdateIfDifferent(string v1, string v2, int displayIndex) => _userMap.UpdateIfDifferent(v1, v2, displayIndex);
  const string _log = """
10-08 13:46    0  WebEventLogsController.Post(WebEventLog { BrowserSignature = Linux; Android 10; K 117.0.**Safari/537.36**en-GB**CpuCores:8**ANGLE (Qualcomm, Adreno (TM) 630, OpenGL ES 3.2)**Google Inc. (Qualcomm)., Id = 0, WebsiteUserId = 0, EventName = home undefined, DoneAt = 10/8/2023 1:46:40 PM, WebsiteUser =  }) ■▄▀■
10-08 13:46    1  WebEventLogsController.Post(WebEventLog { BrowserSignature = Linux; Android 10; K 117.0.**Safari/537.36**en-GB**CpuCores:8**ANGLE (Qualcomm, Adreno (TM) 630, OpenGL ES 3.2)**Google Inc. (Qualcomm)., Id = 0, WebsiteUserId = 0, EventName = ttmr, DoneAt = 10/8/2023 1:46:43 PM, WebsiteUser =  }) ■▄▀■
10-08 13:50    2  WebEventLogsController.Post(WebEventLog { BrowserSignature = Windows NT 10.0; Win64; x64**117.0.2045.47**en-US,en,uk,ru**CpuCores:12**ANGLE (Intel, Intel(R) UHD Graphics 630 Direct3D11 vs_5_0 ps_5_0, D3D11)**Google Inc. (Intel)., Id = 0, WebsiteUserId = 0, EventName = home undefined, DoneAt = 10/8/2023 1:50:28 PM, WebsiteUser =  }) ■▄▀■
""";
}