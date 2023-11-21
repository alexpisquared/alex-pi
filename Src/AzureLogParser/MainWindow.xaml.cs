using System.ComponentModel;
using System.IO;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Db.OneBase.Model;
using Microsoft.Extensions.Configuration;
namespace AzureLogParser;
public partial class MainWindow : Window
{
  readonly string key;
  readonly LogParser logParser = new();
  readonly SpeechSynthesizer synth = new();

  public MainWindow()
  {
    InitializeComponent();
    key = new ConfigurationBuilder().AddUserSecrets<App>().Build()["SecretKey"] ?? "no key"; //tu: adhoc usersecrets

    KeyUp += new KeyEventHandler((s, e) => { if (e.Key == Key.Escape) { Close(); } }); //tu:
    MouseLeftButtonDown += new MouseButtonEventHandler((s, e) => { DragMove(); }); //tu:
  }

  async void OnLoaded(object sender, RoutedEventArgs e) => await ReadInLog();
  async void OnCreate(object sender, RoutedEventArgs e) { tbkReport.Text = "Creating  the log file..."; var (_, _, _) = await logParser.DoCRUD('c', key); await ReadInLog(); }
  async void OnReadIn(object sender, RoutedEventArgs e) { tbkReport.Text = "Reading   the log file..."; /*                                             */ await ReadInLog(); }
  async void OnUpdate(object sender, RoutedEventArgs e) { tbkReport.Text = "Updating  the log file..."; var (_, _, _) = await logParser.DoCRUD('u', key); await ReadInLog(); }
  async void OnDelete(object sender, RoutedEventArgs e) { tbkReport.Text = "Deleting  the log file..."; var (_, _, _) = await logParser.DoCRUD('d', key); await ReadInLog(); }
  async void OnAppend(object sender, RoutedEventArgs e) { tbkReport.Text = "Appending the log file..."; var (_, _, _) = await logParser.DoCRUD('a', key); await ReadInLog(); }
  void OnEdit(object sender, DataGridCellEditEndingEventArgs e)
  {
    var user = (Db.OneBase.Model.WebsiteUser?)e.Row.Item;
    if (user is not null)
      logParser.UpdateIfDifferent(user.MemberSinceKey, ((TextBox)e.EditingElement).Text, e.Column.DisplayIndex);
  }
  void OnExit(object sender, RoutedEventArgs e) => Close();

  void OnUserChanged(object sender, SelectionChangedEventArgs e)
  {
    if (e.AddedItems.Count <= 0) return;

    MemId = (((WebsiteUser?)e?.AddedItems[0])?.MemberSinceKey);
    Har =
    Moz =
    Ver =
    CPU =
    Pla =
    Lan =
    Res = null;
    _events?.Refresh();
  }
  void OnGroupChanged(object sender, SelectionChangedEventArgs e)
  {
    if (e.AddedItems.Count <= 0) return;

    dynamic? selectGroup = e?.AddedItems[0];
    if (selectGroup is null) return;

    MemId = null;
    Har = selectGroup.Hardware;
    Moz = selectGroup.MozillaVer;
    Ver = selectGroup.Versions;
    CPU = selectGroup.CPU;
    Pla = selectGroup.Platform;
    Lan = selectGroup.Languages;
    Res = selectGroup.Resolution;
    _events?.Refresh();
  }

