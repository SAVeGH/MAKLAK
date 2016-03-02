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

            ActionDescriptor actionDescriptor = filterContext.ActionDescriptor;
            
            ControllerDescriptor controllerDescriptor = actionDescriptor.ControllerDescriptor;

            string aName = actionDescriptor.ActionName;
            string cName = controllerDescriptor.ControllerName;

            string requestKey = (string)Session["X"];



            //string controller = filterContext.Controller;

            //filterContext.Result = RedirectToAction("IndexMain", "Home");
                //return;
            

            base.OnActionExecuting(filterContext);
        }

        
    }
}