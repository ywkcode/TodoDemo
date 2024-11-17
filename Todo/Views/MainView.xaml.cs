using System.Windows;
using Todo.Common.Models;
using Todo.Extensions;

namespace Todo.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(IEventAggregator aggregator)
        {
            InitializeComponent();

            //订阅 等待消息发送
            aggregator.Register(arg =>
            {
                DialogHost.IsOpen = arg.IsOpen;
                if (DialogHost.IsOpen)
                {
                    DialogHost.DialogContent = new ProgressView();
                }
            });
             btnMin.Click += (s, e) => { this.WindowState = WindowState.Minimized; };
            btnMax.Click += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                }
                else
                    this.WindowState = WindowState.Maximized;
            };
            btnClose.Click += (s, e) => { this.Close(); };

            ColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };
            //双击事件
            ColorZone.MouseDoubleClick += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                }
                else
                    this.WindowState = WindowState.Maximized;
            };

            //收起左侧菜单
            menuBar.SelectionChanged += (s, e) =>
            {
                ColorZone.IsLeftDrawerOpen = false;
            };
        }
 
    }
}
