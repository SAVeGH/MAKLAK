using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

using Maklak.Models;
using Maklak.Web.Controllers;

namespace Maklak.Web.ModelBinder
{
    public class TabModelBinder : BaseModelBinder
    {
        public TabModelBinder()
        {
            base.GenerateModel += GenerateTabModel;
        }

        private BaseModel GenerateTabModel(ControllerContext controllerContext, ModelBindingContext modelBindingContext, Type modelType)
        {
            BaseController controller = controllerContext.Controller as BaseController;

            string modelKey = string.Empty;

            if (controllerContext.RouteData.Values.ContainsKey("Key"))
                modelKey = Convert.ToString(controllerContext.RouteData.Values["Key"]);

            TabModel model = TabModelHelper.GenerateModel(controller.SID, modelKey);

            return model;
        }
    }
}