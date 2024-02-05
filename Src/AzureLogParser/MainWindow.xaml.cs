
namespace AzureLogParser;
public partial class MainWindow : Window
{
  readonly LogParserVM _vm = new();
  readonly LogParser _logParser;

  public MainWindow()
  {
    InitializeComponent();
    _logParser = new(new ConfigurationBuilder().AddUserSecrets<App>().Build()["SecretKey"] ?? "no key");
    KeyUp += new KeyEventHandler((s, e) => { if (e.Key == Key.Escape) { Close(); } }); //tu:
    MouseLeftButtonDown += new MouseButtonEventHandler((s, e) => { DragMove(); }); //tu:

    DataContext = _vm;
  }

  async void OnLoaded(object sender, RoutedEventArgs e) => await _vm.ReLoad();
  async void OnCreate(object sender, RoutedEventArgs e) { tbkReport.Text = "Creating  the log file..."; var (_, _, _) = await _logParser.DoCRUD('c'); await _vm.ReLoad(); }
  async void OnUpdate(object sender, RoutedEventArgs e) { tbkReport.Text = "Updating  the log file..."; var (_, _, _) = await _logParser.DoCRUD('u'); await _vm.ReLoad(); }
  async void OnDelete(object sender, RoutedEventArgs e) { tbkReport.Text = "Deleting  the log file..."; var (_, _, _) = await _logParser.DoCRUD('d'); await _vm.ReLoad(); }
  async void OnAppend(object sender, RoutedEventArgs e) { tbkReport.Text = "Appending the log file..."; var (_, _, _) = await _logParser.DoCRUD('a'); await _vm.ReLoad(); }
  async void OnExit(object sender, RoutedEventArgs e) { await _vm.TrySave(); Close(); }
  async void OnRefresh(object sender, RoutedEventArgs e) { await _vm.TrySave(); await _vm.ReLoad(); }
}