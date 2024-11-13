using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using Todo.Common.Models;
using Todo.Extensions;

namespace Todo.ViewModels
{
    public class SettingsViewModel : BindableBase
    {
        protected IRegionManager regionManager;

       
        public DelegateCommand<MenuBar> NavigateCommand { get; set; }
        public SettingsViewModel(IRegionManager regionManagerArg)
        {
            CreateMenuBar();
            regionManager = regionManagerArg;
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
        }

        private void Navigate(MenuBar bar)
        {
            regionManager.Regions[PrismManager.SettingsViewRegionName].RequestNavigate(bar.NameSpace);
        }

        private ObservableCollection<MenuBar> menubars;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menubars; }
            set { menubars = value; RaisePropertyChanged(); }
        }

        void CreateMenuBar()
        {
            MenuBars=new ObservableCollection<MenuBar>();
            MenuBars.Add(new MenuBar() { Icon = "Palette", Title = "个性化", NameSpace = "SkinView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "系统设置", NameSpace = "TodoView" });
            MenuBars.Add(new MenuBar() { Icon = "Information", Title = "关于更多", NameSpace = "AboutView" });
        
        }
    }
}
