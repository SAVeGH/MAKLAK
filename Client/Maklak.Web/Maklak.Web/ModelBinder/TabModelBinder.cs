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
            base.InitializeModel += TabModelBinder_InitializeModel;
        }

        private void TabModelBinder_InitializeModel(ControllerContext controllerContext, ModelBindingContext modelBindingContext, Type modelType, BaseModel generatedModel)
        {
            TabModel model = generatedModel as TabModel;

            HttpRequestBase request = controllerContext.HttpContext.Request;

            string modelSelectedKey = request.Form.Get("SelectedKey");

            model.SelectedKey = modelSelectedKey;
        }

        //private void InitializeSuggestionModel(ControllerContext controllerContext, ModelBindingContext modelBindingContext, Type modelType, BaseModel generatedModel)
        //{
        //    throw new NotImplementedException();
        //}

        private BaseModel GenerateTabModel(ControllerContext controllerContext, ModelBindingContext modelBindingContext, Type modelType)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;
            BaseController controller = controllerContext.Controller as BaseController;
            
            string modelKey = request.Form.Get("Key");

            if (string.IsNullOrEmpty(modelKey))
                if (controllerContext.RouteData.Values.ContainsKey("Key"))
                    modelKey = Convert.ToString(controllerContext.RouteData.Values["Key"]);
            //Guid sID = Guid.Parse(request.Form.Get("SID"));          

            TabModel model = TabModelHelper.GenerateModel(controller.SID, modelKey);

            return model;
        }

        //protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        //{
        //    // method is calling inside BindModel method

        //    HttpRequestBase request = controllerContext.HttpContext.Request;

        //    string modelKey = request.Form.Get("Key");
        //    //Guid sID = Guid.Parse(request.Form.Get("SID"));          
            
        //    TabModel model = TabModelHelper.GenerateModel(modelKey); 

        //    return model;
        //}
    }
}