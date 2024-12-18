using Microsoft.EntityFrameworkCore;
using Prism.Ioc;
using System.Configuration;
using System.Data;
using System.Windows;
using Todo.Common;
using Todo.Common.Dialogs;
using Todo.Entity;
using Todo.IService;
using Todo.Service;
using Todo.ViewModels;
using Todo.ViewModels.Dialogs;
using Todo.ViewModels.Duty;
using Todo.Views;
using Todo.Views.Dialogs;
using Todo.Views.Duty;

namespace Todo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainView>();
        }

        /// <summary>
        /// 重写初始化
        /// </summary>
        protected override void OnInitialized()
        {
            var dialog = Container.Resolve<IDialogService>();
            //dialog.ShowDialog("LoginView", callback =>
            dialog.ShowDialog("TemplateView", callback =>
            {
                if (callback.Result != ButtonResult.OK)
                {
                    Environment.Exit(0);
                    return;
                }
            });
            var service=App.Current.MainWindow.DataContext as IConfigureService;
            if (service != null)
            {
                service.Confiure();
            }
            base.OnInitialized();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.GetContainer()
            //  .Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "webUrl"));
            //containerRegistry.GetContainer().RegisterInstance(@"http://localhost:3389/", serviceKey: "webUrl");
             
           
            var dbContextOptions = GetDbContextOptions(); 
            containerRegistry.RegisterInstance<DbContextOptions<ToDoDbContext>>(dbContextOptions);
            containerRegistry.Register<DbContext,ToDoDbContext>(); //  

            //注册实现
            containerRegistry.Register<ITodoLoginService, TodoLoginService>();
            containerRegistry.Register<IToDoService, ToDoService>();
            containerRegistry.Register<IDialogHostService, DialogHostService>();
            containerRegistry.Register<ILoginService, LoginService>();
            containerRegistry.Register<IDutyOrderService, DutyOrderService>();
            containerRegistry.Register<IDutyPlanService, DutyPlanService>();
            containerRegistry.Register<IDutyTemplateService, DutyTemplateService>();
            //注册弹窗
            containerRegistry.RegisterForNavigation<AddToDoView, AddToDoViewModel>();
            containerRegistry.RegisterForNavigation<MsgView, MsgViewModel>();
            containerRegistry.RegisterDialog<LoginView, LoginViewModel>();
            containerRegistry.RegisterDialog<TemplateView, TemplateViewModel>();

            containerRegistry.RegisterForNavigation<MessageView, MessageViewModel>(); //消息提示框
            containerRegistry.RegisterForNavigation<ColorPickerView, ColorPickerViewModel>(); //取色器
            //导航注册
            containerRegistry.RegisterForNavigation<OrderView, OrderViewModel>();
            containerRegistry.RegisterForNavigation<IndexView,IndexViewModel>();
            containerRegistry.RegisterForNavigation<MemoView,MemoViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView,SettingsViewModel>();
            containerRegistry.RegisterForNavigation<TodoView,TodoViewModel>(); 
            containerRegistry.RegisterForNavigation<SkinView, SkinViewModel>();
            containerRegistry.RegisterForNavigation<AboutView, AboutViewModel>();
            containerRegistry.RegisterForNavigation<OrderView,OrderViewModel>();
            containerRegistry.RegisterForNavigation<PlanView, PlanViewModel>();
            containerRegistry.RegisterForNavigation<TodayView, TodayViewModel>();
            containerRegistry.RegisterForNavigation<OnDutyView, OnDutyViewModel>();
            containerRegistry.RegisterForNavigation<TemplateSettingView, TemplateSettingViewModel>();
            //containerRegistry.RegisterForNavigation<TemplateView, TemplateViewModel>();
        }

        private DbContextOptions<ToDoDbContext> GetDbContextOptions()
        {
            // 这里应该是从配置文件、环境变量或其他配置源中获取连接字符串的逻辑
            var connectionString = "Data Source=to.db"; // 示例连接字符串
            return new DbContextOptionsBuilder<ToDoDbContext>()
                .UseSqlite(connectionString)
                .Options;
        }
    }

}
