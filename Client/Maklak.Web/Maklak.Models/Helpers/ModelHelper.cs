using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Models.Helpers
{
    public static class ModelHelper
    {
        private static Guid generateSID()
        {
            Guid sid = Guid.NewGuid();
            return sid;
        }

        public static BaseModel CreateBaseModel()
        {
            Models.DataSets.ModelDS ds = new DataSets.ModelDS();

            DataSets.ModelDS.IdentityRow row = ds.Identity.NewIdentityRow();
            row.SID = generateSID();
            ds.Identity.Rows.Add(row);
            ds.AcceptChanges();

            SessionHelper.SetModel(ds);



            BaseModel model = new BaseModel();

            model.Action = TabModelHelper.DefaultAction;
            model.Controller = TabModelHelper.DefaultController;
            model.SID = row.SID;
            //model.GenerateSID(); // генерируем уникальный SID для страницы

            return model;
        }
    }
}
