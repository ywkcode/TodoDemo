using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Todo.DragDrop.Models;
using Todo.ViewModels.Duty;

namespace Todo.DragDrop.Controls
{
    public class DrawAdorner : Adorner
    {
        private Point? startPoint;
        private Point? endPoint;
        private List<Point> pointList = new List<Point>();
        private Pen _rubberbandPen;

        private DiagramCanvas _designerCanvas;

        private TemplateViewModel _viewModel
        {
            get
            {
                return _designerCanvas.DataContext as TemplateViewModel;
            }
        }

        private DrawingBase _drawingBase;

        public DrawAdorner(DiagramCanvas designerCanvas, Point? dragStartPoint)
            : base(designerCanvas)
        {
            this._designerCanvas = designerCanvas;
            this._designerCanvas.Focus();
            this.startPoint = dragStartPoint;

            if (_viewModel.SelectedToolItem is DrawingBaseToolItem drawingBaseToolItem)
            {
                _rubberbandPen = new Pen(Brushes.Orange, 1);

                _drawingBase = new DrawingBase(dragStartPoint.Value);
            }
            else
            {
                _rubberbandPen = new Pen(Brushes.LightSlateGray, 1);

                _rubberbandPen.DashStyle = new DashStyle(new double[] { 2 }, 1);
            }

        }

        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e)
        {
            if (_drawingBase?.OnMouseMove(this, e) == true)
            {
                if (!this.IsMouseCaptured)
                    this.CaptureMouse();

                this.InvalidateVisual();
            }
            else if (_drawingBase == null && e.LeftButton == MouseButtonState.Pressed)
            {
                if (!this.IsMouseCaptured)
                    this.CaptureMouse();

                endPoint = e.GetPosition(this);

                UpdateSelection();
                this.InvalidateVisual();
            }
            else
            {
                if (this.IsMouseCaptured) this.ReleaseMouseCapture();
            }

            e.Handled = true;
        }

        protected override void OnMouseUp(System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_drawingBase != null && _drawingBase?.OnMouseUp(this, e) == false)
            {
                return;
            }

            // release mouse capture
            if (this.IsMouseCaptured) this.ReleaseMouseCapture();

            // remove this adorner from adorner layer
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this._designerCanvas);
            if (adornerLayer != null)
                adornerLayer.Remove(this);

            if (_drawingBase != null)
            {
                _viewModel.Items.Add(_drawingBase);
            }
            else if (_viewModel.SelectedToolItem != null && this.startPoint.HasValue && this.endPoint.HasValue)
            {
                string type = _viewModel.SelectedToolItem.GetType().FullName.Replace("ToolItem", "");
                var lineBase = Activator.CreateInstance(Type.GetType(type)) as ShapeBase;

                var rect = new Rect(this.startPoint.Value, this.endPoint.Value);
                lineBase.Left = rect.X;
                lineBase.Top = rect.Y;
                lineBase.Width = rect.Width;
                lineBase.Height = rect.Height;
                _viewModel.Items.Add(lineBase);
            }

            e.Handled = true;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            // without a background the OnMouseMove event would not be fired !
            // Alternative: implement a CanvasBrush as a child of this adorner, like
            // the ConnectionAdorner does.
            dc.DrawRectangle(Brushes.Transparent, null, new Rect(RenderSize));
            if (_drawingBase != null)
            {
                dc.DrawGeometry(Brushes.Transparent, _rubberbandPen, _drawingBase.Geometry);
            }
            else if (this.startPoint.HasValue && this.endPoint.HasValue)
            {
                dc.DrawRectangle(Brushes.Transparent, _rubberbandPen, new Rect(this.startPoint.Value, this.endPoint.Value));
            }
        }

        private void UpdateSelection()
        {


        }
    }
}
