using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Models;

namespace Todo.ViewModels
{
    public class MainViewModel:BindableBase
    {
        public MainViewModel()
        {
            MenuBars=new ObservableCollection<MenuBar>();
            CreateMenuBar();
        }

        private ObservableCollection<MenuBar> menubars;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menubars; }
            set { menubars = value;RaisePropertyChanged(); }
        }

        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Home", Title = "首页", NameSpace = "IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookOutline", Title = "待办事项", NameSpace = "TodoView" });
            MenuBars.Add(new MenuBar() { Icon = "NotebookPlus", Title = "备忘录", NameSpace = "MemoView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "设置", NameSpace = "SettingsView" });
        }
    }
}
