using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.DragDrop.Models
{
    public class TemplateObject:BindableBase
    {
        public TemplateObject()
        {
            ShapeBases=new List<ShapeBase>();
        }
        private FormProp _tempformProp;

        public FormProp TempFormProp
        {
            get { return _tempformProp; }
            set { SetProperty(ref _tempformProp, value); }
        }

        private List<ShapeBase> _shapeBases;

        public List<ShapeBase> ShapeBases
        {
            get { return _shapeBases; }
            set { SetProperty(ref _shapeBases, value); }
        }

    }

    public class FormProp:BindableBase
    {
        /// <summary>
        /// 宽度
        /// </summary>
        private double _width;

        public double Width
        {
            get { return _width; }
            set { SetProperty(ref _width, value); }
        }

        /// <summary>
        /// 高度
        /// </summary>
        private double  _height;

        public double  Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); }
        }

        /// <summary>
        /// 背景图路径
        /// </summary>
        private string? _bgImgUrl;

        public string? BgImgUrl
        {
            get { return _bgImgUrl; }
            set { SetProperty(ref _bgImgUrl, value); }
      
        }

        /// <summary>
        /// 背景图名称
        /// </summary>
        private string? _bgImgName;

        public string? BgImgName
        {
            get { return _bgImgName; } 
            set { SetProperty(ref _bgImgName, value); }
        }

        /// <summary>
        /// 背景色
        /// </summary>
        private string? _bgColor;

        public string? BgColor
        {
            get { return _bgColor; }
            set { SetProperty(ref _bgColor, value); }
        }


    }
}
