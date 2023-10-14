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

  async Task ReadInLog()
  {
    tbx1.Text = tbx2.Text = "Loading...";

    var (logRaw, usage2) = await new AzureLogParser.LogParser().DoCRUD('r', key);

    tbx1.Text = logRaw;
    tbx2.Text = usage2;

    tbx1.SelectionStart = tbx1.Text.Length;
    tbx1.ScrollToEnd();
    System.Media.SystemSounds.Beep.Play();
  }

  async void OnLoaded(object sender, RoutedEventArgs e) => await ReadInLog();

  void OnExit(object sender, RoutedEventArgs e) => Close();

  async void OnCreate(object sender, RoutedEventArgs e) { tbx1.Text = tbx2.Text = "Creating  the log file..."; var (_, _) = await new LogParser().DoCRUD('c', key); await ReadInLog(); }
  async void OnReadIn(object sender, RoutedEventArgs e) { await ReadInLog(); }
  async void OnUpdate(object sender, RoutedEventArgs e) { tbx1.Text = tbx2.Text = "Updateing the log file..."; var (_, _) = await new LogParser().DoCRUD('u', key); await ReadInLog(); }
  async void OnDelete(object sender, RoutedEventArgs e) { tbx1.Text = tbx2.Text = "Deleteing the log file..."; var (_, _) = await new LogParser().DoCRUD('d', key); await ReadInLog(); }
  async void OnAppend(object sender, RoutedEventArgs e) { tbx1.Text = tbx2.Text = "Appending the log file..."; var (_, _) = await new LogParser().DoCRUD('a', key); await ReadInLog(); }
}