namespace AzureLogParser;
public partial class App : Application
{
  readonly LogParserVM _vm = new();
  protected override async void OnStartup(StartupEventArgs e)
  {
    base.OnStartup(e);


    //Listeners.Add(new TextWriterTraceListener(@$"C:\Users\alexp\OneDrive\Public\Logs\{AppDomain.CurrentDomain.FriendlyName}.log"));
    //todo: use serilog:
    AAV.Sys.Helpers.Tracer.SetupTracingOptions("EvLogExplr", new TraceSwitch("OnlyUsedWhenInConfig", "This is the trace for all               messages... but who cares?") { Level = TraceLevel.Verbose });
    //tmi: WriteLine($"\r\n{DateTime.Now:yyyy-MM-dd HH:mm:ss.f} App.OnStartup() -- e.Args.Length:{e.Args.Length}, e.Args[0]:{e.Args.FirstOrDefault()}, {Environment.CommandLine}");

    var hasNewVisits = await _vm.ReLoadLists_CheckIfNews(false);
    AutoFlush = true;
    WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.f}  args[0] {e.Args[0],-26}  {(hasNewVisits ? "New visit[s] detected!" : "°")}");

    if (hasNewVisits || e.Args.FirstOrDefault() != "DonotShowIfNothingNew")
      new MainWindow(_vm).Show();
    else
    {
      await Task.Delay(860);
      Shutdown();
    }
  }
}

