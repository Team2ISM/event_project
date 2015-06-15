using Events.Business.Models;
using System.Net.Mail;
using System.Net.Mime;
using Events.Business;
using Events.Business.Helpers;

namespace Events.EmailReminder
{
    class EmailRemindSender
    {
        public void SendEmailRemind(string email, Event evnt, EmailReminderJob.Deadlines period)
        {
            string periodName;
            switch (period)
            {
                case EmailReminderJob.Deadlines.Day:
                    periodName = "день";
                    break;
                case EmailReminderJob.Deadlines.Hour:
                    periodName = "час";
                    break;
                default:
                    return;
            }
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("team2project222@gmail.com");
            msg.To.Add(email);
            msg.Subject = "Напоминание";
            msg.IsBodyHtml = true;
            string body = FormEmailRemind(evnt, periodName);
            msg.Body = body;
            ContentType mimeType = new System.Net.Mime.ContentType("text/html");
            AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
            msg.AlternateViews.Add(alternate);
            msg.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Send(msg);
        }

        private string FormEmailRemind(Event evnt, string period)
        {
            string body = "<td>" +
                            "<h1>До события " + evnt.Title + " остался один " + period + "</h1>" +
                            "<p>Начало события: " + evnt.FromDate.Value.ToString("dd MMMM yyyy года, H:mm") + "</p>" +
                            "<table>" +
                            "<tr>" +
                            "<td class='padding'>" +
                            "<p><a href='" + EnvironmentInfo.Host + "/events/details/" + evnt.Id + "' class='btn-primary'>Перейти на страницу события</a></p>" +
                            "</td>" +
                            " </tr>" +
                            "</table>" +
                            "</td>";
            return body;
        }
    }
}
