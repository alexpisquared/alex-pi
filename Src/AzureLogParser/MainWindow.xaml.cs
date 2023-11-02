using System.ComponentModel;
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
    tbx1.Text = "Loading...";
    dbg1.ItemsSource = null;

    var (logRaw, elogs, users) = await lp.DoCRUD('r', key);

    tbx1.Text = logRaw;
    tbx1.ScrollToEnd(); // scroll to the end of text

    dbg1.ItemsSource = elogs.Where(r => firstVisitId == null || r.FirstVisitId == firstVisitId).OrderByDescending(r => r.DoneAt);
    if (firstVisitId == null)
    {
      var vs = CollectionViewSource.GetDefaultView(users);
      vs.SortDescriptions.Add(new SortDescription("LastVisitAt", ListSortDirection.Descending));
      dbg2.ItemsSource = vs;
    }

    System.Media.SystemSounds.Beep.Play();
  }

  async void OnLoaded(object sender, RoutedEventArgs e) => await ReadInLog();
  void OnExit(object sender, RoutedEventArgs e) => Close();
  async void OnCreate(object sender, RoutedEventArgs e) { tbx1.Text = "Creating  the log file..."; var (_, _, _) = await new LogParser().DoCRUD('c', key); await ReadInLog(); }
  async void OnReadIn(object sender, RoutedEventArgs e) { tbx1.Text = "Reading   the log file..."; /*                                                   */ await ReadInLog(); }
  async void OnUpdate(object sender, RoutedEventArgs e) { tbx1.Text = "Updating  the log file..."; var (_, _, _) = await new LogParser().DoCRUD('u', key); await ReadInLog(); }
  async void OnDelete(object sender, RoutedEventArgs e) { tbx1.Text = "Deleting  the log file..."; var (_, _, _) = await new LogParser().DoCRUD('d', key); await ReadInLog(); }
  async void OnAppend(object sender, RoutedEventArgs e) { tbx1.Text = "Appending the log file..."; var (_, _, _) = await new LogParser().DoCRUD('a', key); await ReadInLog(); }
  async void OnUserChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
  {
    if (e.AddedItems.Count > 0)
      await ReadInLog(((Db.OneBase.Model.WebsiteUser?)e?.AddedItems[0])?.MemberSinceKey);
  }

  private void OnEdit(object sender, System.Windows.Controls.DataGridCellEditEndingEventArgs e)
  {
    var user = (Db.OneBase.Model.WebsiteUser?)e.Row.Item;
    lp.UpdateIfDifferent(user.MemberSinceKey, ((TextBox)e.EditingElement).Text);
  }
}