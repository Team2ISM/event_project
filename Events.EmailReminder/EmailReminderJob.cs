using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quartz;
using Events.Business;
using Events.Business.Models;
using Events.Business.Interfaces;
using Events.Business.Classes;

namespace Events.EmailReminder
{
    public class EmailReminderJob : IJob
    {
        private RemindManager manager;

        private EmailRemindSender sender;

        string HostName;

        public enum Periods { Day = 1, Hour = 2 };

        public EmailReminderJob(RemindManager manager)
        {
            this.manager = manager;
            sender = new EmailRemindSender();
        }

        public void Execute(IJobExecutionContext context)
        {
            var date = DateTime.Now;
            var list = manager.GetListEventsToRemind();

            foreach (var evnt in list)
            {
                RemindSubscribers(evnt, date);
            }
        }

        private void RemindSubscribers(Event evnt, DateTime date)
        {
            IList<User> userList = manager.GetUsersToRemind(evnt.Id);
            IsReminded model = manager.GetIsRemindedModel(evnt.Id);
            if (model == null)
            {
                model = new IsReminded()
                {
                    EventId = evnt.Id
                };
            }
            Periods period = GetPeriodForRemind(evnt, date);

            switch (period)
            {
                case Periods.Day:
                    if (model.Day)
                    {
                        return;
                    }
                    model.Day = true;
                    break;
                case Periods.Hour:
                    if (model.Hour)
                    {
                        return;
                    }
                    model.Hour = true;
                    break;
                default:
                    return;
            }

            Task.Run(() => manager.SaveOrUpdateIsRemindedModel(model));

            foreach (var user in userList)
            {
                Task.Run(() => sender.SendEmailRemind(user.Email, evnt, period));
            }
        }

        private Periods GetPeriodForRemind(Event evnt, DateTime date)
        {
            if (evnt.FromDate < date.AddHours(1).AddSeconds(30) && evnt.FromDate > date.AddHours(1).AddSeconds(-30))
            {
                return Periods.Hour;
            }
            if (evnt.FromDate < date.AddDays(1).AddSeconds(30) && evnt.FromDate > date.AddDays(1).AddSeconds(-30))
            {
                return Periods.Day;
            }
            return 0;
        }
    }
}
