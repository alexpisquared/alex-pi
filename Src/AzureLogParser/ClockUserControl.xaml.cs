using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AzureLogParser;
public partial class ClockUserControl : UserControl
{
  public ClockUserControl()
  {
    InitializeComponent();
  }

  async void OnLoaded(object sender, RoutedEventArgs e)
  {
    DataContext = this;

    while (true)
    {
      Now = DateTime.Now;
      await Task.Delay(100);
    }
  }



  public static readonly DependencyProperty NowProperty = DependencyProperty.Register("Now", typeof(DateTime), typeof(ClockUserControl)/*, new PropertyMetadata(0)*/); public DateTime Now { get { return (DateTime)GetValue(NowProperty); } set { SetValue(NowProperty, value); } }


}
