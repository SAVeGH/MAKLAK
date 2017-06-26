using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Maklak.Models;
using Maklak.Models.Helpers;
using Maklak.Models.DataSets;
using Maklak.Client.Web.ModelBinder;

namespace Maklak.Client.Web.Controllers
{
    public class TabController : BaseController
    {
        //
        // GET: /Tab/        

        public ActionResult TabStrip([ModelBinder(typeof(TabModelBinder))]   TabStripModel model)
        {
            return PartialView("TabStrip", model);
        }

        // При проходе фрактала передаётся Key в параметрах запроса. Он и является ключом модели для TabStrip. 
        // Поэтому модель привязывается правильно стандартным биндером.
        public ActionResult topTabStrip([ModelBinder(typeof(TabModelBinder))]   TabStripModel model)
        {
            return PartialView("TabStrip", model);
        }

        public ActionResult topTabElement(TabRowModel tabRow)
        {
            return PartialView("HorisontalTabElement", tabRow);
        }

        public ActionResult leftTabStrip([ModelBinder(typeof(TabModelBinder))]   TabStripModel model)
        {
            return PartialView("TabStrip", model);
        }       

        public ActionResult leftTabElement(TabRowModel tabRow)
        {
            return PartialView("VerticalTabElement", tabRow);
        }
    }
}