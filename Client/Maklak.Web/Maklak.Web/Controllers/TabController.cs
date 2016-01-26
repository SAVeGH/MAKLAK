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
        public ActionResult TabContent(TabModel model)
        {

            string tabLineKey = "Y";

            Dictionary<int, int> state = GetTabState();
            int selectedValue = (int)model.SelectedIndex;
            int selectedY = (int)this.Session[tabLineKey];
            
            if (!state.ContainsKey(selectedY))
                state.Add(selectedY, 0);
                        
            if (!model.IsVertical)
            {
                tabLineKey = "X";
                
                int xState = state[selectedY] == 0 ? 1 : state[selectedY];
                int selectedX = selectedValue == 0 ? xState : selectedValue;

                model.SelectedIndex = selectedX;

                state[selectedY] = selectedX;
            }

            this.Session[tabLineKey] = model.SelectedIndex;

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
            model.SelectedIndex = 1;
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
            model.SelectedIndex = 1;
            this.Session["Y"] = model.SelectedIndex;
            
            return PartialView("TabStrip", model);
        }        

        public ActionResult vTabElement(TabRowModel tabRow)
        {
            return PartialView("VerticalTabElement", tabRow);
        }
    }
}