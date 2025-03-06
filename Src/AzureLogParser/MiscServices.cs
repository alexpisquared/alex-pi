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
  public static bool IsThereAreNewLogEntriesAndStoreLastNewLogTime(DateTime lastTimeFromAzure, string filePath = @"C:\temp\potentiallyNewUsageTime.txt")
  {
    try
    {
      if (File.Exists(filePath) && DateTime.TryParse(File.ReadAllText(filePath), out var lastTimeFromCache))
      {
        if (lastTimeFromAzure > lastTimeFromCache)
        {
          File.WriteAllText(filePath, lastTimeFromAzure.ToString("o"));
          return true;
        }
      }
      else
      {
        File.WriteAllText(filePath, lastTimeFromAzure.ToString("o"));
        return true;
      }
    }
    catch (Exception ex) { WriteLine($"\n{DateTime.Now:yyyy-MM-dd HH:mm}  ERR  \n  {ex}"); _ = System.Windows.MessageBox.Show(nameof(IsThereAreNewLogEntriesAndStoreLastNewLogTime), ex.Message); }

    return false;
  }
}