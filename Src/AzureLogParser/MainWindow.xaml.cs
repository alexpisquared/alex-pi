using System.ComponentModel;
using System.Windows.Media;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Extensions.Configuration;
namespace AzureLogParser;
public partial class MainWindow : Window
{
  readonly string key;
  readonly LogParser lp = new();

  public MainWindow()
  {
    InitializeComponent();
    key = new ConfigurationBuilder().AddUserSecrets<App>().Build()["SecretKey"] ?? "no key"; //tu: adhoc usersecrets
  }

  async Task ReadInLog(string? firstVisitId = null)
  {
    System.Media.SystemSounds.Hand.Play();
    tbxAllLog.Text = "Loading...";
    dbg1.ItemsSource = null;

    var (logRaw, elogs, users) = await lp.DoCRUD('r', key);

    tbxAllLog.Text = logRaw;
    tbxAllLog.ScrollToEnd(); // scroll to the end of text

    dbg1.ItemsSource = elogs.Where(r => firstVisitId == null || r.FirstVisitId == firstVisitId).OrderByDescending(r => r.DoneAt);
    if (firstVisitId == null)
    {
      var vs = CollectionViewSource.GetDefaultView(users);
      vs.SortDescriptions.Add(new SortDescription("LastVisitAt", ListSortDirection.Descending));
      dbg2.ItemsSource = vs;
    }

    var isNew = NotifyIfThereAreNewLogEntriesAndStoreLastNewLogTime(elogs.Max(r => r.DoneAt), @"C:\temp\potentiallyNewUsageTime.txt");
    tbkReport.Text = isNew ? "++ New usage detected ++" : "-- Nothing new --";
    tbkReport.Foreground = isNew ? Brushes.GreenYellow : Brushes.Gray;

    System.Media.SystemSounds.Beep.Play();
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
      // Handle or log the exception
    }
    return false;
  }

  async void OnLoaded(object sender, RoutedEventArgs e) => await ReadInLog();
  void OnExit(object sender, RoutedEventArgs e) => Close();
  async void OnCreate(object sender, RoutedEventArgs e) { tbxAllLog.Text = "Creating  the log file..."; var (_, _, _) = await new LogParser().DoCRUD('c', key); await ReadInLog(); }
  async void OnReadIn(object sender, RoutedEventArgs e) { tbxAllLog.Text = "Reading   the log file..."; /*                                                   */ await ReadInLog(); }
  async void OnUpdate(object sender, RoutedEventArgs e) { tbxAllLog.Text = "Updating  the log file..."; var (_, _, _) = await new LogParser().DoCRUD('u', key); await ReadInLog(); }
  async void OnDelete(object sender, RoutedEventArgs e) { tbxAllLog.Text = "Deleting  the log file..."; var (_, _, _) = await new LogParser().DoCRUD('d', key); await ReadInLog(); }
  async void OnAppend(object sender, RoutedEventArgs e) { tbxAllLog.Text = "Appending the log file..."; var (_, _, _) = await new LogParser().DoCRUD('a', key); await ReadInLog(); }
  async void OnUserChanged(object sender, SelectionChangedEventArgs e)
  {
    if (e.AddedItems.Count > 0)
      await ReadInLog(((Db.OneBase.Model.WebsiteUser?)e?.AddedItems[0])?.MemberSinceKey);
  }

  void OnEdit(object sender, DataGridCellEditEndingEventArgs e)
  {
    var user = (Db.OneBase.Model.WebsiteUser?)e.Row.Item;
    if (user is not null)
      lp.UpdateIfDifferent(user.MemberSinceKey, ((TextBox)e.EditingElement).Text, e.Column.DisplayIndex);
  }
}