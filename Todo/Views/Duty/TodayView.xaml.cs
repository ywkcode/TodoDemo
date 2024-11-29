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

namespace Todo.Views.Duty
{
    /// <summary>
    /// TodayView.xaml 的交互逻辑
    /// </summary>
    public partial class TodayView : UserControl
    {
        private static Timer timer;
        public TodayView()
        {
            InitializeComponent();
            var timerCallback = new TimerCallback(DisplayTimeEvent);
            timer = new Timer(timerCallback, null, 0, 1000);
        }
        //回调逻辑
        private static void DisplayTimeEvent(Object? stateInfo)
        {
            Console.WriteLine("The TimerCallback event was raised at {0:HH:mm:ss.fff}", DateTime.Now);
        }
    }
}
