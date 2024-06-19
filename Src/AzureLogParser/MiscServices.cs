namespace AzureLogParser;
public class MiscServices
{
  public static bool SaveBlob(string blob, string filePath)
  {
    try
    {
      File.WriteAllText(filePath, blob);
      return true;
    }
    catch (Exception ex) { WriteLine($"\n{DateTime.Now:yyyy-MM-dd HH:mm}  ERR  \n  {ex}"); _ = System.Windows.MessageBox.Show(nameof(SaveBlob), ex.Message); }

    return false;
  }
  public static bool NotifyIfThereAreNewLogEntriesAndStoreLastNewLogTime(DateTime potentiallyNewUsageTime, string filePath)
  {
    try
    {
      var lastKnownTimeOfUsage = File.Exists(filePath) ? File.ReadAllText(filePath) : null;
      if (lastKnownTimeOfUsage != null && DateTime.TryParse(lastKnownTimeOfUsage, out var lastKnownTime))
      {
        if (potentiallyNewUsageTime > lastKnownTime)
        {
          File.WriteAllText(filePath, potentiallyNewUsageTime.ToString("o"));
          return true;
        }
      }
      else
      {
        File.WriteAllText(filePath, potentiallyNewUsageTime.ToString("o"));
        return true;
      }
    }
    catch (Exception ex) { WriteLine($"\n{DateTime.Now:yyyy-MM-dd HH:mm}  ERR  \n  {ex}"); _ = System.Windows.MessageBox.Show(nameof(NotifyIfThereAreNewLogEntriesAndStoreLastNewLogTime), ex.Message); }

    return false;
  }
}