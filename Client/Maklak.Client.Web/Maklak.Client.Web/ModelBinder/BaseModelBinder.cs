using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maklak.Models;
using Maklak.Client.Web.Controllers;

namespace Maklak.Client.Web.ModelBinder
{
    public class BaseModelBinder : DefaultModelBinder
    {
        // событие позволяет создать конкретную модель для классов наследников от BaseModelBinder
        // Например создать экземпляр SuggestionModel при привязке запроса 
        protected event Func<ControllerContext, ModelBindingContext, Type, BaseModel> GenerateModel;
        // событие позволяет инициализировать модель данными запроса
        protected event Action<ControllerContext, ModelBindingContext, Type, BaseModel> InitializeModel;

        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            // создать модель 
            BaseModel model = GenerateModel == null ? (base.CreateModel(controllerContext, bindingContext, modelType) as BaseModel) : GenerateModel(controllerContext, bindingContext, modelType);
            BaseController controller = controllerContext.Controller as BaseController;            

            // для всех автоматически генерируемых средой моделей происходит привязка к SID
            model.Initialize(controller.SID);
            // инициализация модели данными запроса
            if (InitializeModel != null)
                InitializeModel(controllerContext, bindingContext, modelType, model);

            return model;
        }
        
    }
}