﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Maklak.Client.Models;

namespace Maklak.Client.Web.Controllers
{
    public class SuggestionController : BaseController
    {        

        [HttpPost]
        public ActionResult MakeSuggestion(SuggestionModel model)
        {
            //     Любое изменение в текстбоксе включая выбор из списка приведёт к событию oninput. 
            //      Поэтому в MakeSuggestion нужно запомнить ввденное значение в модель, а не только генерировать подсказку. 
            //      SetSuggestion так же вызывает событие oninput и в итоге MakeSuggestion

            //this.ModelState.Clear();            

            if (model.SuggestionData.Count == 0)
                return new EmptyResult(); 

            return PartialView("MakeSuggestion", model);
        }

    }
}