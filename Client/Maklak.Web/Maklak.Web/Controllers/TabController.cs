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

            string tabLineKey = string.Empty;

            Dictionary<int, int> state = GetTabState();
            int selectedValue = (int)model.SelectedIndex;

            if (model.IsVertical)
            {
                tabLineKey = "Y";

                if (!state.ContainsKey(selectedValue))
                    state.Add(selectedValue, 0);
            }
            else
            {
                tabLineKey = "X";

                int selectedY = (int)this.Session["Y"];                
                int currentState = state[selectedY] == 0 ? 1 : state[selectedY];
                int selectedX = selectedValue == 0 ? currentState  : selectedValue;
                
                model.SelectedIndex = selectedX;                             

                state[selectedY] = selectedX;
            }

            this.Session[tabLineKey] = model.SelectedIndex;

            return PartialView("TabStrip", model);
        }

        //[HttpPost]
        //public ActionResult TabContent(TabModel model)
        //{

        //    string tabLineKey = string.Empty;            

        //    Dictionary<int, int> state = GetTabState();

        //    if (model.IsVertical)
        //    {
        //        tabLineKey = "Y";

        //        if (!state.ContainsKey((int)model.SelectedIndex))
        //            state.Add((int)model.SelectedIndex, 0);
        //    }
        //    else
        //    {
        //        tabLineKey = "X";

        //        int lastY = (int)this.Session["Y"];

        //        if (model.SelectedIndex == 0)
        //        {
        //            int savedX = state[lastY];

        //            if (savedX == 0)
        //            {
        //                savedX = 1;
        //                state[lastY] = savedX;
        //            }

        //            model.SelectedIndex = savedX;
        //        }
        //        else
        //        {
        //            state[lastY] = (int)model.SelectedIndex;
        //        }                
        //    }

        //    this.Session[tabLineKey] = model.SelectedIndex;                  

        //    return PartialView("TabStrip", model);
        //}

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