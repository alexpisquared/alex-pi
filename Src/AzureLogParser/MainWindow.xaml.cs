using System.ComponentModel;
using System.IO;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
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
  async void OnUserChanged(object sender, SelectionChangedEventArgs e)
  {
    if (e.AddedItems.Count > 0)
      await ReadInLog(((Db.OneBase.Model.WebsiteUser?)e?.AddedItems[0])?.MemberSinceKey);
  }
  void OnEdit(object sender, DataGridCellEditEndingEventArgs e)
  {
    var user = (Db.OneBase.Model.WebsiteUser?)e.Row.Item;
    if (user is not null)
      logParser.UpdateIfDifferent(user.MemberSinceKey, ((TextBox)e.EditingElement).Text, e.Column.DisplayIndex);
  }
  void OnExit(object sender, RoutedEventArgs e) => Close();

  async Task ReadInLog(string? firstVisitId = null)
  {
    tbxAllLog.Text = 
    tbkReport.Text = "Loading...";
    Console.Beep(360, 100);
    dbEvent.ItemsSource = null;

    var (logRaw, eLogs, users) = await logParser.DoCRUD('r', key);

    tbxAllLog.Text = logRaw;
    tbxAllLog.ScrollToEnd(); // scroll to the end of text

    dbEvent.ItemsSource = eLogs.Where(r => firstVisitId == null || r.FirstVisitId == firstVisitId).OrderByDescending(r => r.DoneAt);
    if (firstVisitId == null)
    {
      var vs = CollectionViewSource.GetDefaultView(users);
      vs.SortDescriptions.Add(new SortDescription("LastVisitAt", ListSortDirection.Descending));
      dbUsers.ItemsSource = null;
      dbUsers.ItemsSource = vs;
    }

    var isNew = NotifyIfThereAreNewLogEntriesAndStoreLastNewLogTime(eLogs.Max(r => r.DoneAt), @"C:\temp\potentiallyNewUsageTime.txt");
    tbkReport.Text = isNew ? "New usage detected!" : "-- Nothing new --";
    tbkReport.Foreground = isNew ? Brushes.GreenYellow : Brushes.Gray;

    if (isNew)
      _ = synth.SpeakAsync(tbkReport.Text);
    else
      Console.Beep(333, 200);
  }
  bool NotifyIfThereAreNewLogEntriesAndStoreLastNewLogTime(DateTime potentiallyNewUsageTime, string filePath)
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
      }
      else
      {
        File.WriteAllText(filePath, potentiallyNewUsageTime.ToString("o"));
        return true;
      }
    }
    catch (Exception ex)
    {
      _ = MessageBox.Show(ex.Message);
    }

    return false;
  }
}