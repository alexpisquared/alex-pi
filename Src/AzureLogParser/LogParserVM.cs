namespace AzureLogParser;
public partial class LogParserVM : ObservableValidator
{
  public LogParserVM() => _logParser = new LogParser(new ConfigurationBuilder().AddUserSecrets<App>().Build()["SecretKey"] ?? "no key");

  const StringComparison sc = StringComparison.OrdinalIgnoreCase;
  readonly LogParser _logParser;
  readonly SpeechSynthesizer synth = new();

  [ObservableProperty] bool isBusy;
  [ObservableProperty] string? logRaw;
  [ObservableProperty] string? report;
  /*[ObservableProperty]*/
  string? MemberSinceKey; //partial void OnMemIdChanged(string? value) => WebEventLogs?.Refresh();

  [ObservableProperty] ICollectionView? webEventLogs;
  [ObservableProperty] ICollectionView? eventtGroups;
  [ObservableProperty] ICollectionView? websiteUsers;
  [ObservableProperty] WebEventLog? selWE; partial void OnSelWEChanged(WebEventLog? value)
  {
    UnselectAllCommand?.NotifyCanExecuteChanged();

    if (value is null) return;

    MemberSinceKey = null;
    //WebEventLogs?.Refresh();
    EventtGroups?.Refresh();
    WebsiteUsers?.Refresh();
  }
  [ObservableProperty] EventtGroup? selEG; partial void OnSelEGChanged(EventtGroup? value)
  {
    UnselectAllCommand?.NotifyCanExecuteChanged();

    if (value is null) return;

    SelWU = null;
    MemberSinceKey = null;
    WebEventLogs?.Refresh();
    //EventtGroups?.Refresh();
    WebsiteUsers?.Refresh();

    //RunReLoadCommand?.NotifyCanExecuteChanged();
  }
  [ObservableProperty] WebsiteUser? selWU; partial void OnSelWUChanged(WebsiteUser? value)
  {
    UnselectAllCommand?.NotifyCanExecuteChanged();

    if (value is null) return;

    SelEG = null;
    MemberSinceKey = value.MemberSinceKey;
    WebEventLogs?.Refresh();
    EventtGroups?.Refresh();
    //WebsiteUsers?.Refresh();

    //RunReLoadCommand?.NotifyCanExecuteChanged();
  }

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
    IsBusy = true;
    Report = "Loading...";
    Console.Beep(260, 500);
    await Task.Delay(26);

    try
    {
      var (logRaw0, eLogs, users) = await _logParser.DoCRUD(crud);

      LogRaw = logRaw0; //tbxAllLog.ScrollToEnd(); // scroll to the end of text

      WebEventLogs = CollectionViewSource.GetDefaultView(eLogs.OrderByDescending(r => r.DoneAt).ToList());
      WebEventLogs.SortDescriptions.Add(new SortDescription(nameof(WebEventLog.DoneAt), ListSortDirection.Descending));
      WebEventLogs.Filter = obj => obj is not WebEventLog w || w is null ||
                                   ((string.IsNullOrEmpty(MemberSinceKey) || w.FirstVisitId?.Equals(MemberSinceKey, sc) == true) &&
                                    (string.IsNullOrEmpty(SelEG?.Hardware) || w.Sub[0]?.Equals(SelEG.Hardware, sc) == true) &&
                                    (string.IsNullOrEmpty(SelEG?.MozillaV) || w.Sub[1]?.Equals(SelEG.MozillaV, sc) == true) &&
                                    (string.IsNullOrEmpty(SelEG?.Versions) || w.Sub[2]?.Equals(SelEG.Versions, sc) == true) &&
                                    (string.IsNullOrEmpty(SelEG?.CpuCores) || w.Sub[3]?.Equals(SelEG.CpuCores, sc) == true) &&
                                    (string.IsNullOrEmpty(SelEG?.Platform) || w.Sub[4]?.Equals(SelEG.Platform, sc) == true) &&
                                    (string.IsNullOrEmpty(SelEG?.Language) || w.Sub[5]?.Equals(SelEG.Language, sc) == true) &&
                                    (string.IsNullOrEmpty(SelEG?.Resolute) || w.Sub[6]?.Equals(SelEG.Resolute, sc) == true));

      WebsiteUsers = CollectionViewSource.GetDefaultView(users.OrderByDescending(r => r.LastVisitAt).ToList());
      WebsiteUsers.SortDescriptions.Add(new SortDescription("LastVisitAt", ListSortDirection.Descending));
      WebsiteUsers.Filter = obj => obj is not WebsiteUser w || w is null || string.IsNullOrEmpty(SelWE?.NickUser) || w.Nickname?.Equals(SelWE?.NickUser, sc) == true;

      EventtGroups = CollectionViewSource.GetDefaultView(eLogs.GroupBy(log => new
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
      EventtGroups.Filter = obj => obj is not EventtGroup w || w is null || string.IsNullOrEmpty(SelWE?.NickWare) || w.NickWare?.Equals(SelWE?.NickWare, sc) == true;

      var isNew = MiscServices.NotifyIfThereAreNewLogEntriesAndStoreLastNewLogTime(eLogs.Max(r => r.DoneAt), @"C:\temp\potentiallyNewUsageTime.txt");
      Report = isNew ? "New visits detected!" : "-- Nothing new --"; //tbkReport.Foreground = isNew ? Brushes.GreenYellow : Brushes.Gray;
      if (/*isNew &&*/ sayIt)
        _ = synth.SpeakAsync(Report);

      return isNew;
    }
    catch (Exception ex) { /*_ = MessageBox.Show(ex.Message);*/ Report = ex.Message; WriteLine($"\n{DateTime.Now:yyyy-MM-dd HH:mm}  ERR  {ex}"); }
    finally { IsBusy = false; }

    return true;
  }
  public async Task TrySave()
  {
    _ = await _logParser.UpdateIfNewUser(WebsiteUsers);
    _ = await _logParser.UpdateIfNewWare(EventtGroups);
    _ = synth.SpeakAsync("Saved");
    await Task.Delay(260);
  }

  internal void OnNickUserChanged(string v) => throw new NotImplementedException();
}
