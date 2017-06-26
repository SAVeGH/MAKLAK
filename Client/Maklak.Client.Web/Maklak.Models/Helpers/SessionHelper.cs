using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Maklak.Models.Helpers
{
    public static class SessionHelper
    {
        public static void SetValue<T>(Guid sid, string key, T value)
        {
            string sessionKey = sid.ToString() + "_" + key;
            string sid_key = "SID_" + sid.ToString();
            HttpContext.Current.Session[sessionKey] = value;
            HttpContext.Current.Session[sid_key] = true;
        }

        public static T GetValue<T>(Guid sid, string key)
        {
            string sessionKey = sid.ToString() + "_" + key;
            T value = (T)HttpContext.Current.Session[sessionKey];
            return value;
        }

        public static bool Exists(Guid sid)
        {
            string sid_key = "SID_" + sid.ToString();
            return HttpContext.Current.Session[sid_key] != null && (bool)HttpContext.Current.Session[sid_key] == true;
        }

        public static void SetModel(Models.DataSets.ModelDS modelDS)
        {
            string sid = modelDS.Identity[0].SID.ToString();

            if (SessionHelper.GetModel(Guid.Parse(sid)) != null)
                return;

            HttpContext.Current.Session[sid] = modelDS;
        }

        public static Models.DataSets.ModelDS GetModel(Guid sid)
        {
            string sidKey = sid.ToString();
            return HttpContext.Current.Session[sidKey] as Models.DataSets.ModelDS;
        }
    }
}
