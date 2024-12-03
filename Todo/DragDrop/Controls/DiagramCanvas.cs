using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Todo.DragDrop.Models;
using Todo.ViewModels.Duty;

namespace Todo.DragDrop.Controls
{
    public class DiagramCanvas : Canvas
    {
        private Point? rubberbandSelectionStartPoint = null;
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //if we are source of event, we are rubberband selecting
                if (e.Source == this)
                {
                    // in case that this click is the start for a 
                    // drag operation we cache the start point
                    Point currentPoint = e.GetPosition(this);
                    rubberbandSelectionStartPoint = currentPoint;

                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // if mouse button is not pressed we have no drag operation, ...
            if (e.LeftButton != MouseButtonState.Pressed)
                rubberbandSelectionStartPoint = null;

            // ... but if mouse button is pressed and start
            // point value is set we do have one
            if (this.rubberbandSelectionStartPoint.HasValue)
            {
                // create rubberband adorner
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    DrawAdorner adorner = new DrawAdorner(this, rubberbandSelectionStartPoint);
                    if (adorner != null)
                    {
                        adornerLayer.Add(adorner);
                    }
                }
            }
        }

        protected override void OnDragOver(DragEventArgs e)
        {
            DragObject dragObject = e.Data.GetData(typeof(DragObject)) as DragObject;


            base.OnDragOver(e);
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            Debug.WriteLine("OnDrop");
            DragObject dragObject = e.Data.GetData(typeof(DragObject)) as DragObject;
            if (dragObject != null  )
            {
                Point position = e.GetPosition(this);

                TemplateViewModel mainViewModel = this.DataContext as TemplateViewModel;
                var lineBase = Activator.CreateInstance(dragObject.ShapeBase.GetType()) as ShapeBase;
                lineBase.Left = position.X;
                lineBase.Top = position.Y;
                lineBase.Width = 100;
                lineBase.Height = 100;
                mainViewModel.Items.Add(lineBase);
                Debug.WriteLine("Add Shape");
            }

            e.Handled = true;

            this.Focus();
        }
    }
}
