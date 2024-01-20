namespace AzureLogParser;
public partial class LogParserVM : ObservableValidator
{
  public LogParserVM() => logParser = new LogParser(new ConfigurationBuilder().AddUserSecrets<App>().Build()["SecretKey"] ?? "no key");
  public async Task ReLoad()
  {
    IsBusy = true;
    LogRaw = Report = "Loading...";
    Console.Beep(360, 100);

    try
    {
      var (logRaw0, eLogs, users) = await logParser.DoCRUD('r'); //tu: adhoc usersecrets});

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
      })
        .OrderByDescending(r => r.LastVisitAt)
        .ThenByDescending(r => r.Versions)
        .ThenByDescending(r => r.MozillaV)
        .ThenByDescending(r => r.Hardware)
        .ThenByDescending(r => r.Resolute)
        .ThenBy(r => r.Language)
        .ToList());

      foreach (EventtGroup item in EventtGroups)
      {
        item.NickWare = logParser.NickMapperWare(item.PseudoKey);
      }

      var isNew = MiscServices.NotifyIfThereAreNewLogEntriesAndStoreLastNewLogTime(eLogs.Max(r => r.DoneAt), @"C:\temp\potentiallyNewUsageTime.txt");
      Report = isNew ? "New usage detected!" : "-- Nothing new --"; //tbkReport.Foreground = isNew ? Brushes.GreenYellow : Brushes.Gray;
      _ = synth.SpeakAsync(Report);
    }
    catch (Exception ex) { _ = MessageBox.Show(ex.Message); Report = ex.Message; }
    finally { IsBusy = false; }
  }
}

public partial class LogParserVM : ObservableValidator
{
  const StringComparison sc = StringComparison.OrdinalIgnoreCase;
  readonly LogParser logParser;
  readonly SpeechSynthesizer synth = new();

  [ObservableProperty] bool isBusy;
  [ObservableProperty] string? logRaw;
  [ObservableProperty] string? report;
  [ObservableProperty] string? memberSinceKey; //partial void OnMemIdChanged(string? value) => WebEventLogs?.Refresh();

  [ObservableProperty] ICollectionView? webEventLogs;
  [ObservableProperty] ICollectionView? eventtGroups;
  [ObservableProperty] ICollectionView? websiteUsers;
  [ObservableProperty] EventtGroup? selEG; partial void OnSelEGChanged(EventtGroup? value)
  {
    if (value is null) return;

    MemberSinceKey = null;
    WebEventLogs?.Refresh();

    RunStep1Command?.NotifyCanExecuteChanged();
  }
  [ObservableProperty] WebsiteUser? selWU; partial void OnSelWUChanged(WebsiteUser? value)
  {
    if (value is null) return;
    SelEG = null;
    MemberSinceKey = value.MemberSinceKey;
    WebEventLogs?.Refresh();

    RunStep1Command?.NotifyCanExecuteChanged();
  }

  [RelayCommand(CanExecute = nameof(CanRunStep1))] public async Task RunStep1() => await RunStep1_(); bool CanRunStep1() => !IsBusy; async Task RunStep1_() => await ReLoad();
  [RelayCommand(CanExecute = nameof(CanDeleteLog))] public async Task DeleteLog() => await DeleteLog_(); bool CanDeleteLog() => !IsBusy; async Task DeleteLog_()
  {
    IsBusy = true;
    Report = "Deleting the log file on remote Azure location...";
    _ = MiscServices.SaveBlob(LogRaw ?? "Nothing here", $@"C:\Users\alexp\source\repos\alex-pi\AzureLogParser\Data\AzureTttLog.{DateTime.Now:yyMMdd-HHmmss}.txt");

    _ = synth.SpeakAsync("To do");
    var (_, _, _) = await logParser.DoCRUD('d');
    await ReLoad();
  }
  [RelayCommand(CanExecute = nameof(CanRunStep2))] public async Task RunStep2() => await RunStep2_(SelWU); bool CanRunStep2() => SelWU is not null; Task RunStep2_(WebsiteUser? selWU) => throw new NotImplementedException();

  public async Task TrySave()
  {
    await logParser.UpdateIfNewUser(WebsiteUsers);
    await logParser.UpdateIfNewWare(EventtGroups);
  }
}
