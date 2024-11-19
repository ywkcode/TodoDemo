using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Service;
using Todo.Shared.Dtos;

namespace Todo.ViewModels.Dialogs
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        private readonly ILoginService  loginService;
        public LoginViewModel(ILoginService loginServiceArg)
        {
            ExecuteCommand = new DelegateCommand<string>(Execute);
            loginService = loginServiceArg;
            UserDto = new ResgiterUserDto();
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "Login":Login();break;
                case "LoginOut": LoginOut();break;
                case "ResgiterPage": SelectIndex = 1;break;
                case "Register": Register(); break;
                case "Return": SelectIndex=0; break;
            }
        }
        async void Login() 
        {
            if (string.IsNullOrWhiteSpace(Account) ||
                string.IsNullOrWhiteSpace(PassWord))
            {
                return;
            }

            var loginResult = await loginService.LoginAsync(new UserDto()
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

        private async void Register()
        {
            if (string.IsNullOrWhiteSpace(UserDto.Account) ||
                   string.IsNullOrWhiteSpace(UserDto.UserName) ||
                   string.IsNullOrWhiteSpace(UserDto.PassWord) ||
                   string.IsNullOrWhiteSpace(UserDto.NewPassWord))
            {
                //aggregator.SendMessage("请输入完整的注册信息！", "Login");
                return;
            }

            if (UserDto.PassWord != UserDto.NewPassWord)
            {
                //aggregator.SendMessage("密码不一致,请重新输入！", "Login");
                return;
            }

            var resgiterResult = await loginService.Register(new  UserDto()
            {
                Account = UserDto.Account,
                UserName = UserDto.UserName,
                PassWord = UserDto.PassWord
            });

            if (resgiterResult != null && resgiterResult.Status)
            {
                //aggregator.SendMessage("注册成功", "Login");
                //注册成功,返回登录页页面
                SelectIndex = 0;
            }
            else
            { 
            
            }
                //aggregator.SendMessage(resgiterResult.Message, "Login");
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
        private int selectIndex;

        public int SelectIndex
        {
            get { return selectIndex; }
            set { selectIndex = value; RaisePropertyChanged(); }
        }
        private ResgiterUserDto userDto;

        public ResgiterUserDto UserDto
        {
            get { return userDto; }
            set { userDto = value; RaisePropertyChanged(); }
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
