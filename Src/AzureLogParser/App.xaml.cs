namespace AzureLogParser;
public partial class App : Application
{
  readonly LogParserVM _vm = new();
  protected override async void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);

    Listeners.Add(new TextWriterTraceListener(@$"C:\Users\alexp\OneDrive\Public\Logs\{AppDomain.CurrentDomain.FriendlyName}.log"));

    var hasNewVisits = await _vm.ReLoadLists_CheckIfNews(true);
    WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm} [***] hasNewVisits: {hasNewVisits}.");

    if (hasNewVisits || e.Args.FirstOrDefault() != "DonotShowIfNothingNew")
      new MainWindow(_vm).Show();
    else
    {
      await Task.Delay(1000);
      Shutdown();
    }

    Flush();
    Close();
  }
}

