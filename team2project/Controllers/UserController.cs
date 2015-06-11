using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Claims;
using System.Threading;
using team2project.Helpers;
using System.Net;
using System.Net.Mime;
using System.Net.Mail;
using Events.Business;
using Events.Business.Classes;
using Events.Business.Models;
using Events.Business.Interfaces;
using Events.NHibernateDataProvider;
using Events.Business.Helpers;
using team2project.Models;
using Newtonsoft.Json;
using System.IO;

namespace team2project.Controllers
{
    public class UserController : Controller
    {
        UserManager userManager;
        EventManager eventManager;
        CitiesManager cityManager;


        public UserController(UserManager userManager, EventManager eventManager, CitiesManager citiesManager)
        {
            this.userManager = userManager;
            this.eventManager = eventManager;
            this.cityManager = citiesManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl))
            {
                if (!User.IsInRole("Admin") && User.Identity.IsAuthenticated)
                {
                    return View("GenericError", ResponseMessages.AccessDenied);
                }
                else if (User.Identity.IsAuthenticated)
                {
                    return Redirect(returnUrl);
                }
                ViewBag.ReturnUrl = Server.UrlEncode(returnUrl);
            }
            
            else if (User.Identity.IsAuthenticated)
            {
                return RedirectToRoute("EventsList", new { period = PeriodStates.Anytime });
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = userManager.GetByEmail(email);
            if (user != null && userManager.isValid(email, password))
            {
                if (user.IsActive == true)
                {
                    SignIn(user);

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(Server.UrlDecode(returnUrl));
                    }
                    else
                    {
                        return RedirectToRoute("EventsList", new { period = PeriodStates.Anytime });
                    }
                }
                else
                {
                    ViewBag.Mail = email;
                    return View("UnconfirmedUser");
                }
            }
            else
            {
                ModelState.AddModelError("", "Неправильный e-mail или пароль");
            }
            return View();
        }

        [HttpPost]
        public ActionResult UnconfirmedUser(string email)
        {
            var user = userManager.GetByEmail(email);
            if (user != null && user.IsActive == false)
            {
                SendActivationLink(AutoMapper.Mapper.Map<UserViewModel>(user));
                return RedirectToAction("ConfirmRegistration", "User");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ConfirmRegistration()
        {
            return View();
        }

        public ActionResult Logout()
        {
            SignOut();
            return RedirectToRoute("EventsList", new { period = PeriodStates.Anytime });
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToRoute("EventsList", new { period = PeriodStates.Anytime });
            }
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            var user = userManager.GetByEmail(email);
            if (user != null)
            {
               userManager.SendNewPassword(user);
            }
            return RedirectToAction("ThankYouPage", "User");
        }

        public ActionResult ThankYouPage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToRoute("EventsList", new { period = PeriodStates.Anytime });
            }
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var userExist = userManager.GetByEmail(user.Email);
                if (userExist == null)
                {
                    var mappedUser = AutoMapper.Mapper.Map<User>(user);
                    mappedUser.LocationId = user.LocationId;
                    userManager.RegisterUser(mappedUser);
                    SendActivationLink(user);

                    return RedirectToAction("ConfirmRegistration", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с такой почтой уже зарегистрирован");                   
                }
            }
            return View();

        }

        [HttpGet]
        public ActionResult Activate(string id)
        {
            var user = userManager.GetById(id);
            if (user != null && user.IsActive == false)
            {
                user.IsActive = true;
                userManager.UpdateUser(user);                
                SignIn(user);
                return RedirectToRoute("Welcome", "User");
            }
            return RedirectToRoute("EventsList", new { period = PeriodStates.Anytime });
        }

        public ActionResult Welcome()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(string oldPassword, string password)
        {
            if (ModelState.IsValid)
            {
                var crypto = new SimpleCrypto.PBKDF2();

                var mail = User.Identity.Name;
                User user = userManager.GetByEmail(mail);

                if (user.Password == crypto.Compute(oldPassword, user.PasswordSalt))
                {
                    userManager.ChangePassword(user, password);
                    ViewBag.PasswordSuccess = "Пароль изменен";
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный старый пароль");
                }
            }
            return View();
        }



        private void SignIn(User user)
        {
            FormsAuthentication.SetAuthCookie(user.Email, false);

            var city = cityManager.GetById(user.LocationId);
            string userData = "Name:" + user.Name + ":Surname:" + user.Surname + ":Location:" + (city != null ? city.Name : "Default");
            userData = HttpUtility.UrlEncode(userData);
            var json = JsonConvert.SerializeObject(userData);

            var userCookie = new HttpCookie("user", json);
            userCookie.Expires.AddDays(365);
            HttpContext.Response.SetCookie(userCookie);
        }

        private void SignOut()
        {
            FormsAuthentication.SignOut();

            if (Request.Cookies["user"] != null)
            {
                var user = new HttpCookie("user")
                {
                    Expires = DateTime.Now.AddDays(-1),
                    Value = null
                };
                Response.Cookies.Add(user);
            }
        }

        private void SendActivationLink(UserViewModel user)
        {
            string authority = Request.Url.Authority;
            string scheme = Request.Url.Scheme;
            string activationLink = scheme + "://" + authority + Url.Action("Activate", new { controller = "User", action = "Activate", id = user.Id });
            string body = user.Name + ", спасибо за регистрацию\n";
            body += "Для активации аккаунта перейдите по ссылке\n" + activationLink;
            string subject = "Подтверждение регистрации";
            string newBody = this.RenderPartialViewToString("email", activationLink);
            userManager.EmailSender(user.Email, newBody, subject);
        }  

    }
}
