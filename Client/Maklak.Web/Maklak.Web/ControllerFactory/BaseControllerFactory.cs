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
                ViewContext parentContext = requestContext.RouteData.DataTokens["ParentActionViewContext"] as ViewContext;

                BaseController parentController = parentContext.Controller as BaseController;

                formSID = parentController.SID;
            }
            else
            {
                string formValue = requestContext.HttpContext.Request.Form["SID"];

                if (!string.IsNullOrEmpty(formValue))
                    formSID = Guid.Parse(formValue);
            }           
            
            Guid sID = formSID == Guid.Empty ? Guid.NewGuid() : formSID;

            BaseController controller = base.GetControllerInstance(requestContext, controllerType) as BaseController;

            controller.SID = sID;

            return controller;
        }
    }
}