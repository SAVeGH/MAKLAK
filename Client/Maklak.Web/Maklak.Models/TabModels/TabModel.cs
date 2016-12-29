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
            base.OnModelReady += TabModel_OnModelReady;
        }

        private void TabModel_OnModelReady()
        {
            ModelDS.SiteMapRow rootRow = data.SiteMap.Where(r => r.Key == TabModelHelper.TabModelType.CATEGORY.ToString()).FirstOrDefault();
            this.Action = rootRow.Action;
            this.Controller = rootRow.Controller;
        }

        

        public ModelDS.TabDataDataTable StripData
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

        

        // Ключь самой модели содержащей табы
        public string Key
        {
            get; set;            
         }

        
        // Ключь выбранного таба в дочерней модели
        //public string SelectedKey
        //{
        //    get
        //    {
        //        ModelDS.TabDataRow row = this.StripData.Where(r => r.Active).FirstOrDefault();

        //        if(row == null)
        //            row = this.StripData.Where(r => r.IsDefault).FirstOrDefault();

        //        if (row == null)
        //            return string.Empty;

        //        return row.Key;

        //    }

        //    set
        //    {
        //        if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
        //            return;

        //        ModelDS.TabDataRow row = this.StripData.Where(r => r.Active).FirstOrDefault();

        //        if (row != null)
        //        {
        //            ModelDS.TabDataRow baseRow = data.TabData.Where(r => r.Id == row.Id).FirstOrDefault();
        //            baseRow.Active = false;
        //            //row.Active = false;
        //        }            

        //        row = this.StripData.Where(r => r.Key == value).FirstOrDefault();

        //        if (row == null)
        //            row = this.StripData.Where(r => r.IsDefault).FirstOrDefault();

        //        if (row == null)
        //            return;

        //        ModelDS.TabDataRow dataRow = data.TabData.Where(r => r.Id == row.Id).FirstOrDefault();
        //        dataRow.Active = true;
        //        //row.Active = true;

        //    }
        //}

        

        protected override bool IsModelInitialized()
        {
            return base.IsModelInitialized() && data.TabData.Count > 0;
        }

        public string TabPanelID
        {
            get;
            set;
        }


    }

    public class SimpleTabModel : TabModel
    {
        public SimpleTabModel()
        {            
            Key = TabModelHelper.TabModelType.NONE.ToString();
        }        
    }

    public class CategoryTabModel : TabModel
    {
        public CategoryTabModel()
        {            
            Key = TabModelHelper.TabModelType.CATEGORY.ToString(); ;
        }        
    }

    public class LoginTabModel : TabModel 
    {
        public LoginTabModel()
        {
            Key = TabModelHelper.TabModelType.LOGIN.ToString(); ;
        }             
    }

    public class SearchTabModel : TabModel
    {
        public SearchTabModel()
        {
            
            Key = TabModelHelper.TabModelType.SEARCH.ToString();
        }   
    }

    public class InOutTabModel : TabModel
    {
        public InOutTabModel()
        {
            Key = TabModelHelper.TabModelType.INOUT.ToString();
        }            
    }

    public class ManageTabModel : TabModel
    {
        public ManageTabModel()
        {
            Key = TabModelHelper.TabModelType.MANAGE.ToString();
        }        
    }


    public class TabRowModel 
    {
        public ModelDS.TabDataRow Row { get; set; }
    }

    


}
