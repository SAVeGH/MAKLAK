using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Maklak.Models;
using Maklak.Models.Helpers;
using Maklak.Models.DataSets;
using Maklak.Web.ModelBinder;

namespace Maklak.Web.Controllers
{
    public class TabController : BaseController
    {
        //
        // GET: /Tab/        

        public ActionResult TabStrip([ModelBinder(typeof(TabModelBinder))]   TabStripModel model)
        {            

            return PartialView("TabStrip", model);
        }

        public ActionResult topTabStrip()
        {
            // BaseModel уже создавалась и инициализирована
            
            TabStripModel model = TabStripModelHelper.GenerateModel(this.SID, TabStripModelHelper.GetDefaultXModelType(this.SID));
              
            return PartialView("TabStrip", model);
        }
        
        public ActionResult topTabElement(TabRowModel tabRow)
        {

            return PartialView("HorisontalTabElement", tabRow);
        }

        public ActionResult leftTabStrip()
        {
            TabStripModel model = TabStripModelHelper.GenerateModel(this.SID, TabStripModelHelper.GetDefaultYModelType(this.SID));            
            
            return PartialView("TabStrip", model);
        }        

        public ActionResult leftTabElement(TabRowModel tabRow)
        {
            return PartialView("VerticalTabElement", tabRow);
        }
    }
}