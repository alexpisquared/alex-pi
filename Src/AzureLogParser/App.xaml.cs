namespace AzureLogParser;
public partial class App : Application
{
  readonly LogParserVM _vm = new();
  protected override async void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);

    Listeners.Add(new TextWriterTraceListener(@$"C:\Users\alexp\OneDrive\Public\Logs\{AppDomain.CurrentDomain.FriendlyName}.log"));

    var hasNewVisits = await _vm.ReLoadLists_CheckIfNews(false);
    if (hasNewVisits)
      WriteLine($"\n{DateTime.Now:yyyy-MM-dd HH:mm}  New visit[s] detected!");

    if (hasNewVisits || e.Args.FirstOrDefault() != "DonotShowIfNothingNew")
      new MainWindow(_vm).Show();
    else
    {
      Write($"*");
      await Task.Delay(860);
      Shutdown();
    }

    Flush();
    Close();
  }
}

