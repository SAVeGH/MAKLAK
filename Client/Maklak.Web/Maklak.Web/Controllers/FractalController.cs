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
    public class FractalController : BaseController
    {
        public ActionResult FractalPanel(FractalModel model)
        {            
            return PartialView(model.FractalPanelAction, model);
        }

        public ActionResult FractalControl(FractalModel model)
        {
            return PartialView(model.FractalControlAction, model);
        }

        public ActionResult FractalContent(FractalModel model)
        {
            return PartialView(model.FractalContentAction, model);
        }
    }
}