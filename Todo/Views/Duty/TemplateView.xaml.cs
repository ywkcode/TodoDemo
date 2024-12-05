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
using Todo.DragDrop.Models;
using Todo.ViewModels.Duty;

namespace Todo.Views.Duty
{
    /// <summary>
    /// TemplateView.xaml 的交互逻辑
    /// </summary>
    public partial class TemplateView : UserControl
    {
        public TemplateView()
        {
            InitializeComponent();
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        
        {
            // 这里是点击事件发生时执行的代码
            //MessageBox.Show("Rectangle 被点击了！");
            var selectItem = (RectangleBase)((Rectangle)e.OriginalSource).DataContext;
            if (selectItem != null)
            {
                TemplateViewModel mainViewModel = this.DataContext as TemplateViewModel;
                mainViewModel.SelectedItem = selectItem;
            }
        }
    }
}
