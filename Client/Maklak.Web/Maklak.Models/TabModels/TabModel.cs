using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.Models.DataSets;
using MvcSiteMapProvider;

namespace Maklak.Models
{
    public class TabModel : BaseModel
    {
        protected string selectedKey;
        
        public TabModel() 
        {
            TabData = new TabDS();
            ISiteMapNode rootTabNode = TabModelHelper.RootTabNode;
            this.Action = rootTabNode.Action;
            this.Controller = rootTabNode.Controller;

            Init();            
        }

        public bool IsVertical { get; set; }
        public TabDS TabData { get; set; }       


        public virtual void Init()
        {
            DefaultKey = TabModelHelper.DefaultKey(SiteMapHelper.NodeByKey(this.Key));

            foreach (ISiteMapNode node in TabModelHelper.RootTabNode.ChildNodes)
            {
                TabDS.TabDataRow row = TabData.TabData.NewTabDataRow();
                row.Key = node.Key;
                row.Name = node.Title;
                TabData.TabData.Rows.Add(row);
            }
        }

        // Ключь самой модели
        public string Key { get; protected set; }
        // Ключь дочерней модели
        public string DefaultKey { get; protected set; }
        // Ключь выбранного таба в дочерней модели
        public string SelectedKey { get; set; }


    }

    public class CategoryTabModel : TabModel
    {
        public CategoryTabModel()
        {            
            IsVertical = true;
            Key = TabModelHelper.TabModelType.CATEGORY.ToString();            
        }       
    }

    public class LoginTabModel : TabModel 
    {
        public LoginTabModel()
        {
            Key = TabModelHelper.TabModelType.LOGIN.ToString();           
            
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
        public TabDS.TabDataRow Row { get; set; }
    }

    


}
