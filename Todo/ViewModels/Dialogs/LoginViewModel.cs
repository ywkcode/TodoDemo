using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Service;

namespace Todo.ViewModels.Dialogs
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        private readonly ILoginService  loginService;
        public LoginViewModel(ILoginService loginServiceArg)
        {
            ExecuteCommand = new DelegateCommand<string>(Execute);
            loginService = loginServiceArg;
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Login":Login();break;
                case "LoginOut": LoginOut();break;
            }
        }
        async void Login() 
        {
            if (string.IsNullOrWhiteSpace(Account) ||
                string.IsNullOrWhiteSpace(PassWord))
            {
                return;
            }

            var loginResult = await loginService.LoginAsync(new Shared.Dtos.UserDto()
            {
                Account = Account,
                PassWord = PassWord
            });

            if (loginResult != null && loginResult.Status)
            {
                RequestClose.Invoke(new DialogResult(ButtonResult.OK));
            }
            else
            {
                //登录失败提示...
               // aggregator.SendMessage(loginResult.Message, "Login");
            }
        }
        void LoginOut() 
        {
            RequestClose.Invoke(new DialogResult(ButtonResult.No));
        }
        #region 属性
        private string account;

        public string Account
        {
            get { return account; }
            set { account = value;RaisePropertyChanged(); }
        }

        private string passWord;

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value; RaisePropertyChanged(); }
        }

        #endregion

        public DelegateCommand<string> ExecuteCommand { get; set; }

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
