﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maklak.Models;
using Maklak.Web.Controllers;

namespace Maklak.Web.ModelBinder
{
    public class BaseModelBinder : DefaultModelBinder
    {
        protected Func<ControllerContext, ModelBindingContext, Type, BaseModel> GenerateModel;
        protected Action<ControllerContext, ModelBindingContext, Type, BaseModel> InitializeModel;

        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            BaseModel model = GenerateModel == null ? (base.CreateModel(controllerContext, bindingContext, modelType) as BaseModel) : GenerateModel(controllerContext, bindingContext, modelType);
            BaseController controller = controllerContext.Controller as BaseController;
            model.Initialize(controller.SID);

            if (InitializeModel != null)
                InitializeModel(controllerContext, bindingContext, modelType, model);

            return model;
        }
        //public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        //{

        //    BaseModel model = bindingContext.Model as BaseModel;
        //    BaseController controller = controllerContext.Controller as BaseController;
        //    model.Initialize(controller.SID);

        //    return model;
        //}
    }
}