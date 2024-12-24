using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.DragDrop.Models
{
    /// <summary>
    /// 矩形
    /// </summary>
    public class RectangleBase:ShapeBase
    {
      
    }
    public class RectangleBaseToolItem : RectangleBase
    {
        private string _displayName;
        public string DisplayName
        {
            get { return _displayName; }
            set { SetProperty(ref _displayName, value); }
        }

        private string _displayColor;
        public string DisplayColor
        {
            get { return _displayColor; }
            set { SetProperty(ref _displayColor, value); }
        }
    }
}
