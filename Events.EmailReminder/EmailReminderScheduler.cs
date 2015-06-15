using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace Events.EmailReminder
{
    public class EmailRemiderScheduler
    {
        public static void Start(IScheduler scheduler)
        {
            JobDetailImpl job = new JobDetailImpl("EmailReminderJob", null, typeof(EmailReminderJob));
            ITrigger trigger = TriggerBuilder.Create()
                                                    .WithIdentity("EmailReminderTrigger", "EmailReminderGroup")
                                                    .StartNow()
                                                    .WithSimpleSchedule(x => x
                                                        .WithIntervalInSeconds(StaticVariables.PeriodTriggerInSeconds)
                                                        .RepeatForever())
                                                    .Build();
            scheduler.ScheduleJob(job, trigger);
        }
    }
}
