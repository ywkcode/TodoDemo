using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Models;

namespace Todo.ViewModels
{
    public class TodoViewModel: BindableBase
    {

        public TodoViewModel()
        {
           
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

        private ObservableCollection<TodoDto> todoDtos;

        public ObservableCollection<TodoDto> TodoDtos
        {
            get { return todoDtos; }
            set { todoDtos = value; RaisePropertyChanged(); }
        }

        void CreateTodos()
        {
            TodoDtos = new ObservableCollection<TodoDto>();
            for (int i = 0; i < 10; i++)
            {
                TodoDtos.Add(new TodoDto() { Title = "待办" + i, Content = "正在处理..." });
            }
        }

        public  DelegateCommand AddCommand { get; set; }
    }
}
