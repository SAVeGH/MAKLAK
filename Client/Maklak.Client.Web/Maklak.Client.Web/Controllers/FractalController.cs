using System.Web.Mvc;

using Maklak.Client.Models;


namespace Maklak.Client.Web.Controllers
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