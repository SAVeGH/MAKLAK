using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.Models.DataSets;
using MvcSiteMapProvider;

namespace Maklak.Models
{
    public abstract class TabModel : BaseModel
    {        
        public TabModel()
        {            

            base.OnModelInitialized += TabModel_OnModelInitialized;
            base.OnModelReady += TabModel_OnModelReady;           
                      
        }

        private void TabModel_OnModelReady()
        {
            ModelDS.SiteMapRow rootRow = data.SiteMap.Where(r => r.Key == TabModelHelper.TabModelType.CATEGORY.ToString()).FirstOrDefault();
            this.Action = rootRow.Action;
            this.Controller = rootRow.Controller;
        }

        private void TabModel_OnModelInitialized()
        {           

            InitTabData();
            
        }

        private void InitTabData()
        {
            InitTabData(null, null, null);

            data.TabData.AcceptChanges();
        }

        private void InitTabData(ModelDS.SiteMapRow mapRow, ModelDS.SiteMapRow parentMapRow, ModelDS.TabDataRow parentRow)
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
            tabRow.IsDefault = parentMapRow == null ? false : parentMapRow.DefaultKey == mapRow.Key;
            tabRow.Active = tabRow.IsDefault;
            data.TabData.AddTabDataRow(tabRow);

            foreach (ModelDS.SiteMapRow row in data.SiteMap.Where(r => !r.IsParent_IdNull() && r.Parent_Id == mapRow.Id))
            {
                InitTabData(row, mapRow, tabRow);
            }
        }

        public bool IsVertical { get; set; }

        public ModelDS.TabDataDataTable TabData
        {
            get
            {
                int parentId = data.TabData.Where(pr => pr.Key == Key).Select(prs => prs.Id).FirstOrDefault();

                ModelDS.TabDataDataTable dataTable = new ModelDS.TabDataDataTable();
                List<ModelDS.TabDataRow> rows = data.TabData.Where(r => !r.IsParent_IdNull() && r.Parent_Id == parentId).ToList();
                rows.ForEach(r => dataTable.ImportRow(r));
                dataTable.AcceptChanges();
                return dataTable;
            }
        }       

        // Ключь самой модели
        public string Key
        {
            get
            {
                return ModelKey();
            }
         }

        // Ключь дочерней модели
        public string DefaultKey { get; protected set; }
        // Ключь выбранного таба в дочерней модели
        public string SelectedKey
        {
            get
            {
                ModelDS.TabDataRow row = data.TabData.Where(r => r.Active).FirstOrDefault();

                if (row == null)
                    return string.Empty;

                return row.Key;

            }

            set
            {                

                ModelDS.TabDataRow row = data.TabData.Where(r => r.Active).FirstOrDefault();

                if (row != null)
                    row.Active = false;

                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    return;

                row = data.TabData.Where(r => r.Key == value).FirstOrDefault();

                if (row == null)
                    return;

                row.Active = true;

            }
        }

        protected abstract string ModelKey();

        protected override bool IsModelInitialized()
        {
            return base.IsModelInitialized() && data.TabData.Count > 0;
        }


    }

    public class CategoryTabModel : TabModel
    {
        public CategoryTabModel()
        {            
            IsVertical = true;                      
        }

        protected override string ModelKey()
        {
            return TabModelHelper.TabModelType.CATEGORY.ToString();
        }
    }

    public class LoginTabModel : TabModel 
    {
        protected override string ModelKey()
        {
            return TabModelHelper.TabModelType.LOGIN.ToString();           
        }        
    }

    public class SearchTabModel : TabModel
    {
       
        protected override string ModelKey()
        {
            return TabModelHelper.TabModelType.SEARCH.ToString();            
        }        
    }

    public class InOutTabModel : TabModel
    {
        
        protected override string ModelKey()
        {
            return TabModelHelper.TabModelType.INOUT.ToString();            
        }        
    }

    public class ManageTabModel : TabModel
    {
        
        protected override string ModelKey()
        {
            return TabModelHelper.TabModelType.MANAGE.ToString();            
        }
    }


    public class TabRowModel 
    {
        public ModelDS.TabDataRow Row { get; set; }
    }

    


}
