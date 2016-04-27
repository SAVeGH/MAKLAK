using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Maklak.Web.Extension;
using Maklak.Models;
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

            string actionName = actionDescriptor.ActionName;
            string controllerName = controllerDescriptor.ControllerName;

            string requestedKey = (string)Session["X"];
            
            string currentKey = SiteMapHelper.ActionControllerKey(actionName, controllerName);
            // для Search POST запросов ключ currentKey пустой
            if (!string.IsNullOrEmpty(currentKey) && !requestedKey.Equals(currentKey))
            {
                filterContext.Result = RedirectToAction(SiteMapHelper.ActionByKey(requestedKey), SiteMapHelper.ControllerByKey(requestedKey));
                return;
            }           

            base.OnActionExecuting(filterContext);
        }

        
    }
}