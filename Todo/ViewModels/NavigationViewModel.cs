using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Extensions;

namespace Todo.ViewModels
{
    public class NavigationViewModel :BindableBase, INavigationAware
    {
        private readonly IContainerProvider container;
        private readonly IEventAggregator eventAggregator;
        public NavigationViewModel(IContainerProvider containerArg )
        {
            container = containerArg;
            eventAggregator = container.Resolve<IEventAggregator>();
        }
        /// <summary>
        /// 虚方法 可以实现或不实现
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
             
        }

        public void UpdateLoading(bool IsOpen)
        {
            eventAggregator.UpdateLoading(new Common.Event.UpdateModel()
            {
                IsOpen = IsOpen
            });
        }
    }
}
