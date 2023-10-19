using System.Windows;
using Microsoft.Extensions.Configuration;

namespace AzureLogParser;
public partial class MainWindow : Window
{
  readonly string key;

  public MainWindow()
  {
    InitializeComponent();
    key = new ConfigurationBuilder().AddUserSecrets<App>().Build()["SecretKey"] ?? "no key"; //tu: adhoc usersecrets
  }

  async Task ReadInLog(string? firstVisitId = null)
  {
    System.Media.SystemSounds.Hand.Play();
    //tbx1.Text = "Loading...";
    dbg1.ItemsSource =
    //dbg2.ItemsSource = 
    null;

    var (logRaw, usage2, elogs, users) = await new LogParser().DoCRUD('r', key);

    //tbx1.Text = logRaw;

    dbg1.ItemsSource = elogs.Where(r => firstVisitId == null || r.FirstVisitId == firstVisitId).OrderByDescending(r => r.DoneAt);
    if (firstVisitId == null)
      dbg2.ItemsSource = users.OrderByDescending(r => r.LastVisitAt);

    System.Media.SystemSounds.Beep.Play();
  }

  async void OnLoaded(object sender, RoutedEventArgs e) => await ReadInLog();

  void OnExit(object sender, RoutedEventArgs e) => Close();

  async void OnCreate(object sender, RoutedEventArgs e) { tbx1.Text = "Creating  the log file..."; var (_, _, _, _) = await new LogParser().DoCRUD('c', key); await ReadInLog(); }
  async void OnReadIn(object sender, RoutedEventArgs e) { tbx1.Text = "Reading   the log file..."; await ReadInLog(); }
  async void OnUpdate(object sender, RoutedEventArgs e) { tbx1.Text = "Updating  the log file..."; var (_, _, _, _) = await new LogParser().DoCRUD('u', key); await ReadInLog(); }
  async void OnDelete(object sender, RoutedEventArgs e) { tbx1.Text = "Deleting  the log file..."; var (_, _, _, _) = await new LogParser().DoCRUD('d', key); await ReadInLog(); }
  async void OnAppend(object sender, RoutedEventArgs e) { tbx1.Text = "Appending the log file..."; var (_, _, _, _) = await new LogParser().DoCRUD('a', key); await ReadInLog(); }

  async void OnUserChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
  {
    if (e.AddedItems.Count > 0)
      await ReadInLog(((Db.OneBase.Model.WebsiteUser?)e?.AddedItems[0])?.EventData);
  }
}