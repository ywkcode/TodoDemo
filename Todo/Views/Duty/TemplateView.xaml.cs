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
        private void Thumb_DragDelta_Resize(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Control thumb = sender as Control;
            var selectItem = (RectangleBase)thumb.DataContext;

            if (selectItem != null)
            {
                double deltaVertical, deltaHorizontal;

                switch (thumb.VerticalAlignment)
                {
                    case VerticalAlignment.Bottom:
                        deltaVertical = Math.Min(-e.VerticalChange, selectItem.Height);
                        selectItem.Height -= deltaVertical;
                        break;
                    case VerticalAlignment.Top:
                        deltaVertical = Math.Min(e.VerticalChange, selectItem.Height);
                        selectItem.Top = deltaVertical;
                        selectItem.Height -= deltaVertical;
                        break;
                    default:
                        break;
                }
                switch (thumb.HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        deltaHorizontal = Math.Min(e.HorizontalChange, selectItem.Width  );
                      
                        selectItem.Width -= deltaHorizontal;
                        break;
                    case HorizontalAlignment.Right:
                        deltaHorizontal = Math.Min(-e.HorizontalChange, selectItem.Width);
                        selectItem.Width -= deltaHorizontal;
                        break;
                    default:
                        break;
                }
                TemplateViewModel mainViewModel = this.DataContext as TemplateViewModel;
                mainViewModel.SelectedItem = selectItem;
            }
            e.Handled = true;
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
