using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Todo.Entity;
using Todo.IService;
using Todo.Service;

namespace Todo.ViewModels.Duty
{
    public class PlanViewModel : BindableBase
    {
        private readonly IDutyPlanService planService;
        public PlanViewModel(IDutyPlanService dutyPlanService)
        {
            planService = dutyPlanService;
            AddPlanCommand = new DelegateCommand(AddPlan);
            DeleteCommand = new DelegateCommand<DutyPlan>(DeletePlan);
            SaveCommand = new DelegateCommand(SavePlan);
            InitData();
        }

        private void SavePlan()
        {
            
        }

        private void DeletePlan(DutyPlan plan)
        {
            Plans.Remove(plan);
            if (plan.Id > 0)
            {
                planService.DeletePlan(plan);
            }
        }

        private void AddPlan()
        {
            Plans.Add(new DutyPlan() { });
        }
        #region Property
        private ObservableCollection<DutyPlan> plans;
        public DelegateCommand AddPlanCommand { get; private set; }

        public DelegateCommand SaveCommand { get; private set; }

        public DelegateCommand<DutyPlan> DeleteCommand { get; private set; }

        public ObservableCollection<DutyPlan> Plans
        {
            get { return plans; }
            set { plans = value; RaisePropertyChanged(); }
        }
        #endregion


         void InitData()
        {
            Plans = new ObservableCollection<DutyPlan>();
            var results =  planService.GetDataLists();
            Plans.Clear();
            foreach (var item in results)
            {
                Plans.Add(item);
            }
        }
    }
}
