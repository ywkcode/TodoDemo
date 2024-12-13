﻿using DryIoc;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Todo.Base;

namespace Todo.Common.Dialogs
{
    /// <summary>
    /// 对话主机服务(自定义)
    /// </summary>
    public class DialogHostService : DialogService, IDialogHostService
    {
        private readonly IContainerExtension containerExtension;

        public DialogHostService(IContainerExtension containerExtension) : base(containerExtension)
        {
            this.containerExtension = containerExtension;
        }

        

        public async Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "Root")
        {
            if (parameters == null)
                parameters = new DialogParameters();

            //从容器当中去除弹出窗口的实例
            var content = containerExtension.Resolve<object>(name);

            //验证实例的有效性 
            if (!(content is FrameworkElement dialogContent))
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");

            if (dialogContent is FrameworkElement view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
                ViewModelLocator.SetAutoWireViewModel(view, true);

            if (!(dialogContent.DataContext is IDialogHostAware viewModel))
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");

            viewModel.DialogHostName = dialogHostName;

            DialogOpenedEventHandler eventHandler = (sender, eventArgs) =>
            {
                if (viewModel is IDialogHostAware aware)
                {
                    aware.OnDialogOpend(parameters);
                }
                eventArgs.Session.UpdateContent(content);
            };

            return (IDialogResult)await DialogHost.Show(dialogContent, viewModel.DialogHostName, eventHandler);
        }

        public async Task<IDialogResult> ShowErrorDialog(string content, string dialogHostName = "Root")
        {
            var parameters = new DialogParameters();
            parameters.Add("Content", content);
            return await this.ShowDialog(DefaultConst.Default_MessageView, parameters, dialogHostName);
        }

        public async Task<IDialogResult> ShowSuccessDialog( string dialogHostName = "Root")
        {
            var parameters = new DialogParameters();
            parameters.Add("Content", "操作成功");
            parameters.Add("Title", "消息提示");
            return await this.ShowDialog(DefaultConst.Default_MessageView, null, dialogHostName);
        }

        public async Task<IDialogResult> ShowWarningDialog(string content,string dialogHostName = "Root")
        {
            var parameters = new DialogParameters();
            parameters.Add("Content", content);
            parameters.Add("Title", "消息提醒");
            return await this.ShowDialog(DefaultConst.Default_MessageView, parameters, dialogHostName);
        }
    }
}
