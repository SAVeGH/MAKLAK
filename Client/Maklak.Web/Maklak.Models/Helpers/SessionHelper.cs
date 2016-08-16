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

        public static Guid GenerateSID()
        {
            Guid sid = Guid.NewGuid();            
            return sid;
        }

        //public static int GenerateSID()
        //{
        //    int sid = 1; // section id - создаётся новая секция в сессии для каждой вкладки

        //    if (HttpContext.Current.Session["SID"] != null)
        //    {
        //        sid = Convert.ToInt32(HttpContext.Current.Session["SID"]);
        //        sid++;
        //        HttpContext.Current.Session["SID"] = sid; // в сессии сохраняется последний выданный sid (эмуляция static autoincrement поля)
        //    }
        //    else
        //    {
        //        HttpContext.Current.Session["SID"] = sid;
        //    }

        //    return sid;
        //}
    }
}
