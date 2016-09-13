using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Models.Helpers
{
    public static class ModelHelper
    {
        //private static Guid generateSID()
        //{
        //    Guid sid = Guid.NewGuid();
        //    return sid;
        //}

        public static BaseModel CreateBaseModel(Guid sID)
        {
            

            BaseModel model = new BaseModel();
            model.Initialize(sID);
            model.Action = TabModelHelper.DefaultAction;
            model.Controller = TabModelHelper.DefaultController;
            //model.SID = row.SID;
            //model.GenerateSID(); // генерируем уникальный SID для страницы

            return model;
        }
    }
}
