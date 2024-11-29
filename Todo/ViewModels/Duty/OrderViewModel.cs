using Prism.Commands;
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
            AddOrderCommand = new DelegateCommand(AddOrder);
            DeleteCommand = new DelegateCommand<DutyOrder>(DeleteOrder);
            SaveCommand = new DelegateCommand(SaveOrder);
            InitData();
        }

        private void SaveOrder()
        {
            var datas = dutyOrderService.GetDataLists();
            foreach (var model in Orders)
            { 
            
            }
        }

        private void DeleteOrder(DutyOrder obj)
        {

            orders.Remove(obj);
            if (obj.Id > 0)
            {
                dutyOrderService.DeleteOrder(obj);
            }  
        }
        /// <summary>
        /// 新增一行
        /// </summary>
        private void AddOrder()
        {
            Orders.Add(new DutyOrder() { });
        }

        #region Property
        private ObservableCollection<DutyOrder> orders; 
        public DelegateCommand AddOrderCommand { get; private set; }

        public DelegateCommand SaveCommand { get; private set; }

        public DelegateCommand<DutyOrder> DeleteCommand { get; private set; }
        #endregion
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
