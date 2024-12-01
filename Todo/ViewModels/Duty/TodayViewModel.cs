using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.IService;
using Todo.Models;

namespace Todo.ViewModels.Duty
{
    public class TodayViewModel : BindableBase
    {
        private static Timer timer; //计时器刷新
        private readonly IDutyOrderService orderService;
        private readonly IDutyPlanService planService;

        #region 属性


        private TodayDtoInfo todayData;

        public TodayDtoInfo TodayData
        {
            get { return todayData; }
            set { todayData = value; RaisePropertyChanged(); }
        }

        
        #endregion
        public TodayViewModel(IDutyOrderService orderServiceArg,IDutyPlanService planServiceArg)
        {
            
            orderService=orderServiceArg;
            planService=planServiceArg;

            TodayData = new TodayDtoInfo();
            var timerCallback = new TimerCallback(DisplayTimeEvent);
            timer = new Timer(timerCallback, null, 0, 60000);
        }
        //回调逻辑
        private   void DisplayTimeEvent(Object? stateInfo)
        {
            var nowdate = DateTime.Now;
            var currentDate = nowdate.ToString("yyyy-MM-dd");
            //当天是否有计划
            var plan =( planService.GetDataLists()).FirstOrDefault(s=>s.PlanDate== currentDate);

            if (plan != null)
            {
                var order = ( orderService.GetDataLists()).FirstOrDefault(s => s.Id == plan.OrderId);
                if (order != null)
                {
                    var fromDate = Convert.ToDateTime($"{currentDate} {order.StartDate}");
                    var endDate = Convert.ToDateTime($"{currentDate} {order.EndDate}");
                    if (nowdate >= fromDate && nowdate <= endDate)
                    {
                        TodayData.Title_Head = "今日值班安排";
                        TodayData.Title_End = "维码科技";
                        TodayData.CurrentDate = nowdate.ToString("yyyy年MM月dd日 时间 HH:mm");
                        TodayData.Leader = $"带班领导 {plan.Leader}";
                        TodayData.LeaderTel= $"联系电话 {plan.LeaderTel}";
                        TodayData.Dutyer = $"值班人员 {plan.Dutyer}";
                        TodayData.DutyerTel = $"联系电话 {plan.DutyerTel}"; 
                    }
                }
            }
        }
    }

    public class TodayDtoInfo: BindableBase
    {
        public string? title_header;
        public string? Title_Head
        {
            get { return title_header; }
            set { title_header = value; RaisePropertyChanged(); }
        }
        public string? currentdate;
        public string?CurrentDate 
        {
            get { return currentdate; }
            set { currentdate = value; RaisePropertyChanged(); }
        }

        public string? leader;
        public string? Leader
        {
            get { return leader; }
            set { leader = value; RaisePropertyChanged(); }
        }
        public string? leadertel;
        public string? LeaderTel
        {
            get { return leadertel; }
            set { leadertel = value; RaisePropertyChanged(); }
        }

        

        public string? dutyer;
        public string? Dutyer
        {
            get { return dutyer; }
            set { dutyer = value; RaisePropertyChanged(); }
        }

        public string? dutyertel;
        public string? DutyerTel
        {
            get { return dutyertel; }
            set { dutyertel = value; RaisePropertyChanged(); }
        }
        public string? title_end;
        public string? Title_End
        {
            get { return title_end; }
            set { title_end = value; RaisePropertyChanged(); }
        }
         
    }
}
