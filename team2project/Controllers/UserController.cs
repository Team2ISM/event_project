using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Claims;
using System.Threading;

using System.Net;
using System.Net.Mime;
using System.Net.Mail;
using Events.Business;
using Events.Business.Classes;
using Events.Business.Models;
using Events.Business.Interfaces;
using Events.NHibernateDataProvider;
using team2project.Models;
using Newtonsoft.Json;
using System.IO;

namespace team2project.Controllers
{
    public abstract class MyBaseController : Controller
    {
        protected string RenderPartialViewToString(string viewName, object model) {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter()) {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }

    public class UserController : MyBaseController
    {
        //
        // GET: /User/

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
                ViewBag.ReturnUrl = Server.UrlEncode(returnUrl);
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

            var user = userManager.GetByMail(email);
            if (user != null && isValid(email, password))
            {
                if (user.IsActive == true)
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);

                    string userData = "Name:" + user.Name + ":Surname:" + user.Surname + ":Location:" + user.Location;
                    var json = JsonConvert.SerializeObject(userData);

                    var userCookie = new HttpCookie("user", json);
                    userCookie.Expires.AddDays(365);
                    HttpContext.Response.SetCookie(userCookie);

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(Server.UrlDecode(returnUrl));
                    }
                    else
                    {
                        return RedirectToAction("Index", "Event");
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
            var user = userManager.GetByMail(email);
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
            return RedirectToAction("Index", "Event");
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
                var user = userManager.GetByMail(email);
                if (user != null)
                {
                    userManager.ForgotPassword(user);
                }
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
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var userExist = userManager.GetByMail(user.Email);
                if (userExist == null)
                {
                    userManager.RegisterUser(AutoMapper.Mapper.Map<User>(user));
                    SendActivationLink(user);

                    return RedirectToAction("ConfirmRegistration", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с такой почтой уже зарегистрирован");                   
                }
            }
            return View(new UserViewModel());

        }

        [HttpGet]
        public ActionResult Activate(string id)
        {
            var user = userManager.GetById(id);
            if (user != null && user.IsActive == false)
            {
                user.IsActive = true;
                userManager.UpdateUser(user);
                FormsAuthentication.SetAuthCookie(user.Email, false);

                string userData = "Name:" + user.Name + ":Surname:" + user.Surname + ":Location:" + user.Location;
                var json = JsonConvert.SerializeObject(userData);

                var userCookie = new HttpCookie("user", json);
                userCookie.Expires.AddDays(365);
                HttpContext.Response.SetCookie(userCookie);

                return RedirectToRoute("Welcome", "User");
            }
            return RedirectToAction("Index", "Event");
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
                User user = userManager.GetByMail(mail);

                if (user.Password == crypto.Compute(oldPassword, user.PasswordSalt))
                {
                    userManager.ChangePassword(user, password);
                    ViewBag.PasswordSuccess = "Пароль изменен";
                }
                else
                {
                    ViewBag.OldPasswordError = "Неправильный старый пароль";
                }
            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult MyPastEvents()
        {
            IList<Event> events = eventManager.GetAuthorPastEvents(User.Identity.Name);
            List<EventViewModel> eventsModels = AutoMapper.Mapper.Map<List<EventViewModel>>(events);
            foreach (var ev in eventsModels)
            {
                ev.Location = cityManager.GetById(Convert.ToInt32(ev.LocationId)).Name;
            }
            return View(eventsModels);
        }

        [Authorize]
        [HttpGet]
        public ActionResult MyFutureEvents()
        {
            IList<Event> events = eventManager.GetAuthorFutureEvents(User.Identity.Name);
            List<EventViewModel> eventsModels = AutoMapper.Mapper.Map<List<EventViewModel>>(events);
            foreach (var ev in eventsModels)
            {
                ev.Location = cityManager.GetById(Convert.ToInt32(ev.LocationId)).Name;
            }
            return View(eventsModels);
        }

        private bool isValid(string email, string password)
        {
            var crypto = new SimpleCrypto.PBKDF2();
            bool isValid = false;

            User user = userManager.GetByMail(email);
            if (user != null)
            {
                if (user.Password == crypto.Compute(password, user.PasswordSalt))
                {
                    isValid = true;
                }
            }

            return isValid;
        }



        private void SendActivationLink(UserViewModel user)
        {
            string authority = Request.Url.Authority;
            string scheme = Request.Url.Scheme;
            string activationLink = scheme + "://" + authority + Url.Action("Activate", new { controller = "User", action = "Activate", id = user.Id });

            string body = user.Name + ", спасибо за регистрацию\n";
            body += "Для активации аккаунта перейдите по ссылке\n" + activationLink;
            string subject = "Подтверждение регистрации";
            string newBody = RenderPartialViewToString("email", activationLink);
            userManager.EmailSender(user.Email, newBody, subject);
        }  

    }
}
