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

        public ActionResult Control()
        {           

            //ExpanderModel expanderModel = new ExpanderModel();
            //expanderModel.Initialize(this.SID);

            return PartialView();
        }

        public ActionResult Content()
        {
            return PartialView();
        }

        public ActionResult Tree(TreeModel treeModel)
        {

            //ExpanderModel expanderModel = new ExpanderModel();
            //expanderModel.Initialize(this.SID);

            return PartialView("TreeControl/Tree",treeModel);
        }

		//public ActionResult NodeFilter(TreeModel treeModel)
		//{
		//	return PartialView("TreeControl/NodeFilter", treeModel);
		//}

		//public ActionResult SelectionPanel(TreeModel treeModel)
		//{
		//	return PartialView("TreeControl/SelectionPanel", treeModel);
		//}

		//public ActionResult TreeNode(TreeNodeModel treeNodeModel)
		//{

		//    //ExpanderModel expanderModel = new ExpanderModel();
		//    //expanderModel.Initialize(this.SID);

		//    return PartialView("TreeControl/TreeNode",treeNodeModel);
		//}


		public ActionResult ProductEditSection()
        {
            return PartialView();
        }

        public ActionResult ProducerSection()
        {
            return PartialView();
        }

        public ActionResult PropertiesEditSection()
        {
            return PartialView("PropertiesEditSection");
        }

        public ActionResult TagsSelectSection(TagModel model)
        {
            return PartialView("TagsSelectSection", model);
        }


    }
}