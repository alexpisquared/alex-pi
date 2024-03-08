using System.Windows.Controls;

namespace AzureLogParser;
public partial class MainWindow : Window
{
  readonly LogParserVM _vm;
  readonly SpeechSynthesizer synth = new();

  public MainWindow(LogParserVM vm)
  {
    InitializeComponent();

    UpdateMaxSize();                                    // Call UpdateMaxSize initially to set the correct initial size
    LocationChanged += (sender, e) => UpdateMaxSize();  // Handle the LocationChanged event to update the size when the window is moved

    KeyUp += new KeyEventHandler(async (s, e) => { if (e.Key == Key.Escape) await SaveAndClose(); }); //tu:
    MouseLeftButtonDown += new MouseButtonEventHandler((s, e) => { if (e.ButtonState == MouseButtonState.Pressed) { DragMove(); } }); //tu:

    _vm = vm;
    DataContext = _vm;
  }

  async void OnExit(object sender, RoutedEventArgs e) => await SaveAndClose();
  void OnCopyClip(object sender, RoutedEventArgs e) => Clipboard.SetText(((Button)sender).Content?.ToString());
  void OnDblClck(object sender, MouseButtonEventArgs e) => GetEmailAddressFromLog();
  void OnGetEmail(object sender, RoutedEventArgs e) => GetEmailAddressFromLog();
  void OnNicknameChanged(object sender, DataTransferEventArgs e)
  {
    var dataGridCell = (DataGridCell)sender;
    //var viewModel = (LogParserVM)dataGridCell.DataContext;
    //var newNickname = viewModel.Nickname;
    // Do something with the newNickname value
    _vm.OnNickUserChanged("54");
  }

  async Task SaveAndClose() { Hide(); await _vm.TrySave(); Close(); }
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
  void GetEmailAddressFromLog()
  {
    var slt = (dg1.SelectedItem as WebEventLog)?.EventName?.Split(':');
    if (slt is null || slt.Length <= 1)
    {
      tbkReport.Content = "Nothing selected in the top DataGrid";
    }
    else
    {
      try
      {
        var timestamp = slt[1];
        var matchingLines = ReadInPotentiallyLockedLogFile(timestamp);
        if (matchingLines.Count <= 0)
        {
          tbkReport.Foreground = System.Windows.Media.Brushes.Orange;
          tbkReport.Content = "No matches in the log";
        }
        else
        {
          var eml = matchingLines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Last();
          tbkReport.Foreground = System.Windows.Media.Brushes.LimeGreen;
          tbkReport.Content = eml;
          Clipboard.SetText(eml);
          _ = synth.SpeakAsync("Copied to Clipboard.");
          //new Window { Title = "Details", Height = 260, Width = 1400, Content = new TextBox { Text = $"{eml}\n\n{string.Join(Environment.NewLine, matchingLines)}\n\n", FontSize = 20, Foreground = System.Windows.Media.Brushes.Blue, TextWrapping = TextWrapping.Wrap, VerticalScrollBarVisibility = ScrollBarVisibility.Auto, HorizontalScrollBarVisibility = ScrollBarVisibility.Auto }}.ShowDialog();
        }
      }
      catch (Exception ex) { /*_ = MessageBox.Show(ex.Message);*/ tbkReport.Content = ex.Message; }
    }
  }
  static List<string> ReadLogFile(string timestamp) => File.ReadAllLines(path).Where(x => x.Contains(timestamp)).ToList();
  static List<string> ReadInPotentiallyLockedLogFile(string timestamp)
  {
    using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
    using var streamReader = new StreamReader(fileStream);
    var lines = new List<string>();
    string? line;
    while ((line = streamReader.ReadLine()) != null)
    {
      if (line.Contains(timestamp))
      {
        lines.Add(line);
      }
    }

    return lines;
  }
  const string path = @"C:\Users\alexp\OneDrive\Public\Logs\MinNavTpl.RAZ.ale.Infi..log";
}