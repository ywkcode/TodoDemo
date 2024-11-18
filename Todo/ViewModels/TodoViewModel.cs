using DryIoc;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Dialogs;
using Todo.Common.Models;
using Todo.Extensions;
using Todo.Service;
using Todo.Shared.Dtos;
using Todo.Shared.Parameters;

namespace Todo.ViewModels
{
    public class TodoViewModel: NavigationViewModel
    {
        private readonly IToDoService toDoService;
        private readonly IDialogHostService dialogHost;
        public TodoViewModel(IToDoService toDoServiceArg,IContainerProvider provider):base(provider)
        {
            toDoService = toDoServiceArg;
            //CreateTodos();
            ExecuteCommand = new DelegateCommand<string>(Execute);
            SelectedCommand = new DelegateCommand<ToDoDto>(Seleted);
            DeleteCommand = new DelegateCommand<ToDoDto>(Delete);
            dialogHost = provider.Resolve<IDialogHostService>(); //从容器中获取
        }

        private async void Delete(ToDoDto dto)
        {
            try
            {
                var dialogResult=await dialogHost.Question("温馨提示", $"确认删除待办事项:{dto.Title}?");
                if (dialogResult.Result != Prism.Dialogs.ButtonResult.OK) return;
                
                UpdateLoading(true);
                var deleteResult = await toDoService.DeleteAsync(dto.Id);
                if (deleteResult.Status)
                {
                    var model = TodoDtos.FirstOrDefault(t => t.Id.Equals(dto.Id));
                    if (model != null)
                        TodoDtos.Remove(model);
                }
            }
            finally
            {
                UpdateLoading(false);
            }
        }

        private void Execute(string obj)
        {
            switch (obj)
            {
                case "新增": Add(); break;
                case "查询": GetDataAsync(); break;
                case "保存": Save(); break;
            }
        }

        private  async void Seleted(ToDoDto obj)
        {
            UpdateLoading(true);
            var todoResult = await toDoService.GetFirstOfDefaultAsync(obj.Id);
            if (todoResult.Status)
            {
                CurrentToDo = todoResult.Result;
                IsRightDrawerOpen = true;//打开右侧窗口
            }
            UpdateLoading(false);
        }

        #region 属性
        private bool isRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

        private int selectedIndex;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 当前选中的待办事项
        /// </summary>
        private ToDoDto currentToDo;

        public ToDoDto CurrentToDo
        {
            get { return currentToDo; }
            set { currentToDo = value; RaisePropertyChanged(); }
        }


        private string search;

        public string Search
        {
            get { return search; }
            set { search = value; }
        }
        #endregion

        private void Add()
        {
            CurrentToDo = new ToDoDto();
            IsRightDrawerOpen=!IsRightDrawerOpen;
        }

        private ObservableCollection<ToDoDto> todoDtos;

        public ObservableCollection<ToDoDto> TodoDtos
        {
            get { return todoDtos; }
            set { todoDtos = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        async void GetDataListsAsync()
        {
            UpdateLoading(true);
            TodoDtos = new ObservableCollection<ToDoDto>();
             var results = await toDoService.GetAllAsync(new QueryParameter()
            {
                PageIndex = 0,
                PageSize = 100, 
            });
            if (results.Status) //查询成功
            {
                TodoDtos.Clear();
                foreach (var item in results.Result.Items)
                {
                    TodoDtos.Add(item);
                }
            }
            UpdateLoading(false);
        }
        async void GetDataAsync()
        {
            UpdateLoading(true);

            int? Status = SelectedIndex == 0 ? null : SelectedIndex == 2 ? 1 : 0;

            var todoResult = await toDoService.GetAllFilterAsync(new ToDoParameter()
            {
                PageIndex = 0,
                PageSize = 100,
                Search = Search,
                Status = Status
            });

            if (todoResult.Status)
            {
                TodoDtos.Clear();
                foreach (var item in todoResult.Result.Items)
                {
                    TodoDtos.Add(item);
                }
            }
            UpdateLoading(false);
        }

        //保存
        private async void Save()
        {
            if (string.IsNullOrWhiteSpace(CurrentToDo.Title) ||
                string.IsNullOrWhiteSpace(CurrentToDo.Content))
                return;

            UpdateLoading(true);

            try
            {
                if (CurrentToDo.Id > 0)
                {
                    var updateResult = await toDoService.UpdateAsync(CurrentToDo);
                    if (updateResult.Status)
                    {
                        var todo = TodoDtos.FirstOrDefault(t => t.Id == CurrentToDo.Id);
                        if (todo != null)
                        {
                            todo.Title = CurrentToDo.Title;
                            todo.Content = CurrentToDo.Content;
                            todo.Status = CurrentToDo.Status;
                        }
                    }
                    IsRightDrawerOpen = false;
                }
                else
                {
                    var addResult = await toDoService.AddAsync(CurrentToDo);
                    if (addResult.Status)
                    {
                        TodoDtos.Add(addResult.Result);
                        IsRightDrawerOpen = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                UpdateLoading(false);
            }
        }
        public  DelegateCommand<string> ExecuteCommand { get; private set;  }

        public DelegateCommand<ToDoDto> SelectedCommand { get; private set; }

        public DelegateCommand<ToDoDto> DeleteCommand { get; private set; }



        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            GetDataListsAsync();
        }
    }
}
