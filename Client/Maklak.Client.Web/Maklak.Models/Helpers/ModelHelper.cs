using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.Models.Helpers;
using Maklak.Models.DataSets;

namespace Maklak.Models.Helpers
{
    public static class ModelHelper
    {
        public static void SetDefaults(BaseModel model)
        {           
            model.Action = ModelHelper.DefaultAction(model.SID);
            model.Controller = ModelHelper.DefaultController(model.SID);            
        }

        internal static string DefaultController(Guid sID)
        {

            ModelDS.SiteMapRow rootRow = GetRootTabRow(sID);

            return rootRow.RecursiveController;
        }

        internal static string DefaultAction(Guid sID)
        {
            ModelDS.SiteMapRow rootRow = GetRootTabRow(sID);

            return rootRow.RecursiveAction;
        }

        public static ModelDS.SiteMapRow GetRootTabRow(Guid sID)
        {
            ModelDS data = SessionHelper.GetModel(sID);

            ModelDS.SiteMapRow row = data.SiteMap.Where(r => !r.IsRecursiveControllerNull()).FirstOrDefault();

            return row;
        }

        public static string RootKey(Guid sID)
        {
            return ModelHelper.GetRootTabRow(sID).Key;            
        }
    }
}
