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
using Todo.Extensions;

namespace Todo.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView(IEventAggregator eventAggregator)
        {
            InitializeComponent();
            //注册提示消息的订阅 封装了subscribe
            eventAggregator.ResgiterMessage(arg =>
            {
                LoginSnackerBar.MessageQueue.Enqueue(arg.Message);
            },"Login");
        }
    }
}
