using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Maklak.Client.Web.Extension
{
    public static class ControllerExtension
    {
        public static ActionResult TransferToAction(this Controller ctrl, string controller, string action)
        {
            RouteValueDictionary dict = new RouteValueDictionary();
            dict.Add("controller", controller);
            dict.Add("action", action);
            TransferToRouteResult result = new TransferToRouteResult(dict);
            return result;

        }
    }

    //  Реализация Server.Transfer
    public class TransferResult : ActionResult
    {
        public string Url { get; private set; }

        public TransferResult(string url)
        {
            this.Url = url;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            HttpContext httpContext = HttpContext.Current;

            httpContext.Server.TransferRequest(this.Url, true);

        }
    }

    public class TransferToRouteResult : ActionResult
    {
        public string RouteName { get; set; }
        public RouteValueDictionary RouteValues { get; set; }

        public TransferToRouteResult(RouteValueDictionary routeValues)
            : this(null, routeValues)
        {
        }

        public TransferToRouteResult(string routeName, RouteValueDictionary routeValues)
        {
            this.RouteName = routeName ?? string.Empty;
            this.RouteValues = routeValues ?? new RouteValueDictionary();
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            UrlHelper urlHelper = new UrlHelper(context.RequestContext);
            string url = urlHelper.RouteUrl(this.RouteName, this.RouteValues);

            TransferResult actualResult = new TransferResult(url);
            actualResult.ExecuteResult(context);
        }
    }

}