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
            JobDetailImpl job = new JobDetailImpl("1Job", null, typeof(EmailReminderJob));

            ITrigger trigger = TriggerBuilder.Create()
                                                    .WithIdentity("trigger1", "group1")
                                                    .StartNow()
                                                    .WithSimpleSchedule(x => x
                                                        .WithIntervalInSeconds(60)
                                                        .RepeatForever())
                                                    .Build();


            scheduler.ScheduleJob(job, trigger);
        }
    }
}
