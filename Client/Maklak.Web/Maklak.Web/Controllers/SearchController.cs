using System;
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

        public ActionResult Control()
        {           

            ExpanderModel expanderModel = new ExpanderModel();
            expanderModel.Initialize(this.SID);

            return PartialView(expanderModel);
        }

        public ActionResult Content()
        {
            

            return PartialView();
        }

        public ActionResult Product()
        {
            return new EmptyResult();
        }

        public ActionResult Properties()
        {
            return new EmptyResult();
        }

        public ActionResult Tags()
        {
            return new EmptyResult();
        }


    }
}