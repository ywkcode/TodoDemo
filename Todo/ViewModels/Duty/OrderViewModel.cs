using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Entity;
using Todo.IService;

namespace Todo.ViewModels.Duty
{
     public class OrderViewModel:BindableBase
    {
        private readonly IDutyOrderService dutyOrderService;
        public OrderViewModel(IDutyOrderService dutyOrderServiceArg)
        {
            dutyOrderService = dutyOrderServiceArg;
            InitData();
        }
        private ObservableCollection<DutyOrder> orders;

        public ObservableCollection<DutyOrder> Orders
        {
            get { return orders; }
            set { orders = value; RaisePropertyChanged(); }
        }
        async void InitData()
        {
            Orders = new ObservableCollection<DutyOrder>();
            var results =await dutyOrderService.GetDataLists();
            Orders.Clear();
            foreach (var item in results)
            {
                Orders.Add(item);
            }
        }

    }
}
