using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Maklak.Client.Models;

namespace Maklak.Client.Web.Controllers
{
    public class HomeController : BaseController
    {
       
        // GET: Home
        // Модель создаётся и инициализируется автоматически в BaseModelBinder
        public ActionResult Index(BaseModel model)
        {
            Maklak.Client.Models.Helpers.ModelHelper.SetDefaults(model);

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