namespace Todo.Models
{
    public class ResgiterUserDto : BaseDto
    {
        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value;    }
        }

        private string account;

        public string Account
        {
            get { return account; }
            set { account = value;  }
        }

        private string passWord;

        public string PassWord
        {
            get { return passWord; }
            set { passWord = value;  }
        }

        private string newpassWord;

        public string NewPassWord
        {
            get { return newpassWord; }
            set { newpassWord = value;   }
        }
    }
}
