using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maklak.Models.Helpers;

namespace Maklak.Models
{
    public class BaseModel
    {
        protected Models.DataSets.ModelDS data;

        public void Initialize(Guid sID)
        {
            data = SessionHelper.GetModel(sID);

            if (data != null)
                return;

            data = new DataSets.ModelDS();

            Models.DataSets.ModelDS.IdentityRow row = data.Identity.NewIdentityRow();

            row.SID = sID;

            data.Identity.AddIdentityRow(row);

            data.Identity.AcceptChanges();

            SessionHelper.SetModel(data);

        }

        public string Action
        {
            get
            {
                if (data == null)
                    return string.Empty;

                Models.DataSets.ModelDS.ACDRow row = data.ACD.AsEnumerable().Where(r => r.CODE == "CURRENT").FirstOrDefault();

                if (row == null)
                    return string.Empty;

                return row.Action;
                 
             }
            set
            {
                if (data == null)
                    return;

                Models.DataSets.ModelDS.ACDRow row = data.ACD.AsEnumerable().Where(r => r.CODE == "CURRENT").FirstOrDefault();

                if (row == null)
                {
                    row = data.ACD.NewACDRow();
                    row.CODE = "CURRENT";
                    data.ACD.AddACDRow(row);
                    data.ACD.AcceptChanges();
                }                    

                row.Action = value;
            }
        }
        public string Controller
        {
            get
            {
                if (data == null)
                    return string.Empty;

                Models.DataSets.ModelDS.ACDRow row = data.ACD.AsEnumerable().Where(r => r.CODE == "CURRENT").FirstOrDefault();

                if (row == null)
                    return string.Empty;

                return row.Controller;

            }
            set
            {
                if (data == null)
                    return;

                Models.DataSets.ModelDS.ACDRow row = data.ACD.AsEnumerable().Where(r => r.CODE == "CURRENT").FirstOrDefault();

                if (row == null)
                {
                    row = data.ACD.NewACDRow();
                    row.CODE = "CURRENT";
                    data.ACD.AddACDRow(row);
                    data.ACD.AcceptChanges();
                }

                row.Controller = value;
            }
        }
        public Guid SID
        {
            get
            {
                if (data == null)
                    return Guid.Empty;

                Models.DataSets.ModelDS.IdentityRow row = data.Identity[0];

                return row.SID;
            }            
        }
        
    }
}
