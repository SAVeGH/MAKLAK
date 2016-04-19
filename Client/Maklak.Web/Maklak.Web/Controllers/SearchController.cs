using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maklak.Web.Controllers
{
    public class SearchController : Controller
    {
        // GET: Some
        public ActionResult Search(Maklak.Models.LoginModel model)
        {
            return PartialView(model);
        }
    }
}