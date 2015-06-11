using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Mail;
using System.Net.Mime;

using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using Events.Business;
using Events.Business.Interfaces;
using Events.Business.Models;


namespace Events.Business.Classes
{
    public class UserManager : BaseManager
    {
        IUserDataProvider userDataProvider;

        public string userName;

        protected override string Name { get; set; }

        public UserManager(IUserDataProvider userDataProvider, ICacheManager cacheManager)
        {
            Name = "Users";
            this.userDataProvider = userDataProvider;
            this.cacheManager = cacheManager;
        }

        public IList<User> GetAllUsers()
        {
            return FromCache<IList<User>>("AllUsersList",
                    ( ) => {
                        return userDataProvider.GetAllUsers();
                    });            
        }

        public User GetById(string id)
        {
            return cacheManager.FromCache<User>("id:" + id,
                 ( ) => {
                     return userDataProvider.GetById(id);
                 });
        }

        public User GetByEmail(string mail)
        {
            return cacheManager.FromCache<User>("email:" + mail,
                 ( ) => {
                     return userDataProvider.GetByMail(mail);
                 });            
        }

        public void CreateUser(User user)
        {
            userDataProvider.CreateUser(user);
            ToCache<User>("id:" + user.Id,
                ( ) => {
                    return user;
                });
            ToCache<User>("email:" + user.Email,
                ( ) => {
                    return user;
                });
            ClearCache();
        }

        public void DeleteUser(User user)
        {
            userDataProvider.DeleteUser(user);
            RemoveFromCache("id:" + user.Id);
            RemoveFromCache("email:" + user.Email);
            ClearCache();            
        }

        public void UpdateUser(User user)
        {
            userDataProvider.UpdateUser(user);
            RemoveFromCache("id:" + user.Id);
            ToCache<User>("id:" + user.Id,
                ( ) => {
                    return user;
                });
            ToCache<User>("email:" + user.Email,
                ( ) => {
                    return user;
                });
            ClearCache();          
        }

        public string GetFullName(string email)
        {
            return FromCache<string>("UserName:" + email,
                () =>
                {
                    return userDataProvider.GetFullName(email);
                });
        }

        public void ChangePassword(User user, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            var encrPass = crypto.Compute(password);

            user.Password = encrPass;
            user.PasswordSalt = crypto.Salt;

            UpdateUser(user);
        }

        public void RegisterUser(User user)
        {
            var crypto = new SimpleCrypto.PBKDF2();

            var encrPass = crypto.Compute(user.Password);

            user.Password = encrPass;
            user.PasswordSalt = crypto.Salt;
            user.IsActive = false;

            CreateUser(user);
        }

        public void SendNewPassword(User user)
        {
            var crypto = new SimpleCrypto.PBKDF2();

            string newPassword = Guid.NewGuid().ToString().Substring(0, 8); ;

            var encrPass = crypto.Compute(newPassword);

            user.Password = encrPass;
            user.PasswordSalt = crypto.Salt;

            UpdateUser(user);

            MailMessage msg = new MailMessage();

            string body = user.Name + ", ваш пароль: \n" + newPassword;
            string subject = "Новый пароль";

            EmailSender(user.Email, body, subject);
        }

        public void EmailSender(string userEmail, string body, string subject)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("team2project222@gmail.com");
            msg.To.Add(userEmail);
            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;

            ContentType mimeType = new System.Net.Mime.ContentType("text/html");
            AlternateView alternate = AlternateView.CreateAlternateViewFromString(body, mimeType);
            msg.AlternateViews.Add(alternate);

            msg.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient();

            client.Send(msg);
        }
    }
}
