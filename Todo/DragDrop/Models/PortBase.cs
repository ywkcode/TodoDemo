using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.DragDrop.Models
{
    public class PortBase : BindableBase
    {
        private PortOrientation _orientation;
        public PortOrientation Orientation
        {
            get
            {
                return _orientation;
            }
            set
            {
                SetProperty(ref _orientation, value);
            }
        }
    }

    public enum PortOrientation
    {
        None = 0,
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left,
        TopLeft
    }
}
