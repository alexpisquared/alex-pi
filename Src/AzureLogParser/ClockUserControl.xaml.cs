using System.Windows.Controls;

namespace AzureLogParser;
public partial class ClockUserControl : System.Windows.Controls.UserControl
{
  public ClockUserControl() => InitializeComponent();

  async void OnLoaded(object sender, RoutedEventArgs e)
  {
    DataContext = this;

    while (true)
    {
      Now = DateTime.Now;
      await Task.Delay(100);
    }
  }

  public static readonly DependencyProperty NowProperty = DependencyProperty.Register("Now", typeof(DateTime), typeof(ClockUserControl)/*, new PropertyMetadata(0)*/); public DateTime Now { get => (DateTime)GetValue(NowProperty); set => SetValue(NowProperty, value); }
}