using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Maklak.Models;

namespace Maklak.Web.Controllers
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
            int y = Session["Y"] == null ? 1 : (int)Session["Y"];
            int x = Session["X"] == null ? 1 : (int)Session["X"];

            if (x != 1 || y != 1)
            {
                //RedirectToAction("IndexMain", "Home");
                //this.TransferToAction("IndexMain", "Home");
                
                //return RedirectToAction("IndexMain", "Home"); ;
            }

            return PartialView(model);
        }
    }
}