using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

using Maklak.Models;
using Maklak.Web.ModelBinder;
using Maklak.Web.ControllerFactory;

namespace Maklak.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);            
            ModelBinders.Binders.Add(typeof(TabModel), new TabModelBinder());
            ModelBinders.Binders.Add(typeof(SuggestionModel), new SuggestionModelBinder());
            ModelBinders.Binders.DefaultBinder = new BaseModelBinder();
            RegisterCustomControllerFactory();


        }
        private void RegisterCustomControllerFactory()
        {
            IControllerFactory factory = new BaseControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(factory);
            //ModelBinders.Binders.DefaultBinder
        }
    }
}
