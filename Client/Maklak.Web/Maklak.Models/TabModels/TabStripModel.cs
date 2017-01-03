using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.Models.DataSets;
using MvcSiteMapProvider;

namespace Maklak.Models
{
    public abstract class TabStripModel : BaseModel
    {
        
        public TabStripModel()
        {            
            base.OnModelReady += TabModel_OnModelReady;
        }

        private void TabModel_OnModelReady()
        {
            ModelDS.SiteMapRow rootRow = data.SiteMap.Where(r => r.Key == TabStripModelHelper.TabModelType.CATEGORY.ToString()).FirstOrDefault();
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

    public class SimpleTabModel : TabStripModel
    {
        public SimpleTabModel()
        {            
            Key = TabStripModelHelper.TabModelType.NONE.ToString();
        }        
    }

    public class CategoryTabModel : TabStripModel
    {
        public CategoryTabModel()
        {            
            Key = TabStripModelHelper.TabModelType.CATEGORY.ToString(); ;
        }        
    }

    public class LoginTabModel : TabStripModel 
    {
        public LoginTabModel()
        {
            Key = TabStripModelHelper.TabModelType.LOGIN.ToString(); ;
        }             
    }

    public class SearchTabModel : TabStripModel
    {
        public SearchTabModel()
        {
            
            Key = TabStripModelHelper.TabModelType.SEARCH.ToString();
        }   
    }

    public class InOutTabModel : TabStripModel
    {
        public InOutTabModel()
        {
            Key = TabStripModelHelper.TabModelType.INOUT.ToString();
        }            
    }

    public class ManageTabModel : TabStripModel
    {
        public ManageTabModel()
        {
            Key = TabStripModelHelper.TabModelType.MANAGE.ToString();
        }        
    }


    public class TabRowModel 
    {
        public ModelDS.TabDataRow Row { get; set; }
    }

    


}
