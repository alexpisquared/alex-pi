namespace AzureLogParser;
public partial class LogParserVM : ObservableValidator
{
  public LogParserVM() => _logParser = new LogParser(new ConfigurationBuilder().AddUserSecrets<App>().Build()["SecretKey"] ?? "no key");

  const StringComparison _sc = StringComparison.OrdinalIgnoreCase;
  readonly LogParser _logParser;
  readonly SpeechSynthesizer _synth = new();

  [ObservableProperty] bool _isBusy;
  [ObservableProperty] string? _logRaw;
  [ObservableProperty] string? _report;
  /*[ObservableProperty]*/
  string? _memberSinceKey; //partial void OnMemIdChanged(string? value) => WebEventLogs?.Refresh();

  [ObservableProperty] ICollectionView? _webEventLogs;
  [ObservableProperty] ICollectionView? _eventtGroups;
  [ObservableProperty] ICollectionView? _websiteUsers;
  [ObservableProperty] WebEventLog? _selWE; partial void OnSelWEChanged(WebEventLog? value)
  {
    UnselectAllCommand?.NotifyCanExecuteChanged();

    if (value is null) return;

    _memberSinceKey = null;
    //WebEventLogs?.Refresh();
    EventtGroups?.Refresh();
    WebsiteUsers?.Refresh();
  }
  [ObservableProperty] EventtGroup? _selEG; partial void OnSelEGChanged(EventtGroup? value)
  {
    UnselectAllCommand?.NotifyCanExecuteChanged();

    if (value is null) return;

    SelWU = null;
    _memberSinceKey = null;
    WebEventLogs?.Refresh();
    //EventtGroups?.Refresh();
    WebsiteUsers?.Refresh();

    //RunReLoadCommand?.NotifyCanExecuteChanged();
  }
  [ObservableProperty] WebsiteUser? _selWU; partial void OnSelWUChanged(WebsiteUser? value)
  {
    UnselectAllCommand?.NotifyCanExecuteChanged();

    if (value is null) return;

    SelEG = null;
    _memberSinceKey = value.MemberSinceKey;
    WebEventLogs?.Refresh();
    EventtGroups?.Refresh();
    //WebsiteUsers?.Refresh();

    //RunReLoadCommand?.NotifyCanExecuteChanged();
  }
  [ObservableProperty] bool _excludeApiCaHome; partial void OnExcludeApiCaHomeChanged(bool oldValue, bool newValue) { WebEventLogs?.Refresh(); ; }
  [ObservableProperty] bool _tttRotationOnly; partial void OnTttRotationOnlyChanged(bool oldValue, bool newValue) { WebEventLogs?.Refresh(); ; }

