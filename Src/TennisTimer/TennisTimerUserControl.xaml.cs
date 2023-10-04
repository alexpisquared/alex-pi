using System.Media;
using System.Windows;
using System.Windows.Controls;

namespace TennisTimer;

public partial class TennisTimerUserControl : UserControl
{
  public TennisTimerUserControl()
  {
    InitializeComponent();
    Loaded += OnLoaded;
  }

  async void OnLoaded(object s, RoutedEventArgs e)
  {
    const double everyXMin = 1;

    pb1.Maximum = everyXMin * 60;

    while (true)
    {
      DateTime now, next = FindNextTime(everyXMin);

      do
      {
        await Task.Delay(100);
        now = DateTime.Now;
        tb1.Text = $"{next - now:mm\\:ss}";
        pb1.Value = (next - now).TotalSeconds;
      } while (now < next);

      await PlayWavFilesAsync(2);
    }

    static DateTime FindNextTime(double everyXMin)
    {
      var now = DateTime.Now;
      return new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0).AddMinutes(((now.Minute / everyXMin) + 1) * everyXMin);
    }
  }

  async Task PlayWavFilesAsync(int i)
  {
    await Task.Delay(100);
    switch (i)
    {
      case 1:
        PlayResourse(@"C:\Users\alexp\source\repos\alex-pi\TennisTimer\Media\Start - Arcade Power Up.wav");
        PlayResourse(@"C:\Users\alexp\source\repos\alex-pi\TennisTimer\Media\en-US-AriaNeural~1.00~33~friendly~Last minute!.wav");
        break;
      default:
        PlayResourse(@"C:\Users\alexp\source\repos\alex-pi\TennisTimer\Media\Good - Fanfare.wav");
        PlayResourse(@"C:\Users\alexp\source\repos\alex-pi\TennisTimer\Media\en-US-AriaNeural~1.00~33~friendly~Time to change!.wav");
        break;
    }

    await Task.Delay(5000);
  }

  void PlayResourse(string filePath) => new SoundPlayer(filePath).PlaySync();

  private void OnClose(object sender, RoutedEventArgs e)
  {
    Window.GetWindow(this).Close();
  }
}
