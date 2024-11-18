﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.ViewModels.Dialogs
{
    public class LoginViewModel : BindableBase, IDialogAware
    {

        public string Title { get; set; } = "自定义名称";
        public DialogCloseListener RequestClose { get; set; }
         

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
             
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
             
        }
    }
}
