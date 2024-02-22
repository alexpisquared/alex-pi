using System.Drawing;

namespace AzureLogParser;
public partial class MainWindow : Window
{
  readonly LogParserVM _vm = new();
  readonly LogParser _logParser;

  public MainWindow()
  {
    InitializeComponent();

    UpdateMaxSize(); // Call UpdateMaxSize initially to set the correct initial size
    LocationChanged += (sender, e) => UpdateMaxSize();    // Handle the LocationChanged event to update the size when the window is moved

    _logParser = new(new ConfigurationBuilder().AddUserSecrets<App>().Build()["SecretKey"] ?? "no key");
    KeyUp += new System.Windows.Input.KeyEventHandler((s, e) => { if (e.Key == Key.Escape) { Close(); } }); //tu:
    MouseLeftButtonDown += new MouseButtonEventHandler((s, e) => { DragMove(); }); //tu:

    DataContext = _vm;
  }

  void UpdateMaxSize()
  {
    var currentScreen = System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(this).Handle); // Get the screen where the window is currently located

    if (currentScreen.Primary) // If the current screen is the primary screen, subtract the taskbar size
    {
      MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
      MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
    }
    else
    {
      MaxHeight = currentScreen.Bounds.Height;
      MaxWidth = currentScreen.Bounds.Width;
    }
  }

  async void OnLoaded(object sender, RoutedEventArgs e) => await _vm.ReLoad();
  async void OnCreate(object sender, RoutedEventArgs e) { tbkReport.Content = "Creating  the log file..."; var (_, _, _) = await _logParser.DoCRUD('c'); await _vm.ReLoad(); }
  async void OnUpdate(object sender, RoutedEventArgs e) { tbkReport.Content = "Updating  the log file..."; var (_, _, _) = await _logParser.DoCRUD('u'); await _vm.ReLoad(); }
  async void OnDelete(object sender, RoutedEventArgs e) { tbkReport.Content = "Deleting  the log file..."; var (_, _, _) = await _logParser.DoCRUD('d'); await _vm.ReLoad(); }
  async void OnAppend(object sender, RoutedEventArgs e) { tbkReport.Content = "Appending the log file..."; var (_, _, _) = await _logParser.DoCRUD('a'); await _vm.ReLoad(); }
  async void OnExit(object sender, RoutedEventArgs e) { await _vm.TrySave(); Close(); }
  async void OnRefresh(object sender, RoutedEventArgs e) { await _vm.TrySave(); await _vm.ReLoad(); }
  void OnDblClck(object sender, MouseButtonEventArgs e) => GetEmailAddressFromLog();
  void OnGetEmail(object sender, RoutedEventArgs e) => GetEmailAddressFromLog();

  void GetEmailAddressFromLog()
  {
    var slt = (dg1.SelectedItem as WebEventLog)?.EventName?.Split(':');
    if (slt is null || slt.Length <= 1)
    {
      EmailAddress1.Header = "Nothing selected in the top DataGrid";
    }
    else
    {
      var timestamp = slt[1];
      var matchingLines = File.ReadAllLines("""C:\Users\alexp\OneDrive\Public\Logs\MinNavTpl.RAZ.ale.Infi..log""").Where(x => x.Contains(timestamp)).ToList();
      if (matchingLines.Count <= 0)
      {
        tbkReport.Foreground = System.Windows.Media.Brushes.Orange;
        tbkReport.Content =
        EmailAddress1.Header = "No matches in the log";
      }
      else
      {
        var eml = matchingLines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Last();
        tbkReport.Foreground = System.Windows.Media.Brushes.LimeGreen;
        tbkReport.Content =
        EmailAddress1.Header = eml;
        //new Window { Title = "Details", Height = 260, Width = 1400, Content = new TextBox { Text = $"{eml}\n\n{string.Join(Environment.NewLine, matchingLines)}\n\n", FontSize = 20, Foreground = System.Windows.Media.Brushes.Blue, TextWrapping = TextWrapping.Wrap, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, HorizontalScrollBarVisibility = ScrollBarVisibility.Auto }}.ShowDialog();
      }
    }
  }

  void OnCopyClip(object sender, RoutedEventArgs e) => Clipboard.SetText(EmailAddress1.Header?.ToString());
}