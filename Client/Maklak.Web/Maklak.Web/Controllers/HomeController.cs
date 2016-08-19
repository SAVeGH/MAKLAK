using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Maklak.Models;

namespace Maklak.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //BaseModel model = new BaseModel();

            //model.Action = TabModelHelper.DefaultAction;
            //model.Controller = TabModelHelper.DefaultController;
            //model.GenerateSID(); // генерируем уникальный SID для страницы
            BaseModel model = Maklak.Models.Helpers.ModelHelper.CreateBaseModel();
            return View(model);
        }

        public ActionResult IndexMain(BaseModel model)
        {
            return PartialView("IndexMain",model);
        }

        [HttpPost]
        public ActionResult IndexPost(BaseModel model)
        {
            return PartialView("IndexMain", model);
        }
    }
}