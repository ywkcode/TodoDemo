using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common.Dialogs
{
    public interface IDialogHostService : IDialogService
    {
        Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "Root");

        Task<IDialogResult> ShowSuccessDialog(string dialogHostName = "Root");

        Task<IDialogResult> ShowWarningDialog(string content,string dialogHostName = "Root");


        Task<IDialogResult> ShowErrorDialog(string content, string dialogHostName = "Root");
    }
} 
