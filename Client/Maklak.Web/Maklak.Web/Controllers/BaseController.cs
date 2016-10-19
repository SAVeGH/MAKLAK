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
        public Guid SID { get; set; }

        

        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    // только для запросов верхнего уровня
        //    if (filterContext.IsChildAction)
        //        return;

        //    ActionDescriptor actionDescriptor = filterContext.ActionDescriptor;
            
        //    ControllerDescriptor controllerDescriptor = actionDescriptor.ControllerDescriptor;

        //    IDictionary<string, object> actionParams = filterContext.ActionParameters;
        //    string modelKey = actionParams.Keys.FirstOrDefault(); // только один ключь тут


        //    if (string.IsNullOrEmpty(modelKey))
        //        return;

        //    BaseModel model = (BaseModel)actionParams[modelKey];  // модель содержит GUID страницы

        //    string actionName = actionDescriptor.ActionName;
        //    string controllerName = controllerDescriptor.ControllerName;

        //    string requestedKey = SessionHelper.GetValue<string>(model.SID, "X");
            
        //    string currentKey = SiteMapHelper.ActionControllerKey(actionName, controllerName);
        //    // для Search POST запросов ключ currentKey пустой
        //    // определяем отлчается ли запрошенный ключ от текущего. Если да - то переходим на страницу с новым ключом.
        //    if (!string.IsNullOrEmpty(currentKey) && !requestedKey.Equals(currentKey))
        //    {
        //        // рекурсивный заход в BaseController OnActionExecuting
        //        filterContext.Result = RedirectToAction(SiteMapHelper.ActionByKey(requestedKey), SiteMapHelper.ControllerByKey(requestedKey), model);
        //        return;
        //    }           

        //    base.OnActionExecuting(filterContext);
        //}

        
    }
}