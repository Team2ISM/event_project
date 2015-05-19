using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Users.Business;
using Users.Business.Interfaces;
using Users.NHibernateDataProvider;
using team2project.Models;

namespace team2project.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        IUserDataProvider data;

        public UserController(IUserDataProvider data)
        {
            this.data = data;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                if (isValid(email, password))
                {
                    FormsAuthentication.SetAuthCookie(email, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login is incorrect");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var crypto = new SimpleCrypto.PBKDF2();

                var encrPass = crypto.Compute(user.Password);

                User newUser = AutoMapper.Mapper.Map<User>(user);
                newUser.Password = encrPass;
                newUser.PasswordSalt = crypto.Salt;
                newUser.IsActive = false;

                data.CreateUser(newUser);

                //MailMessage msg = new MailMessage();

                //msg.From = new MailAddress("team2project222@gmail.com");
                //msg.To.Add("diabolikkys@gmail.com");
                //msg.Subject = "test";
                //msg.Body = "Test Content";
                //msg.Priority = MailPriority.High;

                //SmtpClient client = new SmtpClient();

                ////client.Credentials = new NetworkCredential("team2project222@gmail.com", "project2team", "smtp.gmail.com");
                ////client.Host = "smtp.gmail.com";
                ////client.Port = 25;
                ////client.DeliveryMethod = SmtpDeliveryMethod.Network;
                ////client.EnableSsl = true;
                //client.UseDefaultCredentials = false;

                //client.Send(msg);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(string password, string newPassword)
        {
            if (ModelState.IsValid)
            {
                var crypto = new SimpleCrypto.PBKDF2();

                var mail = User.Identity.Name;
                User user = data.GetByMail(mail);

                if (user.Password == crypto.Compute(password, user.PasswordSalt))
                {
                    var encrPass = crypto.Compute(newPassword);

                    user.Password = encrPass;
                    user.PasswordSalt = crypto.Salt;

                    data.UpdateUser(user);
                }
            }
            return View();
        }

        private bool isValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool isValid = false;

            User user = data.GetByMail(email);
            if (user != null)
            {
                if (user.Password == crypto.Compute(password, user.PasswordSalt))
                {
                    isValid = true;
                }
            }

            return isValid;
        }

    }
}
