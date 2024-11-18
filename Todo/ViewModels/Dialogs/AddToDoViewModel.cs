using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.ViewModels.Dialogs
{
    public class AddToDoViewModel  : BindableBase, IDialogAware
    {

        public string Title { get; set; }

        public DialogCloseListener RequestClose { get; } 

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
