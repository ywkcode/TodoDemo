using DryIoc;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Dialogs;
using Todo.Shared.Dtos;

namespace Todo.ViewModels.Dialogs
{
    public class AddToDoViewModel : BindableBase, IDialogHostAware
    {
        public AddToDoViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
        }
        public string DialogHostName { get; set; }
        /// <summary>
        /// 确定
        /// </summary>
        public DelegateCommand SaveCommand { get; set; }

        /// <summary>
        /// 取消
        /// </summary>
        public DelegateCommand CancelCommand { get; set; }

        private ToDoDto model;

        /// <summary>
        /// 新增或编辑的实体
        /// </summary>
        public ToDoDto Model
        {
            get { return model; }
            set { model = value; RaisePropertyChanged(); }
        }

        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No)); //取消返回NO告诉操作结束
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Model.Title) ||
                string.IsNullOrWhiteSpace(model.Content)) return;

            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                //确定时,把编辑的实体返回并且返回OK
                DialogParameters param = new DialogParameters();
                param.Add("Value", Model);
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK));
            }
        }

        public void OnDialogOpend(IDialogParameters parameters)
        {
            
        }
    }
}
