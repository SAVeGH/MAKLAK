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

        [HttpPost]
        public ActionResult TabContent( [ModelBinder(typeof(TabModelBinder))]   TabModel model)
        {

            string tabLineKey = "Y";

            Dictionary<string, string> state = GetTabState(model.SID);
            string selectedKey = model.SelectedKey;
            string selectedY = SessionHelper.GetValue<string>(model.SID, tabLineKey); //(string)this.Session[tabLineKey];

            if (!state.ContainsKey(selectedY))
                state.Add(selectedY, string.Empty);

            if (!model.IsVertical)
            {
                tabLineKey = "X";
                
                string xState = string.IsNullOrEmpty(state[selectedY]) ? model.DefaultKey : state[selectedY];
                string selectedX = string.IsNullOrEmpty(selectedKey) ? xState : selectedKey;
                
                model.SelectedKey = selectedX;

                state[selectedY] = selectedX;
            }

            //this.Session[tabLineKey] = model.SelectedKey;
            SessionHelper.SetValue<string>(model.SID, tabLineKey, model.SelectedKey);

            return PartialView("TabStrip", model);
        }

        private Dictionary<string, string> GetTabState(Guid sid)
        {
            string sessionKey = "TabState";
            Dictionary<string, string> state = SessionHelper.GetValue<Dictionary<string, string>>(sid, sessionKey); //(Dictionary<string, string>)this.Session[sessionKey];

            if (state == null)
            {
                state = new Dictionary<string, string>();
                //this.Session[sessionKey] = state;
                
                SessionHelper.SetValue<Dictionary<string, string>>(sid,sessionKey, state);
            }
            

            return state;
        }

        public ActionResult hTabStrip()
        {
            
            TabModel model = TabModelHelper.GenerateModel(this.SID, TabModelHelper.DefaultXModelKey);
            //model.SID = contextModel.SID;
            SessionHelper.SetValue<string>(model.SID, "X", model.SelectedKey);
            //this.Session["X"] = model.SelectedKey;
                     
            return PartialView("TabStrip", model);
        }
        
        public ActionResult hTabElement(TabRowModel tabRow)
        {

            return PartialView("HorisontalTabElement", tabRow);
        }

        public ActionResult vTabStrip()
        {
            TabModel model = TabModelHelper.GenerateModel(this.SID, TabModelHelper.DefaultYModelKey);
            //model.SID = contextModel.SID;
            //this.Session["Y"] = model.SelectedKey;
            SessionHelper.SetValue<string>(model.SID, "Y", model.SelectedKey);
            return PartialView("TabStrip", model);
        }        

        public ActionResult vTabElement(TabRowModel tabRow)
        {
            return PartialView("VerticalTabElement", tabRow);
        }
    }
}