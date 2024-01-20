namespace AzureLogParser;
public partial class MainWindow : Window
{
  readonly LogParserVM vm = new();
  readonly LogParser logParser;

  public MainWindow()
  {
    InitializeComponent();
    logParser = new(new ConfigurationBuilder().AddUserSecrets<App>().Build()["SecretKey"] ?? "no key");
    KeyUp += new KeyEventHandler((s, e) => { if (e.Key == Key.Escape) { Close(); } }); //tu:
    MouseLeftButtonDown += new MouseButtonEventHandler((s, e) => { DragMove(); }); //tu:

    DataContext = vm;
  }

  async void OnLoaded(object sender, RoutedEventArgs e) => await vm.ReLoad();
  async void OnCreate(object sender, RoutedEventArgs e) { tbkReport.Text = "Creating  the log file..."; var (_, _, _) = await logParser.DoCRUD('c'); await vm.ReLoad(); }
  async void OnUpdate(object sender, RoutedEventArgs e) { tbkReport.Text = "Updating  the log file..."; var (_, _, _) = await logParser.DoCRUD('u'); await vm.ReLoad(); }
  async void OnDelete(object sender, RoutedEventArgs e) { tbkReport.Text = "Deleting  the log file..."; var (_, _, _) = await logParser.DoCRUD('d'); await vm.ReLoad(); }
  async void OnAppend(object sender, RoutedEventArgs e) { tbkReport.Text = "Appending the log file..."; var (_, _, _) = await logParser.DoCRUD('a'); await vm.ReLoad(); }
  async void OnExit(object sender, RoutedEventArgs e) { await vm.TrySave(); Close(); }
}