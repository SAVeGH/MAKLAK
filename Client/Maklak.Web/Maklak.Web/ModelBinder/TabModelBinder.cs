using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maklak.Models;

namespace Maklak.Web.ModelBinder
{
    public class TabModelBinder : DefaultModelBinder
    {        
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            // method is calling inside BindModel method

            HttpRequestBase request = controllerContext.HttpContext.Request;

            bool isVertical = Convert.ToBoolean(request.Form.Get("IsVertical"));

            TabModel model = isVertical ? new TabVModel() as TabModel : new TabHModel() as TabModel;

            return model;
        }
    }
}