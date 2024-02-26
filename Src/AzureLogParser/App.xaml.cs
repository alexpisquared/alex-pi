namespace AzureLogParser;
public partial class App : Application
{
  readonly LogParserVM _vm = new();
  protected override async void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);

    var hasNewVisits = await _vm.ReLoadLists_CheckIfNews(true);
    if (hasNewVisits || e.Args.FirstOrDefault() != "DonotShowIfNothingNew")
      new MainWindow(_vm).Show();
    else
    {
      await Task.Delay(1000);
      Shutdown();
    }
  }
}

