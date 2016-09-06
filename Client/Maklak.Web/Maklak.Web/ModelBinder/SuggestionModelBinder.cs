using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

using Maklak.Models;

namespace Maklak.Web.ModelBinder
{
    public class SuggestionModelBinder : BaseModelBinder
    {
        public SuggestionModelBinder()
        {
            base.GenerateModel += GenerateSuggestionModel;
            base.InitializeModel += InitializeSuggestionModel;
        }

        protected void InitializeSuggestionModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType, BaseModel generatedModel)
        {
            // method is calling inside BindModel method

            HttpRequestBase request = controllerContext.HttpContext.Request;

            string suggestionKey = request.Form.Get("suggestionKey");
            string inputValue = request.Form[0];// первый элемент с любым именем

            SuggestionModel model = generatedModel as SuggestionModel;

            
            SuggestionModelHelper.InitModel(model, inputValue, suggestionKey);

        }

        protected BaseModel GenerateSuggestionModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            // method is calling inside BindModel method
            

            SuggestionModel model = SuggestionModelHelper.GenerateModel();
            return model;
        }
    }
}