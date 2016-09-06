using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

using Maklak.Models;

namespace Maklak.Web.ModelBinder
{
    public class TabModelBinder : BaseModelBinder
    {
        public TabModelBinder()
        {
            base.GenerateModel += GenerateTabModel;
            //base.InitializeModel += InitializeSuggestionModel;
        }

        //private void InitializeSuggestionModel(ControllerContext controllerContext, ModelBindingContext modelBindingContext, Type modelType, BaseModel generatedModel)
        //{
        //    throw new NotImplementedException();
        //}

        private BaseModel GenerateTabModel(ControllerContext controllerContext, ModelBindingContext modelBindingContext, Type modelType)
        {
            HttpRequestBase request = controllerContext.HttpContext.Request;

            string modelKey = request.Form.Get("Key");
            //Guid sID = Guid.Parse(request.Form.Get("SID"));          

            TabModel model = TabModelHelper.GenerateModel(modelKey);

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