using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using System.Net;
using System.Net.Mail;
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
                var user = data.GetByMail(email);
                if (user != null && user.IsActive && isValid(email, password))
                {
                    FormsAuthentication.SetAuthCookie(email, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login or password is incorrect");
                    @ViewBag.Error = "Login or password is incorrect";
                }
            }
            return View();
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

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            if (ModelState.IsValid)
            {
                var user = data.GetByMail(email);
                if (user != null)
                {
                    var crypto = new SimpleCrypto.PBKDF2();

                    string newPassword = GeneratePassword();
                    
                    var encrPass = crypto.Compute(newPassword);

                    user.Password = encrPass;
                    user.PasswordSalt = crypto.Salt;

                    data.UpdateUser(user);

                    MailMessage msg = new MailMessage();

                    string body = user.Name + ", your password is:\n" + newPassword;

                    msg.From = new MailAddress("team2project222@gmail.com");
                    msg.To.Add(user.Email);
                    msg.Subject = "Remind password";
                    msg.Body = body;
                    msg.Priority = MailPriority.High;

                    SmtpClient client = new SmtpClient();

                    client.Send(msg);
                }
                else
                {
                    return RedirectToAction("NoSuchUser", "User");
                }
            }
            return View();
        }

        public ActionResult NoSuchUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var crypto = new SimpleCrypto.PBKDF2();

                    var encrPass = crypto.Compute(user.Password);

                    User newUser = AutoMapper.Mapper.Map<User>(user);
                    newUser.Password = encrPass;
                    newUser.PasswordSalt = crypto.Salt;
                    newUser.IsActive = false;

                    data.CreateUser(newUser);

                    MailMessage msg = new MailMessage();

                    string userMail = user.Email;
                    string activationLink = "http://localhost:7161/User/Activate/" + user.Id;
                    string body = "Dear " + user.Name + ", thank you for registration\n";
                    body += "To activate your account click on the link below\n";
                    body += activationLink;
                    msg.From = new MailAddress("team2project222@gmail.com");
                    msg.To.Add(user.Email);
                    msg.Subject = "Registration confirm";
                    msg.Body = body;
                    msg.Priority = MailPriority.High;

                    SmtpClient client = new SmtpClient();

                    client.Send(msg);
                    ViewBag.RegistrationSuccess = "Please, confirm your registration by follow the link on your mail";
                }
                catch (Exception ex)
                {
                    ViewBag.DuplicateMailError = "User with this email already registered";
                }
                User.IsInRole("role");
            }
            return View();

        }
       
        [HttpGet]
        public ActionResult Activate(string id)
        {
            var user = data.GetById(id);
            if (user != null && user.IsActive == false)
            {
                user.IsActive = true;
                data.UpdateUser(user);
                FormsAuthentication.SetAuthCookie(user.Email, false);
                UserViewModel model = AutoMapper.Mapper.Map<UserViewModel>(user);
                return RedirectToRoute("Welcome", "User");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }

        public ActionResult Welcome()
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

        private string GeneratePassword()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }

    }
}
