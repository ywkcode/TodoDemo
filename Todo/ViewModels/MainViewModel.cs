﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Models;
using Todo.Extensions;

namespace Todo.ViewModels
{
    public class MainViewModel:BindableBase
    {
        protected IRegionManager regionManager;
        private   IRegionNavigationJournal journal;
        public MainViewModel(IRegionManager regionManagerArg)
        {
            MenuBars=new ObservableCollection<MenuBar>();
            CreateMenuBar();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            regionManager=regionManagerArg;

            GoBackCommand = new DelegateCommand(() =>
            {
                if (journal!=null&&journal.CanGoBack)
                {
                    journal.GoBack();
                }
            });
            GoForwardCommand = new DelegateCommand(() => {
                if (journal != null && journal.CanGoForward)
                {
                    journal.GoForward();
                }
            });
        }

        //导航
        private void Navigate(MenuBar bar)
        {
            if (bar == null ||string.IsNullOrEmpty(bar.NameSpace))
            {
                return;
            }
            //回调
            regionManager.Regions[PrismManager.MainViewRegionName].RequestNavigate(bar.NameSpace, back =>
            {
                journal = back.Context.NavigationService.Journal;
            });

            
        }

        public DelegateCommand<MenuBar> NavigateCommand { get; set; }

        //上一步
        public DelegateCommand GoBackCommand { get; set; }

        //下一步
        public DelegateCommand GoForwardCommand { get; set; }

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