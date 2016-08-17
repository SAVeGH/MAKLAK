using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Maklak.Web.Extension;
using Maklak.Models;
using Maklak.Models.Helpers;
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

            IDictionary<string, object> actionParams = filterContext.ActionParameters;
            string modelKey = actionParams.Keys.FirstOrDefault(); // только один ключь тут
            BaseModel model = (BaseModel)actionParams[modelKey];

            string actionName = actionDescriptor.ActionName;
            string controllerName = controllerDescriptor.ControllerName;

            string requestedKey = SessionHelper.GetValue<string>(model.SID, "X");//(string)Session["X"];
            
            string currentKey = SiteMapHelper.ActionControllerKey(actionName, controllerName);
            // для Search POST запросов ключ currentKey пустой
            if (!string.IsNullOrEmpty(currentKey) && !requestedKey.Equals(currentKey))
            {
                filterContext.Result = RedirectToAction(SiteMapHelper.ActionByKey(requestedKey), SiteMapHelper.ControllerByKey(requestedKey),model);
                return;
            }           

            base.OnActionExecuting(filterContext);
        }

        
    }
}