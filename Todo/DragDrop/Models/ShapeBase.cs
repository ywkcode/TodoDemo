using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.DragDrop.Models
{
    /// <summary>
    /// 形状基类
    /// </summary>
    public class ShapeBase : BindableBase
    {
        private double _left;
        public double Left
        {
            get { return _left; }
            set { SetProperty(ref _left, value); }
        }

        private double _top;
        public double Top
        {
            get { return _top; }
            set { SetProperty(ref _top, value); }
        }

        private double _width;
        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                SetProperty(ref _width, value);
            }
        }

        private double _height;
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                SetProperty(ref _height, value);
            }
        }

        private bool _isSelectd;
        public bool IsSelectd {
            get
            {
                return _isSelectd;
            }
            set
            {
                SetProperty(ref _isSelectd, value);
            }
        }

        private string _id;
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                SetProperty(ref _id, value);
            }
        }
    }
}
