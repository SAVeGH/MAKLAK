using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

using Maklak.Client.Models;
using Maklak.Client.Web.Controllers;

namespace Maklak.Client.Web.ModelBinder
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

            TabStripModel model = TabStripModelHelper.GenerateModel(controller.SID, modelKey);

            return model;
        }
    }
}