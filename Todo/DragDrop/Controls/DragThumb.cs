using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Todo.DragDrop.Models;

namespace Todo.DragDrop.Controls
{
    public class DragThumb:Thumb
    {
        public DragThumb()
        {
            base.DragDelta += DragThumb_DragDelta;
            base.DragStarted += DragThumb_DragStarted;
            base.DragCompleted += DragThumb_DragCompleted;
        }


        private ShapeBase ShapeBase { get { return DataContext as ShapeBase; } }

        private void DragThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
           
        }

        private void DragThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            GetDesignerCanvas(this)?.Focus();
        }

        private void DragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (ShapeBase != null)
            {
                ShapeBase.Left += e.HorizontalChange;
                ShapeBase.Top += e.VerticalChange;
            }
        }

        private Canvas GetDesignerCanvas(DependencyObject element)
        {
            while (element != null && !(element is Canvas))
                element = VisualTreeHelper.GetParent(element);

            return element as Canvas;
        }
    }
}
