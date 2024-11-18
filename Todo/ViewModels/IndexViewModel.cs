using System.Collections.ObjectModel;
using Todo.Common.Models;

namespace Todo.ViewModels
{
    public class IndexViewModel : BindableBase
    {
        private readonly IDialogService dialogService;
        public DelegateCommand<string> ExecuteCommand { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IndexViewModel(IDialogService dialogService)
        {
            Title = "你好，当前是周一下午13:40";
            CreateTaskBars();
            CreateTodos();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            this.dialogService = dialogService;
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
            dialogService.ShowDialog("AddToDoView");
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
