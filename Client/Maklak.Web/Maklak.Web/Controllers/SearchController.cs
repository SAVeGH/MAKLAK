﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Maklak.Models;

namespace Maklak.Web.Controllers
{
    public class SearchController : BaseController
    {
        // GET: Some
        public ActionResult Search(SearchModel model)
        {
            return PartialView(model);
        }

        
    }
}