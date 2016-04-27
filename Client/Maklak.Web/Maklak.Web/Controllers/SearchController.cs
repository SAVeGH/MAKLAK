using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Maklak.Models;

namespace Maklak.Web.Controllers
{
    public class SearchController : BaseController
    {
        // GET: Some
        public ActionResult Search(SearchModel model)
        {
            return PartialView(model);
        }

        //[HttpPost]
        //public ActionResult PropertySuggestion(string PropertyInput)
        //{
        //    SearchModel model = new SearchModel();

        //    this.ModelState.Clear();

        //    List<string> list = new List<string>();//= //Account.GetNameSuggestion(PropertyInput);

        //    for (int i = 0; i < 8; i++)
        //    {
        //        list.Add("item_" + i.ToString());
        //    }
        //    model.Names.AddRange(list);

        //    if (model.Names.Count == 0)
        //        return Content(string.Empty);

        //    return PartialView("PropertySuggestion", model);
        //}
    }
}