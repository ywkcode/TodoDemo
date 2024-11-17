using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Models;
using Todo.Service;
using Todo.Shared.Dtos;
using Todo.Shared.Parameters;

namespace Todo.ViewModels
{
    public class TodoViewModel: BindableBase
    {
        private readonly IToDoService toDoService;
        public TodoViewModel(IToDoService toDoServiceArg)
        {
            toDoService = toDoServiceArg;
            CreateTodos();
            AddCommand = new DelegateCommand(Add);
        }
        private bool isRightDrawerOpen;

        public bool IsRightDrawerOpen
        {
            get { return isRightDrawerOpen; }
            set { isRightDrawerOpen = value; RaisePropertyChanged(); }
        }

        private void Add()
        {
            IsRightDrawerOpen=!IsRightDrawerOpen;
        }

        private ObservableCollection<ToDoDto> todoDtos;

        public ObservableCollection<ToDoDto> TodoDtos
        {
            get { return todoDtos; }
            set { todoDtos = value; RaisePropertyChanged(); }
        }

        async void CreateTodos()
        {
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
        }

        public  DelegateCommand AddCommand { get; set; }
    }
}
