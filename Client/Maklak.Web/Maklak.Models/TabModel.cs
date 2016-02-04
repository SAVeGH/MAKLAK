using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.Models.DataSets;

namespace Maklak.Models
{
    public abstract class TabModel
    {
        protected int? selectedId;
        
        public TabModel() 
        {
            TabData = new TabDS();

            Init();            
        }

        public bool IsVertical { get; set; }
        public TabDS TabData { get; set; }
        public int? SelectedId
        {
            get
            { 
                selectedId = TabData.TabData.AsEnumerable().Where(r => r.IsActive).Select(r => r.Id).FirstOrDefault(); 
                return selectedId; 
            }
            set
            {
                selectedId = value;

                TabDS.TabDataRow currentSelectedRow = TabData.TabData.AsEnumerable().Where(r => r.IsActive).FirstOrDefault();                

                if (currentSelectedRow != null)
                    currentSelectedRow.IsActive = false;

                if (value == null)
                    return;

                TabDS.TabDataRow newSelectedRow = TabData.TabData.AsEnumerable().Where(r => r.Id == value).FirstOrDefault();

                if (newSelectedRow == null)
                    return;

                newSelectedRow.IsActive = true;

                

            }
        }

        public int DefaultId { get; set; }

        public abstract void Init();

        
        public TabModelHelper.TabModelType Code { get; set; }  
        
    }

    public class TabVModel : TabModel
    {
        public TabVModel()
        {
            Code = TabModelHelper.TabModelType.VERTICAL;
            IsVertical = true;            
            SelectedId = 1;
            DefaultId = 1;
        }        

        public override void Init()
        {                       

            TabDS.TabDataRow row = TabData.TabData.NewTabDataRow();
            row.Id = 1;
            row.Name = "Search & Order";
            row.IsActive = false;
            row.IsVisible = true;            
            TabData.TabData.Rows.Add(row);

            row = TabData.TabData.NewTabDataRow();
            row.Id = 2;
            row.Name = "In & Out";
            row.IsActive = false;
            row.IsVisible = true;            
            TabData.TabData.Rows.Add(row);

            row = TabData.TabData.NewTabDataRow();
            row.Id = 3;
            row.Name = "Manage";
            row.IsActive = false;
            row.IsVisible = true;            
            TabData.TabData.Rows.Add(row);

            row = TabData.TabData.NewTabDataRow();
            row.Id = 4;
            row.Name = "Login & Profile";
            row.IsActive = false;
            row.IsVisible = true;            
            TabData.TabData.Rows.Add(row);
        }
    }

    public class LoginModel : TabModel 
    {
        public LoginModel()
        {
            Code = TabModelHelper.TabModelType.LOGIN;            
            SelectedId = 1;
            DefaultId = 1;
        }        

        public override void Init()
        {                  

            TabDS.TabDataRow row = TabData.TabData.NewTabDataRow();
            row.Id = 1;
            row.Name = "Login";
            row.IsActive = false;
            row.IsVisible = true;
            TabData.TabData.Rows.Add(row);
            row = TabData.TabData.NewTabDataRow();
            row.Id = 2;
            row.Name = "Profile";
            row.IsActive = false;
            row.IsVisible = true;
            TabData.TabData.Rows.Add(row);
            
        }
    }

    public class SearchModel : TabModel
    {
        public SearchModel()
        {
            Code = TabModelHelper.TabModelType.SEARCH;
            SelectedId = 1;
            DefaultId = 1;
        }

        public override void Init()
        {
            IsVertical = false;

            TabData = new TabDS();

            TabDS.TabDataRow row = TabData.TabData.NewTabDataRow();
            row.Id = 1;
            row.Name = "Search";
            row.IsActive = false;
            row.IsVisible = true;
            TabData.TabData.Rows.Add(row);
            row = TabData.TabData.NewTabDataRow();
            row.Id = 2;
            row.Name = "Order";
            row.IsActive = false;
            row.IsVisible = true;
            TabData.TabData.Rows.Add(row);
            row = TabData.TabData.NewTabDataRow();
            row.Id = 2;
            row.Name = "Summary";
            row.IsActive = false;
            row.IsVisible = true;
            TabData.TabData.Rows.Add(row);

        }
    }


    public class TabRowModel 
    {
        public TabDS.TabDataRow Row { get; set; }
    }

    


}
