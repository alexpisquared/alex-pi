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
  [ObservableProperty] string? memberSinceKey; //partial void OnMemIdChanged(string? value) => WebEventLogs?.Refresh();

  [ObservableProperty] ICollectionView? webEventLogs;
  [ObservableProperty] ICollectionView? eventtGroups;
  [ObservableProperty] ICollectionView? websiteUsers;
  [ObservableProperty] WebEventLog? selEL; partial void OnSelELChanged(WebEventLog? value)
  {
    if (value is null) return;

    MemberSinceKey = null;
    WebsiteUsers?.Refresh();
    EventtGroups?.Refresh();
  }
  [ObservableProperty] EventtGroup? selEG; partial void OnSelEGChanged(EventtGroup? value)
  {
    if (value is null) return;

    MemberSinceKey = null;
    WebEventLogs?.Refresh();

    RunReLoadCommand?.NotifyCanExecuteChanged();
  }
  [ObservableProperty] WebsiteUser? selWU; partial void OnSelWUChanged(WebsiteUser? value)
  {
    if (value is null) return;

    SelEG = null;
    MemberSinceKey = value.MemberSinceKey;
    WebEventLogs?.Refresh();

    RunReLoadCommand?.NotifyCanExecuteChanged();
  }

  [RelayCommand(CanExecute = nameof(CanRunReLoad))] public async Task RunReLoad() => await RunReLoad_(); /**/ bool CanRunReLoad() => !IsBusy; async Task RunReLoad_() => await ReLoadLists_CheckIfNews();
  [RelayCommand(CanExecute = nameof(CanCreateLog))] public async Task CreateLog() => await DoCrud_('c'); /**/ bool CanCreateLog() => !IsBusy;
  [RelayCommand(CanExecute = nameof(CanUpdateLog))] public async Task UpdateLog() => await DoCrud_('u'); /**/ bool CanUpdateLog() => !IsBusy;
  [RelayCommand(CanExecute = nameof(CanDeleteLog))] public async Task DeleteLog() => await DoCrud_('d'); /**/ bool CanDeleteLog() => !IsBusy;
  [RelayCommand(CanExecute = nameof(CanAppendLog))] public async Task AppendLog() => await DoCrud_('a'); /**/ bool CanAppendLog() => !IsBusy;

  async Task DoCrud_(char crud)
  {
    IsBusy = true;
    Report = "...ing the log file on remote Azure location...";
    //if (crud == 'd')      _ = MiscServices.SaveBlob(LogRaw ?? "Nothing here", $@"C:\Users\alexp\source\repos\alex-pi\AzureLogParser\Data\AzureTttLog.{DateTime.Now:yyMMdd-HHmmss}.txt");

    var (_, _, _) = await _logParser.DoCRUD(crud);
    await ReLoadLists_CheckIfNews();
  }
  public async Task<bool> ReLoadLists_CheckIfNews(bool sayIt = false)
  {
    IsBusy = true;
    LogRaw = Report = "Loading...";
    Console.Beep(360, 100);

    try
    {
      var (logRaw0, eLogs, users) = await _logParser.DoCRUD('r');

      LogRaw = logRaw0; //tbxAllLog.ScrollToEnd(); // scroll to the end of text

      WebEventLogs = CollectionViewSource.GetDefaultView(eLogs.OrderByDescending(r => r.DoneAt));
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
      WebsiteUsers.Filter = obj => obj is not WebsiteUser w || w is null || string.IsNullOrEmpty(SelEL?.NickUser) || w.Nickname?.Equals(SelEL?.NickUser, sc) == true;

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
        Resolute = r.Key.Resolution
      }).ToList());
      EventtGroups.SortDescriptions.Add(new SortDescription("LastVisitAt", ListSortDirection.Descending));
      EventtGroups.Filter = obj => obj is not EventtGroup w || w is null || string.IsNullOrEmpty(SelEL?.NickWare) || w.NickWare?.Equals(SelEL?.NickWare, sc) == true;

      foreach (EventtGroup item in EventtGroups) item.NickWare = _logParser.NickMapperWare(item.PseudoKey);

      var isNew = MiscServices.NotifyIfThereAreNewLogEntriesAndStoreLastNewLogTime(eLogs.Max(r => r.DoneAt), @"C:\temp\potentiallyNewUsageTime.txt");
      Report = isNew ? "New usage detected!" : "-- Nothing new --"; //tbkReport.Foreground = isNew ? Brushes.GreenYellow : Brushes.Gray;
      if (/*isNew &&*/ sayIt)
        _ = synth.SpeakAsync(Report);

      return isNew;
    }
    catch (Exception ex) { _ = MessageBox.Show(ex.Message); Report = ex.Message; }
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
}
