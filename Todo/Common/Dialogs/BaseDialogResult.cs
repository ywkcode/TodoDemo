using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common.Dialogs
{
   

    public enum BaseDialogButton
    {
        Cancle=0, //取消
        Ok=1 
    }

    public class BaseDialogResult
    {
        public BaseDialogButton Button { get; set; }

        public object? Data { get; set; }
    }

    public class BaseDialogParameters
    {
        public string DialogHost { get; set; } = "Root";

        public string DialogTitle { get; set; }

        public string DialogContent { get; set; }
    }
}
