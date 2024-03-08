namespace AzureLogParser;
public partial class App : Application
{
  readonly LogParserVM _vm = new();
  protected override async void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);

    Listeners.Add(new TextWriterTraceListener(@$"C:\Users\alexp\OneDrive\Public\Logs\{AppDomain.CurrentDomain.FriendlyName}.log"));

    var hasNewVisits = await _vm.ReLoadLists_CheckIfNews(false);

    if (hasNewVisits || e.Args.FirstOrDefault() == "DonotShowIfNothingNew")
      WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm}  {(hasNewVisits ? "New visit[s] detected!" : "")}");

    if (hasNewVisits || e.Args.FirstOrDefault() != "DonotShowIfNothingNew")
      new MainWindow(_vm).Show();
    else
    {
      await Task.Delay(750);
      Shutdown();
    }

    Flush();
    Close();
  }
}

