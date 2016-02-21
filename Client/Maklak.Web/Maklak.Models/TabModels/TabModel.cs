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
            ISiteMapNode rootNode = SiteMapHelper.NodeByKey(this.Key);

            DefaultKey = TabModelHelper.DefaultKey(rootNode);

            foreach (ISiteMapNode node in rootNode.ChildNodes)
            {
                TabDS.TabDataRow row = TabData.TabData.NewTabDataRow();
                row.Key = node.Key;
                row.Name = node.Title;
                row.Enabled = DefaultKey == node.Key;
                TabData.TabData.Rows.Add(row);
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
                TabDS.TabDataRow row = TabData.TabData.AsEnumerable().Where(r => r.Enabled).FirstOrDefault();

                if (row == null)
                    return string.Empty;

                return row.Key;

            }

            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    return;

                TabDS.TabDataRow row = TabData.TabData.AsEnumerable().Where(r => r.Enabled).FirstOrDefault();

                if (row == null)
                    return;

                row.Enabled = false;

                row = TabData.TabData.AsEnumerable().Where(r => r.Key == value).FirstOrDefault();

                if (row == null)
                    return;

                row.Enabled = true;

            }
        }

        protected abstract string ModelKey();


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
        public TabDS.TabDataRow Row { get; set; }
    }

    


}
