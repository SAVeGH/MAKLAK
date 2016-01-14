using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Maklak.Models;
using Maklak.Models.DataSets;

namespace Maklak.Web.Controllers
{
    public class TabController : Controller
    {
        //
        // GET: /Tab/


        public ActionResult hTabStrip()
        {
            TabModel model = new TabHModel();
            model.IsVertical = false;

            return PartialView("TabStrip", model);
        }

        
        public ActionResult hTabElement(TabRowModel tabRow)
        {

            return PartialView("HorisontalTabElement", tabRow);
        }

        public ActionResult vTabStrip()
        {
            TabModel model = new TabVModel();
            model.IsVertical = true;

            return PartialView("TabStrip", model);
        }

        [HttpPost]
        public ActionResult TabContent(TabModel model)
        {           

            return PartialView("TabStrip", model);
        }

        public ActionResult vTabElement(TabRowModel tabRow)
        {
            return PartialView("VerticalTabElement", tabRow);
        }
    }
}