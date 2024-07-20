namespace AzureLogParser;
public partial class MainWindow : Window
{
  readonly LogParserVM _vm;
  readonly SpeechSynthesizer _synth = new();

  public MainWindow(LogParserVM vm)
  {
    InitializeComponent();

    UpdateMaxSizeLestCoverTaskbar();                                    // Call UpdateMaxSize initially to set the correct initial size
    LocationChanged += (s, e) => UpdateMaxSizeLestCoverTaskbar();  // Handle the LocationChanged event to update the size when the window is moved

    KeyUp += new System.Windows.Input.KeyEventHandler(async (s, e) => { if (e.Key == Key.Escape) await SaveAndClose(); }); //tu:
    MouseLeftButtonDown += new MouseButtonEventHandler((s, e) => { if (e.ButtonState == MouseButtonState.Pressed) { DragMove(); } }); //tu:

    _vm = vm;
    DataContext = _vm;
  }

  async void OnExit(object s, RoutedEventArgs e) => await SaveAndClose();
  void OnCopyClip(object s, RoutedEventArgs e) => System.Windows.Clipboard.SetText(((System.Windows.Controls.Button)s).Content?.ToString());
  async void OnDblClck(object s, MouseButtonEventArgs e) => await GetEmailAddressFromLog();
  async void OnGetEmail(object s, RoutedEventArgs e) => await GetEmailAddressFromLog();

  async Task SaveAndClose() { Hide(); await _vm.TrySave(); Close(); }
  void UpdateMaxSizeLestCoverTaskbar() //tu: //todo: move to a helper class in the AAV shared libs. Compare with CI's solution.
  {
    var currentScreen = System.Windows.Forms.Screen.FromHandle(new System.Windows.Interop.WindowInteropHelper(this).Handle); // Get the screen where the window is currently located
    if (currentScreen.Primary) //tu: If the current screen is the primary screen, subtract the taskbar size
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
  async Task GetEmailAddressFromLog()
  {
    var eventNameCsv = (dg1.SelectedItem as WebEventLog)?.EventName?.Split(':');
    if (eventNameCsv is null || eventNameCsv.Length <= 1)
    {
      tbkReport.Content = "Nothing selected in the top DataGrid";
    }
    else
    {
      try
      {
        var timestamp = eventNameCsv[1];
        var matchingLines = ReadInPotentiallyLockedLogFile(timestamp);
        if (matchingLines.Count <= 0)
        {
          tbkReport.Foreground = System.Windows.Media.Brushes.Orange;
          tbkReport.Content = $"No matches in the broadcast log for  {timestamp}.";
        }
        else
        {
          var eml = matchingLines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Last();
          tbkReport.Foreground = System.Windows.Media.Brushes.LimeGreen;
          tbkReport.Content = eml;
          System.Windows.Clipboard.SetText(eml);
          _ = _synth.SpeakAsync("Copied to Clipboard.");

          await _vm.Bingo(dg1.SelectedItem as WebEventLog, eml);

          //new Window { Title = "Details", Height = 260, Width = 1400, Content = new TextBox { Text = $"{eml}\n\n{string.Join(Environment.NewLine, matchingLines)}\n\n", FontSize = 20, Foreground = System.Windows.Media.Brushes.Blue, TextWrapping = TextWrapping.Wrap, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, HorizontalScrollBarVisibility = ScrollBarVisibility.Auto }}.ShowDialog();
        }
      }
      catch (Exception ex) { /*_ = MessageBox.Show(ex.Message);*/ tbkReport.Content = ex.Message; WriteLine($"\n{DateTime.Now:yyyy-MM-dd HH:mm}  ERR  \n  {ex}"); }
    }
  }
  static List<string> ReadLogFile(string timestamp) => File.ReadAllLines(_broadcastLogFile).Where(x => x.Contains(timestamp)).ToList();
  static List<string> ReadInPotentiallyLockedLogFile(string timestamp)
  {
    var lines = new List<string>();
    if (File.Exists(_broadcastLogFile))
    {
      using var fileStream = new FileStream(_broadcastLogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite); // Open the potentially locked file.
      using var streamReader = new StreamReader(fileStream);
      string? line;
      while ((line = streamReader.ReadLine()) != null)
      {
        if (line.Contains(timestamp))
        {
          lines.Add(line);
        }
      }
    }

    return lines;
  }
  const string _broadcastLogFile = @"C:\Users\alexp\OneDrive\Public\Logs\MinNavTpl.RAZ.ale.Infi..log";
}