  [RelayCommand(CanExecute = nameof(CanLoadOldTx))] public async Task LoadOldTx() => await LoadOldTx_(); bool CanLoadOldTx() => !IsBusy; async Task LoadOldTx_()
  {
    var before = LogRaw?.Length;
    var files = Directory.GetFiles(@"C:\Users\alexp\source\repos\alex-pi\AzureLogParser\Data\", "*.txt"); // read all *.txt files from $@"C:\Users\alexp\source\repos\alex-pi\AzureLogParser\Data\*.txt" and append contents of them into a single string:
    LogRaw += string.Join("\r\n\r\n", files.Select(File.ReadAllText));
    var after = LogRaw.Length;

    var (logRaw0, eLogs, users) = await _logParser.AddLogLinesToLists(LogRaw);
    //todo: restore _ = await ReLoadCVSs_CheckIfNews(logRaw0, eLogs, users);

    Report = $"Characters: {before:N0} => {after:N0}.";
    Console.Beep(360, 100);
  }
  [RelayCommand(CanExecute = nameof(CanRunReLoad))] public async Task RunReLoad() => _ = await ReLoadLists_CheckIfNews(false); bool CanRunReLoad() => !IsBusy;
  [RelayCommand(CanExecute = nameof(CanCreateLog))] public async Task CreateLog() => await DoCrud_('c'); bool CanCreateLog() => !IsBusy;
  [RelayCommand(CanExecute = nameof(CanUpdateLog))] public async Task UpdateLog() => await DoCrud_('u'); bool CanUpdateLog() => !IsBusy;
  [RelayCommand(CanExecute = nameof(CanDeleteLog))] public async Task DeleteLog() => await DoCrud_('d'); bool CanDeleteLog() => !IsBusy;
  [RelayCommand(CanExecute = nameof(CanAppendLog))] public async Task AppendLog() => await DoCrud_('a'); bool CanAppendLog() => !IsBusy;

  [RelayCommand(CanExecute = nameof(CanUnselectAll))]
  public async Task UnselectAll()
  {
    SelWE = null;
    SelEG = null;
    SelWU = null;

    WebEventLogs?.Refresh();
    EventtGroups?.Refresh();
    WebsiteUsers?.Refresh();

    //RunReLoadCommand?.NotifyCanExecuteChanged();

    Console.Beep(360, 100);
    await Task.Delay(26);
  }
  bool CanUnselectAll() => SelWE is not null || SelEG is not null || SelWU is not null;

  [RelayCommand] public async Task UpdateNicks() { await TrySave(); _ = await ReLoadLists_CheckIfNews(false); await UnselectAll(); }
  [RelayCommand] public async Task MakeUnique() { Console.Beep(360, 100); await Task.Delay(26); }

  async Task DoCrud_(char crud) { IsBusy = true; Report = $"{crud}-ing the log file on remote Azure location..."; _ = await ReLoadCVSs_CheckIfNews(crud); }
  public async Task<bool> ReLoadLists_CheckIfNews(bool sayIt) => await ReLoadCVSs_CheckIfNews('r', sayIt);
  public async Task<bool> ReLoadCVSs_CheckIfNews(char crud, bool sayIt = false)
  {
    //Console.Beep(260, 500);
    IsBusy = true;
    Report = "Loading...";

    try
    {
      var (logRaw_, eLogs_, users_) = await _logParser.DoCRUD(crud);

      LogRaw = logRaw_;

      CheckFindVisitorsWhoClickedBroadcastEmailLink(eLogs_, users_);

      WebEventLogs = CollectionViewSource.GetDefaultView(eLogs_.OrderByDescending(r => r.DoneAt).ToList());
      WebEventLogs.SortDescriptions.Add(new SortDescription(nameof(WebEventLog.DoneAt), ListSortDirection.Descending));
      WebEventLogs.Filter = obj => obj is not WebEventLog w || w is null || (
                                    (ExcludeApiCaHome == false || (w.EventName.Contains("home") == false && w.EventName.Contains("ttt") == false)) &&
                                    (TttRotationOnly == false || w.EventName.Contains("ttt") == true) &&
                                    (string.IsNullOrEmpty(_memberSinceKey) || w.FirstVisitId?.Equals(_memberSinceKey, _sc) == true) &&
                                    (string.IsNullOrEmpty(SelEG?.Hardware) || w.Sub[0]?.Equals(SelEG.Hardware, _sc) == true) &&
                                    (string.IsNullOrEmpty(SelEG?.MozillaV) || w.Sub[1]?.Equals(SelEG.MozillaV, _sc) == true) &&
                                    (string.IsNullOrEmpty(SelEG?.Versions) || w.Sub[2]?.Equals(SelEG.Versions, _sc) == true) &&
                                    (string.IsNullOrEmpty(SelEG?.CpuCores) || w.Sub[3]?.Equals(SelEG.CpuCores, _sc) == true) &&
                                    (string.IsNullOrEmpty(SelEG?.Platform) || w.Sub[4]?.Equals(SelEG.Platform, _sc) == true) &&
                                    (string.IsNullOrEmpty(SelEG?.Language) || w.Sub[5]?.Equals(SelEG.Language, _sc) == true) &&
                                    (string.IsNullOrEmpty(SelEG?.Resolute) || w.Sub[6]?.Equals(SelEG.Resolute, _sc) == true));

      WebsiteUsers = CollectionViewSource.GetDefaultView(users_.OrderByDescending(r => r.LastVisitAt).ToList());
      WebsiteUsers.SortDescriptions.Add(new SortDescription("LastVisitAt", ListSortDirection.Descending));
      WebsiteUsers.Filter = obj => obj is not WebsiteUser w || w is null || string.IsNullOrEmpty(SelWE?.NickUser) || w.Nickname?.Equals(SelWE?.NickUser, _sc) == true;

      EventtGroups = CollectionViewSource.GetDefaultView(eLogs_.GroupBy(log => new
      {
        Hardware = log.Sub[0],
        MozillaVer = log.Sub[01],
        Versions = log.Sub[02],
        CPUCORES = log.Sub[03].TrimStart('0'),
        Platform = log.Sub[04],
        Languages = log.Sub[05],
        Resolution = log.Sub[06],
      }).Select(r => new EventtGroup
      {
        LastVisitAt = r.Max(r => r.DoneAt),
        Count = r.Count(),
        Hardware = r.Key.Hardware,
        MozillaV = r.Key.MozillaVer,
        Versions = r.Key.Versions,
        CpuCores = r.Key.CPUCORES,
        Platform = r.Key.Platform,
        Language = r.Key.Languages,
        Resolute = r.Key.Resolution,
        NickWare = _logParser.NickMapperWare($"{r.Key.Hardware}|{r.Key.MozillaVer}|{r.Key.Versions}|{r.Key.CPUCORES}|{r.Key.Platform}|{r.Key.Languages}|{r.Key.Resolution}")
      }).ToList());
      //foreach (EventtGroup eg in EventtGroups) eg.NickWare = _logParser.NickMapperWare(eg.PseudoKey);
      EventtGroups.SortDescriptions.Add(new SortDescription("LastVisitAt", ListSortDirection.Descending));
      EventtGroups.Filter = obj => obj is not EventtGroup w || w is null || string.IsNullOrEmpty(SelWE?.NickWare) || w.NickWare?.Equals(SelWE?.NickWare, _sc) == true;

      var isNew = MiscServices.NotifyIfThereAreNewLogEntriesAndStoreLastNewLogTime(eLogs_.Max(r => r.DoneAt), @"C:\temp\potentiallyNewUsageTime.txt");
      Report = isNew ? "New visits detected!" : "-- Nothing new --"; //tbkReport.Foreground = isNew ? Brushes.GreenYellow : Brushes.Gray;
      if (/*isNew &&*/ sayIt)
        _ = _synth.SpeakAsync(Report);

      return isNew;
    }
    catch (Exception ex) { /*_ = MessageBox.Show(ex.Message);*/ Report = ex.Message; WriteLine($"\n{DateTime.Now:yyyy-MM-dd HH:mm}  ERR  {ex}"); }
    finally { IsBusy = false; }

    return true;
  }

  private static void CheckFindVisitorsWhoClickedBroadcastEmailLink(List<WebEventLog> eLogs_, List<WebsiteUser> users_)
  {
    var allLogLines = ReadAllLinesFromPotentiallyLockedLogFile();

    foreach (var webEventLog in eLogs_.Where(r => r.EventName.Contains(':')))
    {
      // 1. Find the matching line in the broadcast log file.
      var line = allLogLines.FirstOrDefault(line => line.Contains(webEventLog.EventName.Split(':')[1]));
      if (line is null) continue;

      // 2. Extract the email address from the line.
      var email = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Last();
      if (string.IsNullOrEmpty(email)) continue;

      // 3. Find the WebsiteUser with the FirstVisitId.
      var matchingUser = users_.FirstOrDefault(user => user.MemberSinceKey == webEventLog.FirstVisitId);
      if (matchingUser is null) continue;

      // 4. Check if the email address is different from the one in the WebsiteUser.
      if (!matchingUser.Nickname.Contains(email))
      {
        // 5. If different, update the WebsiteUser with the email address.
        matchingUser.Nickname = email;
      }
    }
  }

  public async Task TrySave()
  {
    _ = await _logParser.UpdateIfNewUser(WebsiteUsers);
    _ = await _logParser.UpdateIfNewWare(EventtGroups);
    _ = _synth.SpeakAsync("Saved");
    await Task.Delay(260);
  }

  internal async Task Bingo(WebEventLog? webEventLog, string eml)
  {
    if (WebsiteUsers?.SourceCollection is IEnumerable<WebsiteUser> websiteUsers)
    {
      var matchingUser = websiteUsers.FirstOrDefault(user => user.MemberSinceKey == webEventLog?.FirstVisitId);
      if (matchingUser != null)
      {
        //wipes out the list:
        matchingUser.Nickname = eml;
        //WebsiteUsers?.Refresh();
        await UnselectAll();
      }
    }
  }

  static List<string> ReadAllLinesFromPotentiallyLockedLogFile()
  {
    var lines = new List<string>();
    if (File.Exists(_broadcastLogFile))
    {
      using var fileStream = new FileStream(_broadcastLogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); // Open the potentially locked file.
      using var streamReader = new StreamReader(fileStream);
      string? line;
      while ((line = streamReader.ReadLine()) != null)
      {
        lines.Add(line);
      }
    }

    return lines;
  }
  const string _broadcastLogFile = @"C:\Users\alexp\OneDrive\Public\Logs\MinNavTpl.RAZ.ale.Infi..log";
}