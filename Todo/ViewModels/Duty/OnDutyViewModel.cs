using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Dialogs;
using Todo.DragDrop.Models;
using Todo.IService;
using Todo.Models;

namespace Todo.ViewModels.Duty
{
    public class OnDutyViewModel:BindableBase
    {
        private readonly IDutyOrderService orderService;
        private readonly IDutyPlanService planService;
        private readonly IDutyTemplateService templateService;
        
        public OnDutyViewModel(IDutyOrderService dutyOrderService, IDutyPlanService dutyPlanService, IDutyTemplateService dutyTemplateService)
        {
            orderService=dutyOrderService;
            planService = dutyPlanService;
            templateService=dutyTemplateService;
            Initial();
        }

        #region Props
        private ObservableCollection<ShapeBase> _items = new ObservableCollection<ShapeBase>();
        public ObservableCollection<ShapeBase> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initial()
        {
            var nowdate = DateTime.Now;
            var currentDate = nowdate.ToString("yyyy-MM-dd");
            //当天是否有计划
            var plan = (planService.GetDataLists()).FirstOrDefault(s => s.PlanDate == currentDate);
            //加载模版
            var templats = templateService.GetSingle(1);
            if (templats != null &&plan!=null)
            { 
                var order = (orderService.GetDataLists()).FirstOrDefault(s => s.Id == plan.OrderId);
                var fromDate = Convert.ToDateTime($"{currentDate} {order.StartDate}");
                var endDate = Convert.ToDateTime($"{currentDate} {order.EndDate}");
                if (nowdate >= fromDate && nowdate <= endDate)
                {
                    var datas = JsonConvert.DeserializeObject<List<RectangleBase>>(templats.Content);
                    foreach (var data in datas)
                    {
                        if (!string.IsNullOrEmpty(data.FieldName))
                        {
                            object propertyValue = GetPropertyValue(plan, data.FieldName);
                            data.FieldValue = propertyValue?.ToString() ?? "";
                        }
                        Items.Add(data);
                    }

                }
                   
            }
            
        }

        public static object GetPropertyValue(object obj, string propertyName)
        {
            // 获取对象的类型
            Type type = obj.GetType();

            // 获取属性信息
            PropertyInfo propertyInfo = type.GetProperty(propertyName);

            // 检查属性是否存在且可访问
            if (propertyInfo != null && propertyInfo.CanRead)
            {
                // 获取属性值
                return propertyInfo.GetValue(obj, null);
            }
            else
            {
                // 属性不存在或不可访问时抛出异常或返回默认值
                throw new ArgumentException($"Property '{propertyName}' not found or not readable on object of type '{obj.GetType().Name}'.");
            }
        }
    }
}
