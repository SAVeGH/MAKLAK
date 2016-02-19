﻿using System;
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

            string modelKey = request.Form.Get("Key");
            
            //int selectedY = request.RequestContext.HttpContext.Session["Y"] == null ? 0 : Convert.ToInt32(request.RequestContext.HttpContext.Session["Y"]);

            TabModelHelper.TabModelType tabModelType = (TabModelHelper.TabModelType)Enum.Parse(typeof(TabModelHelper.TabModelType), modelKey);
            TabModel model = TabModelHelper.GenerateModel(tabModelType); 

            return model;
        }
    }
}