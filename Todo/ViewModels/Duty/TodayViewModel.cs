using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.IService;

namespace Todo.ViewModels.Duty
{
    public class TodayViewModel : BindableBase
    {
        private static Timer timer;
        private readonly IDutyOrderService orderService;
        private readonly IDutyPlanService planService;

        #region 属性
        #endregion
        public TodayViewModel(IDutyOrderService orderServiceArg,IDutyPlanService planServiceArg)
        {
            var timerCallback = new TimerCallback(DisplayTimeEvent);
            timer = new Timer(timerCallback, null, 0, 1000);
            orderService=orderServiceArg;
            planService=planServiceArg;
        }
        //回调逻辑
        private async  void DisplayTimeEvent(Object? stateInfo)
        {
            var nowdate = DateTime.Now;
            var currentDate = nowdate.ToString("yyyy-MM-dd");

            var plan =(await planService.GetDataLists()).FirstOrDefault(s=>s.PlanDate== currentDate);
        }
    }
}
