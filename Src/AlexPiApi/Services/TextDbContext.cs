using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AlexPiApi.Services;

public class TextDbContext : ITextDbContext
{
  long _counter = 0;
  readonly PoorMansLogger _poorMansLogger;

  public TextDbContext(string connectionString)
  {
    _poorMansLogger = new PoorMansLogger(connectionString);
  }

  public string Audit() => $"▀{nameof(TextDbContext)} -- {_counter} lines added this session▀";

  public async Task AddStringAsync(string text)
  {
    Trace.WriteLine(text);
    await _poorMansLogger.AppendToFileAsync($"{DateTime.Now}{_counter++,5}  {text}\r\n");
  }
}
