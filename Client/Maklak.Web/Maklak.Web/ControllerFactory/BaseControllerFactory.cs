using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maklak.Web.Controllers;

namespace Maklak.Web.ControllerFactory
{
    public class BaseControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            Guid formSID = Guid.Empty;

            if (requestContext.RouteData.DataTokens.Count > 0 && requestContext.RouteData.DataTokens.Keys.Contains("ParentActionViewContext"))
            {
                // сюда заходит при выполнении дочернего запроса методом GET
                ViewContext parentContext = requestContext.RouteData.DataTokens["ParentActionViewContext"] as ViewContext;

                BaseController parentController = parentContext.Controller as BaseController;

                formSID = parentController.SID;
            }
            else
            {
                string formValue = string.Empty;

                if (requestContext.HttpContext.Request.Params.AllKeys.Contains("SID"))
                    formValue = requestContext.HttpContext.Request.Params["SID"];
                
                if (!string.IsNullOrEmpty(formValue))
                    formSID = Guid.Parse(formValue);
            }           
            
            Guid sID = formSID == Guid.Empty ? Guid.NewGuid() : formSID;

            BaseController controller = base.GetControllerInstance(requestContext, controllerType) as BaseController;

            controller.SID = sID; // все контроллеры наследники получают SID при создании

            return controller;
        }
    }
}