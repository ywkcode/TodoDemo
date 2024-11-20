using System.Windows;
using Todo.Common.Dialogs;
using Todo.Common.Models;
using Todo.Extensions;
using Todo.ViewModels;

namespace Todo.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
      
        public MainView(IEventAggregator aggregator,  IDialogHostService dialogHostService)
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
            //退出系统提示
            btnClose.Click += async (s, e) => 
            {
                var dialogResult = await dialogHostService.Question("温馨提示", "确认退出系统？");
                if (dialogResult.Result !=  ButtonResult.OK) return;
                this.Close();
            };

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
