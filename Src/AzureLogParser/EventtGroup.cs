namespace AzureLogParser;

public class EventtGroup
{
  public DateTime LastVisitAt { get; set; }
  public int Count { get; set; }
  public string? Hardware { get; set; }
  public string? MozillaV { get; set; }
  public string? Versions { get; set; }
  public string? CpuCores { get; set; }
  public string? Platform { get; set; }
  public string? Language { get; set; }
  public string? Resolute { get; set; }
  public string PseudoKey => $"{Hardware}|{MozillaV}|{Versions}|{CpuCores}|{Platform}|{Language}|{Resolute}";
  public string NickWare { get; set; } = "";
}
