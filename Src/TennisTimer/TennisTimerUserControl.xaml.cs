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
    const double everyXMin = 5;

    pb1.Maximum = everyXMin * 60;

    while (true)
    {
      DateTime now, next = FindNextTime(everyXMin);

      do
      {
        await Task.Delay(250);
        now = DateTime.Now;
        tb1.Text = $"{next - now:mm\\:ss}";
        pb1.Value = (next - now).TotalSeconds;
      } while (now < next);

      tb1.Text = "▄▀▄▀▄";
      PlayWavFilesAsync(2);
    }
  }

  static DateTime FindNextTime(double everyXMin)
  {
    var now = DateTime.Now;
    return new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0).AddMinutes(((now.Minute / everyXMin) + 1) * everyXMin);
  }

  void PlayWavFilesAsync(int i)
  {
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
  }

  void PlayResourse(string filePath) => new SoundPlayer(filePath).PlaySync();

  void OnClose(object sender, RoutedEventArgs e) => Window.GetWindow(this).Close();
}
