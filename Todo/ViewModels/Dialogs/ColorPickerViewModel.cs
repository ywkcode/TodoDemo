using MaterialDesignColors;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Dialogs;
using MaterialDesignThemes.Wpf;
using System.Reflection.Metadata;

namespace Todo.ViewModels.Dialogs
{
    public class ColorPickerViewModel : BindableBase, IDialogHostAware
    {
        public ColorPickerViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
            GetColorCommand = new DelegateCommand<object>(GetColor);
            PickColor = "#FF5E35B1";
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; RaisePropertyChanged(); }
        }


        private string pickColor;

        public string PickColor
        {
            get { return pickColor; }
            set { pickColor = value; RaisePropertyChanged(); }
        }

        private void GetColor(object obj)
        {
            var hue = (Color)obj;
            PickColor= obj.ToString();
        }
        /// <summary>
        /// 关闭弹窗
        /// </summary>
        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No)); //取消返回NO告诉操作结束
        }

        private void Save()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                //确定时,把编辑的实体返回并且返回OK
                DialogParameters param = new DialogParameters();
                param.Add("Value", PickColor);
                param.Add("Type", Type);
                var dialog = new DialogResult()
                {
                    Parameters = param,
                    Result = ButtonResult.OK
                };
                DialogHost.Close(DialogHostName, dialog);
            }
        }

        public string DialogHostName { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public DelegateCommand<object> GetColorCommand { get; set; }

        public void OnDialogOpend(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Type"))
                Type = parameters.GetValue<string>("Type"); 
        }


        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;

        public DialogCloseListener RequestClose { get; set; }
    }
}
