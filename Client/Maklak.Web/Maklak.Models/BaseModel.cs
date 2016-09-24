using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvcSiteMapProvider;
using Maklak.Models.Helpers;
using Maklak.Models.DataSets;

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

            InitIdenty(sID);

            InitSiteMap();

            InitTabData();

            SessionHelper.SetModel(data);

        }

        private void InitIdenty(Guid sID)
        {
            ModelDS.IdentityRow row = data.Identity.NewIdentityRow();

            row.SID = sID;

            data.Identity.AddIdentityRow(row);

            data.Identity.AcceptChanges();
        }

        private void InitSiteMap()
        {
            InitSiteMap(null, null);

            data.SiteMap.AcceptChanges();
        }

        private void InitTabData()
        {
            InitTabData(null, null);

            data.TabData.AcceptChanges();
        }

        private void InitTabData(ModelDS.SiteMapRow mapRow, ModelDS.TabDataRow parentRow)
        {
            ModelDS.TabDataRow tabRow = data.TabData.NewTabDataRow();

            if (mapRow == null)
            {
                mapRow = data.SiteMap.Where(r => r.Key == TabModelHelper.TabModelType.CATEGORY.ToString()).FirstOrDefault();
                tabRow.SetParent_IdNull();
            }
            else
            {
                tabRow.Parent_Id = parentRow.Id;
            }
            
            tabRow.Name = mapRow.Title;
            tabRow.Key = mapRow.Key;

            data.TabData.AddTabDataRow(tabRow);

            foreach (ModelDS.SiteMapRow row in data.SiteMap.Where(r => !r.IsParent_IdNull() && r.Parent_Id == mapRow.Id))
            {
                InitTabData(row, tabRow);
            }
        }

        private void InitSiteMap(ISiteMapNode node, ModelDS.SiteMapRow parentRow)
        {
            ModelDS.SiteMapRow row = data.SiteMap.NewSiteMapRow();

            if (node == null)
            {
                node = SiteMapHelper.SiteMap.RootNode;
                row.SetParent_IdNull();
            }
            else
            {
                row.Parent_Id = parentRow.Id;
            }

            row.Title = node.Title;
            row.Action = node.Action;
            row.Controller = node.Controller;
            row.Key = node.Key;

            if (node.Attributes.Keys.Contains("defaultkey"))            
                row.DefaultKey = Convert.ToString(node.Attributes["defaultkey"]);
            else
                row.SetDefaultKeyNull();               

            data.SiteMap.AddSiteMapRow(row);

            foreach (ISiteMapNode childNode in node.ChildNodes)
            {
                InitSiteMap(childNode, row);
            }
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
                }                    

                row.Action = value;
                data.ACD.AcceptChanges();
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
                }

                row.Controller = value;
                data.ACD.AcceptChanges();
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
