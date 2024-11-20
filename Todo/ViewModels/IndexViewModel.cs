using System.Collections.ObjectModel;
using System.Windows.Threading;
using Todo.Common.Dialogs;
using Todo.Common.Models;
using Todo.Common.Session;

namespace Todo.ViewModels
{
    public class IndexViewModel : BindableBase
    {
        //private readonly IDialogService dialogService;

        private DispatcherTimer _timer;
        //自定义的弹窗
        private readonly IDialogHostService dialogService;
        public DelegateCommand<string> ExecuteCommand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IndexViewModel(IDialogHostService dialogService)
        {
            Title = $"你好，{AppSession.UserName}";
            CreateTaskBars();
            CreateTodos();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.dialogService = dialogService;
            InitTime();


        }
        private void InitTime()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);//更新频率设为每秒
            _timer.Tick +=Time_Tick;
            _timer.Start();
            updateTime();
        }

        private void Time_Tick(object? sender, EventArgs e)
        {
            updateTime();
        }

        private void updateTime()
        {
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
        }
        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增待办": AddToDo(); break;
                default:break;
            }
        }
        void AddToDo()
        {
            dialogService.ShowDialog("AddToDoView",null);
        }
        #region 属性
        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<TaskBar> taskBars;

        public ObservableCollection<TaskBar> TaskBars
        {
            get { return taskBars; }
            set { taskBars = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<TodoDto> todoDtos;
        

        public ObservableCollection<TodoDto> TodoDtos
        {
            get { return todoDtos; }
            set { todoDtos = value; RaisePropertyChanged(); }
        }

        private string _currentTime;
        public string CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
                RaisePropertyChanged();
            }
        }
        #endregion


        void CreateTaskBars()
        {

            TaskBars = new ObservableCollection<TaskBar>();
            TaskBars.Add(new TaskBar() { Icon = "ClockFast", Title = "汇总", Color = "#FF0CA0FF", Target = "ToDoView",Content="9"});
            TaskBars.Add(new TaskBar() { Icon = "ClockCheckOutline", Title = "已完成", Color = "#FF1ECA3A", Target = "ToDoView", Content = "9" });
            TaskBars.Add(new TaskBar() { Icon = "ChartLineVariant", Title = "完成比例", Color = "#FF02C6DC", Target = "", Content = "9" });
            TaskBars.Add(new TaskBar() { Icon = "PlaylistStar", Title = "备忘录", Color = "#FFFFA000", Target = "MemoView", Content = "9" });
        }

        void CreateTodos()
        {
            TodoDtos = new ObservableCollection<TodoDto>();
            for(int i=0;i<10;i++)
            {
                TodoDtos.Add(new TodoDto() { Title="待办"+i,Content="正在处理..."});
            }
        }
    }
}
