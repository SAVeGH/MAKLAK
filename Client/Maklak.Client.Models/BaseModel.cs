using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MvcSiteMapProvider;
using Maklak.Client.Models.Helpers;
using Maklak.Client.DataSets;

namespace Maklak.Client.Models
{
    public class BaseModel
    {
        protected ModelDS data;
        protected event Action OnModelInitialized;
        protected event Action OnModelReady;
        

        public void Initialize(Guid sID)
        {
            data = SessionHelper.GetModel(sID);            

            if (!IsModelInitialized())
            {
                BaseInitialization(sID);

                RiseOnModelInitialized();
            }            
            // DataSet заполнен. Вызов события для работы с данными
            RiseOnModelReady();

        }

        protected virtual bool IsModelInitialized()
        {
            return IsBaseModelInitialized();
        }

        private bool IsBaseModelInitialized()
        {
            if (data == null)
                return false;

            if (data.Identity.Count == 0 || data.SiteMap.Count == 0 || data.FractalData.Count == 0)
                return false;

            return true;
        }

        private void BaseInitialization(Guid sID)
        {
            if (IsBaseModelInitialized())
                return;

            data = new DataSets.ModelDS();

            InitIdenty(sID);

            SessionHelper.SetModel(data);

            InitSiteMap();

            InitFractalData();

        }

		public virtual void Created(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			
		}

		private void RiseOnModelReady()
        {
            if (OnModelReady == null)
                return;

            OnModelReady();
        }

        private void RiseOnModelInitialized()
        {
            if (OnModelInitialized == null)
                return;

            OnModelInitialized();
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

            if (node.Attributes.Keys.Contains("dokposition"))
                row.DokPosition = Convert.ToString(node.Attributes["dokposition"]);

            if (node.Attributes.Keys.Contains("recursiveaction"))
                row.RecursiveAction = Convert.ToString(node.Attributes["recursiveaction"]);

            if (node.Attributes.Keys.Contains("recursivecontroller"))
                row.RecursiveController = Convert.ToString(node.Attributes["recursivecontroller"]);


            data.SiteMap.AddSiteMapRow(row);
            // рекурсивное заполнение по дочерним узлам
            foreach (ISiteMapNode childNode in node.ChildNodes)
            {
                InitSiteMap(childNode, row);
            }
        }

        private void InitFractalData()
        {
            InitFractalData(null, null, null);

            data.FractalData.AcceptChanges();
        }

        private void InitFractalData(ModelDS.SiteMapRow mapRow, ModelDS.SiteMapRow parentMapRow, ModelDS.FractalDataRow parentRow)
        {
            ModelDS.FractalDataRow tabRow = data.FractalData.NewFractalDataRow();

            if (mapRow == null)
            {
                mapRow = ModelHelper.GetRootTabRow(this.SID); 
                tabRow.SetParent_IdNull();
            }
            else
            {
                tabRow.Parent_Id = parentRow.Id;
            }

            tabRow.Name = mapRow.Title;
            tabRow.Key = mapRow.Key;
            tabRow.IsDefault = parentMapRow == null ? false : parentMapRow.DefaultKey == mapRow.Key;
            tabRow.Active = tabRow.IsDefault;

            if(!mapRow.IsDokPositionNull())
                tabRow.DokPosition = mapRow.DokPosition;

            data.FractalData.AddFractalDataRow(tabRow);
            // рекурсивное заполнение по дочерним узлам
            foreach (ModelDS.SiteMapRow row in data.SiteMap.Where(r => !r.IsParent_IdNull() && r.Parent_Id == mapRow.Id))
            {
                InitFractalData(row, mapRow, tabRow);
            }
        }

        public string Action
        {
            get
            {
                if (data == null)
                    return string.Empty;

                ModelDS.ACDRow row = data.ACD.AsEnumerable().Where(r => r.CODE == "CURRENT").FirstOrDefault();

                if (row == null)
                    return string.Empty;

                return row.Action;
                 
             }
            set
            {
                if (data == null)
                    return;

                ModelDS.ACDRow row = data.ACD.AsEnumerable().Where(r => r.CODE == "CURRENT").FirstOrDefault();

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

                ModelDS.ACDRow row = data.ACD.AsEnumerable().Where(r => r.CODE == "CURRENT").FirstOrDefault();

                if (row == null)
                    return string.Empty;

                return row.Controller;

            }
            set
            {
                if (data == null)
                    return;

                ModelDS.ACDRow row = data.ACD.AsEnumerable().Where(r => r.CODE == "CURRENT").FirstOrDefault();

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

                ModelDS.IdentityRow row = data.Identity[0];

                return row.SID;
            }            
        }
        
    }
}