  async Task ReadInLog()
  {
    tbxAllLog.Text =
    tbkReport.Text = "Loading...";
    Console.Beep(360, 100);
    dbEvent.ItemsSource = null;

    var (logRaw, eLogs, users) = await logParser.DoCRUD('r', key);

    tbxAllLog.Text = logRaw;
    tbxAllLog.ScrollToEnd(); // scroll to the end of text

    _events = CollectionViewSource.GetDefaultView(eLogs.OrderByDescending(r => r.DoneAt));
    _events.SortDescriptions.Add(new SortDescription(nameof(WebEventLog.DoneAt), ListSortDirection.Descending));
    _events.Filter = obj => obj is not WebEventLog w || w is null ||
        ((string.IsNullOrEmpty(MemId) || w.FirstVisitId?.Contains(MemId, StringComparison.OrdinalIgnoreCase) == true) &&
        (string.IsNullOrEmpty(Har) || w.Sub[0]?.Contains(Har, StringComparison.OrdinalIgnoreCase) == true) &&
        (string.IsNullOrEmpty(Moz) || w.Sub[1]?.Contains(Moz, StringComparison.OrdinalIgnoreCase) == true) &&
        (string.IsNullOrEmpty(Ver) || w.Sub[2]?.Contains(Ver, StringComparison.OrdinalIgnoreCase) == true) &&
        (string.IsNullOrEmpty(CPU) || w.Sub[3]?.Contains(CPU, StringComparison.OrdinalIgnoreCase) == true) &&
        (string.IsNullOrEmpty(Pla) || w.Sub[4]?.Contains(Pla, StringComparison.OrdinalIgnoreCase) == true) &&
        (string.IsNullOrEmpty(Lan) || w.Sub[5]?.Contains(Lan, StringComparison.OrdinalIgnoreCase) == true) &&
        (string.IsNullOrEmpty(Res) || w.Sub[6]?.Contains(Res, StringComparison.OrdinalIgnoreCase) == true));
    dbEvent.ItemsSource = _events;

    //if (MemId == null) // only update the user list if the user is not selected
    //{
    //  //nogo: prevents from editing: var vs = CollectionViewSource.GetDefaultView(users.OrderByDescending(r => r.LastVisitAt));      //vs.SortDescriptions.Add(new SortDescription("LastVisitAt", ListSortDirection.Descending));
    //  dbUsers.ItemsSource = null; // 
      dbUsers.ItemsSource = users.OrderByDescending(r => r.LastVisitAt).ToList();
    //}

    var gr = eLogs.GroupBy(log => new
    {
      Hardware = log.Sub[0],
      MozillaVer = log.Sub[01],
      Versions = log.Sub[02],
      CPU = log.Sub[03].TrimStart('0'),
      Platform = log.Sub[04],
      Languages = log.Sub[05],
      Resolution = log.Sub[06],
    }).Select(r => new
    {
      Count = r.Count(),
      r.Key.Hardware,
      r.Key.MozillaVer,
      r.Key.Versions,
      r.Key.CPU,
      r.Key.Platform,
      r.Key.Languages,
      r.Key.Resolution
    })
      .OrderByDescending(r => r.Versions)
      .ThenByDescending(r => r.MozillaVer)
      .ThenByDescending(r => r.Hardware)
      .ThenBy(r => r.Languages)
      .ThenByDescending(r => r.Resolution)
      .ToList();

    dbGroup.ItemsSource = gr;

    var isNew = NotifyIfThereAreNewLogEntriesAndStoreLastNewLogTime(eLogs.Max(r => r.DoneAt), @"C:\temp\potentiallyNewUsageTime.txt");
    tbkReport.Text = isNew ? "New usage detected!" : "-- Nothing new --";
    tbkReport.Foreground = isNew ? Brushes.GreenYellow : Brushes.Gray;

    if (isNew)
      _ = synth.SpeakAsync(tbkReport.Text);
    else
      Console.Beep(333, 200);
  }
  ICollectionView? _events;
  string? Har, Moz, Ver, CPU, Pla, Lan, Res, MemId; bool NotifyIfThereAreNewLogEntriesAndStoreLastNewLogTime(DateTime potentiallyNewUsageTime, string filePath)
  {
    try
    {
      var lastKnownTimeOfUsage = File.Exists(filePath) ? File.ReadAllText(filePath) : null;
      if (lastKnownTimeOfUsage != null && DateTime.TryParse(lastKnownTimeOfUsage, out var lastKnownTime))
      {
        if (potentiallyNewUsageTime > lastKnownTime)
        {
          File.WriteAllText(filePath, potentiallyNewUsageTime.ToString("o"));
          return true;
        }
      } else
      {
        File.WriteAllText(filePath, potentiallyNewUsageTime.ToString("o"));
        return true;
      }
    } catch (Exception ex)
    {
      _ = MessageBox.Show(ex.Message);
    }

    return false;
  }
}