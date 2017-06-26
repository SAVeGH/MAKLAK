using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Maklak.Client.Models;

namespace Maklak.Client.Web.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {           

            return PartialView(model);
        }

        public ActionResult Register(LoginModel model)
        {

            return PartialView(model);
        }

        public ActionResult UserProfile(LoginModel model)
        {

            return PartialView(model);
        }
    }
}