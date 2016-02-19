using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Maklak.Models;
using Maklak.Models.DataSets;
using Maklak.Web.ModelBinder;

namespace Maklak.Web.Controllers
{
    public class TabController : Controller
    {
        //
        // GET: /Tab/

        [HttpPost]
        public ActionResult TabContent( [ModelBinder(typeof(TabModelBinder))]   TabModel model)
        {

            //string tabLineKey = "Y";

            //Dictionary<int, int> state = GetTabState();
            //int selectedValue = (int)model.SelectedId;
            //int selectedY = (int)this.Session[tabLineKey];

            //if (!state.ContainsKey(selectedY))
            //    state.Add(selectedY, 0);

            //if (!model.IsVertical)
            //{
            //    tabLineKey = "X";
            //    int defaultId = model.DefaultId == 0 ? 1 : model.DefaultId;
            //    int xState = state[selectedY] == 0 ? defaultId : state[selectedY];
            //    int selectedX = selectedValue == 0 ? xState : selectedValue;
                
            //    model = selectedValue == 0 ? TabModelHelper.GenerateModel(TabModelHelper.TabModelType.VERTICAL, selectedY) : model;

            //    model.SelectedId = selectedX;

            //    state[selectedY] = selectedX;
            //}

            //this.Session[tabLineKey] = model.SelectedId;

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
            TabModel model = TabModelHelper.GenerateModel(TabModelHelper.TabModelType.SEARCH);
            
            //this.Session["X"] = model.SelectedId;
                     
            return PartialView("TabStrip", model);
        }
        
        public ActionResult hTabElement(TabRowModel tabRow)
        {

            return PartialView("HorisontalTabElement", tabRow);
        }

        public ActionResult vTabStrip()
        {
            TabModel model = TabModelHelper.GenerateModel(TabModelHelper.TabModelType.CATEGORY);

            //this.Session["Y"] = model.SelectedId;
            
            return PartialView("TabStrip", model);
        }        

        public ActionResult vTabElement(TabRowModel tabRow)
        {
            return PartialView("VerticalTabElement", tabRow);
        }
    }
}