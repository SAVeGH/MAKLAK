using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Maklak.Models;

namespace Maklak.Web.Controllers
{
    public class SuggestionController : BaseController
    {        

        [HttpPost]
        public ActionResult MakeSuggestion(SuggestionModel model)
        {            

            this.ModelState.Clear();            

            if (model.SuggestionValues.Count == 0)
                return new EmptyResult(); //Content(string.Empty);

            return PartialView("MakeSuggestion", model);
        }

        
    }
}