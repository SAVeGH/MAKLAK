using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Maklak.Web.Extension;

namespace Maklak.Web.Controllers
{
    public class BaseController : Controller
    {
        

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;            

            int y = Session["Y"] == null ? 1 : (int)Session["Y"];
            int x = Session["X"] == null ? 1 : (int)Session["X"];

            if (x != 1 || y != 1)
            {
                filterContext.Result = RedirectToAction("IndexMain", "Home");
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        
    }
}