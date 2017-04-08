﻿using System;
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
            //TODO: любое изменение в текстбоксе включая выбор из списка приведёт к событию oninput. 
            //      Поэтому в MakeSuggestion нужно запомнить ввденное значение в модель, а не только генерировать подсказку. 
            //      Тогда SetSuggestion становится не нужен

            this.ModelState.Clear();            

            if (model.SuggestionValues.Count == 0)
                return new EmptyResult(); 

            return PartialView("MakeSuggestion", model);
        }

    }
}