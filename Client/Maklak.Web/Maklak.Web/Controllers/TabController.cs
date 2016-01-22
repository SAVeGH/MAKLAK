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

        [HttpPost]
        public ActionResult TabContent(TabModel model,bool Reset)
        {
            
            string tabKey = model.IsVertical ? "Y" : "X";

            if(model.IsVertical)
                this.Session[tabKey] = model.SelectedIndex;

            Dictionary<int, int> state = GetTabState();  
            
                      
            
            //int hTabId = model.IsVertical ? state.Where(kvp => kvp.Key == model.SelectedIndex).FirstOrDefault().Value : model.SelectedIndex;
            //this.Session[tabKey] = model.IsVertical ? model.SelectedIndex : hTabId;

            //int vTabId = (int)this.Session["Y"];

            //if (state.ContainsKey(vTabId))
            //    state[vTabId] = hTabId;
            //else
            //    state.Add(vTabId, hTabId);



            //if (!model.IsVertical)
            //    model.SelectedIndex = hTabId;           

            return PartialView("TabStrip", model);
        }

        private Dictionary<int, int> GetTabState()
        {
            string sessionKey = "TabState";
            Dictionary<int, int> state = (Dictionary<int, int>)this.Session[sessionKey];

            if (state == null)
            {
                state = new Dictionary<int, int>();                
                this.Session[sessionKey] = state;
            }
            

            return state;
        }

        public ActionResult hTabStrip()
        {
            TabModel model = new TabHModel();
            model.IsVertical = false;
            this.Session["X"] = model.SelectedIndex;
                     
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
            this.Session["Y"] = model.SelectedIndex;
            
            return PartialView("TabStrip", model);
        }        

        public ActionResult vTabElement(TabRowModel tabRow)
        {
            return PartialView("VerticalTabElement", tabRow);
        }
    }
}