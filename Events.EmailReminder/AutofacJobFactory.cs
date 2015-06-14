using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.ComponentModel;
using Autofac;

namespace Events.EmailReminder
{
    public class AutofacJobFactory : IJobFactory
    {
        private readonly IComponentContext _container;

        public AutofacJobFactory(IComponentContext container)
        {
            _container = container;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return (IJob)(_container.Resolve(bundle.JobDetail.JobType));
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
