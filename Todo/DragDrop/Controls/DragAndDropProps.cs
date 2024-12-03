using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Todo.DragDrop.Models;

namespace Todo.DragDrop.Controls
{
    public static class DragAndDropProps
    {
        #region EnabledForDrag

        public static readonly DependencyProperty EnabledForDragProperty =
            DependencyProperty.RegisterAttached("EnabledForDrag", typeof(bool), typeof(DragAndDropProps),
                new FrameworkPropertyMetadata(false,
                    new PropertyChangedCallback(OnEnabledForDragChanged)));

        public static bool GetEnabledForDrag(DependencyObject d)
        {
            return (bool)d.GetValue(EnabledForDragProperty);
        }

        public static void SetEnabledForDrag(DependencyObject d, bool value)
        {
            d.SetValue(EnabledForDragProperty, value);
        }

        private static void OnEnabledForDragChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement fe = (FrameworkElement)d;


            if ((bool)e.NewValue)
            {
                fe.PreviewMouseDown += Fe_PreviewMouseDown;
                fe.PreviewMouseMove += Fe_MouseMove;
            }
            else
            {
                fe.PreviewMouseDown -= Fe_PreviewMouseDown;
                fe.PreviewMouseMove -= Fe_MouseMove;
            }
        }
        #endregion

        #region DragStartPoint

        public static readonly DependencyProperty DragStartPointProperty =
            DependencyProperty.RegisterAttached("DragStartPoint", typeof(Point?), typeof(DragAndDropProps));

        public static Point? GetDragStartPoint(DependencyObject d)
        {
            return (Point?)d.GetValue(DragStartPointProperty);
        }


        public static void SetDragStartPoint(DependencyObject d, Point? value)
        {
            d.SetValue(DragStartPointProperty, value);
        }

        #endregion

        static void Fe_MouseMove(object sender, MouseEventArgs e)
        {
           


            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            Point? dragStartPoint = GetDragStartPoint((DependencyObject)sender);
            var point = e.GetPosition((IInputElement)sender);
            if (dragStartPoint == point)
                return;

            if (dragStartPoint.HasValue && ((FrameworkElement)sender).DataContext is ShapeBase toolBoxData)
            {
                DragObject dataObject = new DragObject();

                string type = toolBoxData.GetType().FullName.Replace("ToolItem", "");
                dataObject.ShapeBase = Activator.CreateInstance(Type.GetType(type)) as ShapeBase;
                


                System.Windows.DragDrop.DoDragDrop((DependencyObject)sender, dataObject, DragDropEffects.Copy);
                e.Handled = true;
            }
        }

        static void Fe_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

            SetDragStartPoint((DependencyObject)sender, e.GetPosition((IInputElement)sender));
        }
    }
}
