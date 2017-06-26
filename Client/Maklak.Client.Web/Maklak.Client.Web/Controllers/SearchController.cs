using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Maklak.Client.Models;

namespace Maklak.Client.Web.Controllers
{
    public class SearchController : BaseController
    {
        // GET: Some
        public ActionResult Search(SearchModel model)
        {
            

            return PartialView(model);
        }

        public ActionResult Control(ExpanderModel expanderModel)
        {           

            //ExpanderModel expanderModel = new ExpanderModel();
            //expanderModel.Initialize(this.SID);

            return PartialView(expanderModel);
        }

        public ActionResult Content()
        {
            return PartialView();
        }

        public ActionResult ProductEditSection()
        {
            return PartialView();
        }

        public ActionResult PropertiesEditSection()
        {
            return new EmptyResult();
        }

        public ActionResult TagsSelectSection(TagModel model)
        {
            return PartialView("TagsSelectSection", model);
        }


    }
}