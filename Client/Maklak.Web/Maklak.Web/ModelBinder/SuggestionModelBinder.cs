using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

using Maklak.Models;

namespace Maklak.Web.ModelBinder
{
    public class SuggestionModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            // method is calling inside BindModel method

            HttpRequestBase request = controllerContext.HttpContext.Request;

            string suggestionKey = request.Form.Get("suggestionKey");
            string inputValue = request.Form[0];// первый элемент с любым именем
            Guid sID = Guid.Parse(request.Form.Get("SID"));

            SuggestionModel model = SuggestionModelHelper.GenerateModel(sID,inputValue,suggestionKey);
            return model;
        }
    }
}