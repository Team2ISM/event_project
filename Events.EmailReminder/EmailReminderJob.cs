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

        private DateTime timeNow;

        public enum Deadlines { NotRemind, Day, Hour };

        public EmailReminderJob(RemindManager manager)
        {
            this.manager = manager;
            sender = new EmailRemindSender();
        }

        public void Execute(IJobExecutionContext context)
        {
            timeNow = DateTime.Now;
            var list = manager.GetListEventsToRemind();

            foreach (var evnt in list)
            {
                RemindSubscribers(evnt);
            }
        }

        private void RemindSubscribers(Event evnt)
        {
            Deadlines deadline = GetPeriodForRemind(evnt);
            if (deadline == Deadlines.NotRemind)
            {
                return;
            }

            IList<User> userList = manager.GetUsersToRemind(evnt.Id);
            RemindModel model = manager.GetIsRemindedModel(evnt.Id);
            if (model == null)
            {
                model = new RemindModel()
                {
                    EventId = evnt.Id
                };
            }
            else 
            {
                switch (deadline)
                {
                    case Deadlines.Day:
                        if (model.Day)
                        {
                            return;
                        }
                        model.Day = true;
                        break;
                    case Deadlines.Hour:
                        if (model.Hour)
                        {
                            return;
                        }
                        model.Hour = true;
                        break;
                    default:
                        return;
                }
            }

            Task.Run(() => manager.SaveOrUpdateIsRemindedModel(model));

            foreach (var user in userList)
            {
                Task.Run(() => sender.SendEmailRemind(user.Email, evnt, deadline));
            }
        }

        private Deadlines GetPeriodForRemind(Event evnt)
        {
            if (evnt.FromDate < timeNow.AddHours(1).AddSeconds(30) && evnt.FromDate > timeNow.AddHours(1).AddSeconds(-30))
            {
                return Deadlines.Hour;
            }
            if (evnt.FromDate < timeNow.AddDays(1).AddSeconds(30) && evnt.FromDate > timeNow.AddDays(1).AddSeconds(-30))
            {
                return Deadlines.Day;
            }
            return 0;
        }
    }
}
