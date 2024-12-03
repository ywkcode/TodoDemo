using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Todo.DragDrop.Models
{
    public class DrawingBase : ShapeBase
    {
        private Geometry _geometry;
        public Geometry Geometry
        {
            get
            {
                return _geometry;
            }
            set
            {
                SetProperty(ref _geometry, value);
            }
        }

        private Point _startPoint;
        public DrawingBase(Point start)
        {
            _startPoint = start;
        }

        public bool OnMouseMove(IInputElement sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var point = e.GetPosition(sender);

                if (Geometry is PathGeometry geometry)
                {

                }
                else
                {
                    geometry = new PathGeometry();
                    var figure = new PathFigure { StartPoint = _startPoint };
                    geometry.Figures.Add(figure);
                }

                LineSegment arc = new LineSegment(point, true) { IsSmoothJoin = true };
                geometry.Figures[0].Segments.Add(arc);
                Geometry = geometry;


                return true;
            }
            else
            {
                return false;
            }
        }

        public bool OnMouseDown(IInputElement sender, MouseButtonEventArgs e)
        {
            return true;
        }

        public bool OnMouseUp(IInputElement sender, MouseButtonEventArgs e)
        {
            Left = Geometry.Bounds.Left;
            Top = Geometry.Bounds.Top;
            Width = Geometry.Bounds.Width;
            Height = Geometry.Bounds.Height;
            return true;
        }


    }
    public class DrawingBaseToolItem : DrawingBase
    {
        public DrawingBaseToolItem(Point start) : base(start)
        {
        }

        public DrawingBaseToolItem() : this(new Point())
        {

        }
    }
}
