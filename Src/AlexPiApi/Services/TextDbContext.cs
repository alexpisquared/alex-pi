using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AlexPiApi.Services;

public class TextDbContext : ITextDbContext
{
  long _counter = 0;
  readonly PoorMansLogger _poorMansLogger;

  public TextDbContext(string connectionString) => _poorMansLogger = new PoorMansLogger(connectionString);

  public string Audit() => $"▀{nameof(TextDbContext)} -- {_counter} lines added this session▀";

  public async Task AddStringAsync(string text)
  {
    var r = $"{DateTime.Now:MM-dd HH:mm}{_counter++,5}  {text}\r\n";
    Trace.WriteLine(r);
    await _poorMansLogger.AppendToFileAsync(r);
  }
}
