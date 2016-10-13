using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Models.Helpers
{
    public static class ModelHelper
    {
        
        public static BaseModel CreateBaseModel(Guid sID)
        {
            

            BaseModel model = new BaseModel();
            model.Initialize(sID);
            model.Action = TabModelHelper.DefaultAction(sID);
            model.Controller = TabModelHelper.DefaultController(sID);            

            return model;
        }
    }
}
