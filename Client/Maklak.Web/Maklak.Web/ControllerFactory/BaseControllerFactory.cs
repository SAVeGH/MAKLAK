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

            if(requestContext.RouteData.Values.Keys.Contains("SID"))
                formSID = (Guid)requestContext.RouteData.Values["SID"];//requestContext.HttpContext.Request.Form["SID"];
            
            Guid sID = formSID == Guid.Empty ? Guid.NewGuid() : formSID;

            BaseController controller = base.GetControllerInstance(requestContext, controllerType) as BaseController;//Activator.CreateInstance(controllerType, new[] { sID }) as Controller;

            controller.SID = sID;

            return controller;
        }
    }
}