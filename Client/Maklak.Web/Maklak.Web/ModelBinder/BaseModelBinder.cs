using System;
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
        protected event Func<ControllerContext, ModelBindingContext, Type, BaseModel> GenerateModel;
        protected event Action<ControllerContext, ModelBindingContext, Type, BaseModel> InitializeModel;

        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            BaseModel model = GenerateModel == null ? (base.CreateModel(controllerContext, bindingContext, modelType) as BaseModel) : GenerateModel(controllerContext, bindingContext, modelType);
            BaseController controller = controllerContext.Controller as BaseController;
            // для всех автоматически генерируемых средой моделей происходит привязка к SID
            model.Initialize(controller.SID);

            if (InitializeModel != null)
                InitializeModel(controllerContext, bindingContext, modelType, model);

            return model;
        }
        
    }
}