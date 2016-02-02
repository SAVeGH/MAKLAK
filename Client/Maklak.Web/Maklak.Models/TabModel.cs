﻿using System;
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

        
        public string Code { get; set; }  
        
    }

    public class TabHModel : TabModel 
    {
        public TabHModel()
        {
            Code = "HM";
        }

        public int? SelectedHIndex 
        {
            get { return base.SelectedId; }
            set { base.SelectedId = value; }
        }

        public override void Init()
        {
            IsVertical = false;

            TabData = new TabDS();

            TabDS.TabDataRow row = TabData.TabData.NewTabDataRow();
            row.Id = 1;
            row.Name = "item 1";
            row.IsActive = false;
            row.IsVisible = true;
            TabData.TabData.Rows.Add(row);
            row = TabData.TabData.NewTabDataRow();
            row.Id = 2;
            row.Name = "item 2";
            row.IsActive = false;
            row.IsVisible = true;
            TabData.TabData.Rows.Add(row);
            row = TabData.TabData.NewTabDataRow();
            row.Id = 3;
            row.Name = "item 3";
            row.IsActive = false;
            row.IsVisible = true;
            TabData.TabData.Rows.Add(row);
        }
    }
    public class TabVModel : TabModel
    {
        public TabVModel()
        {
            Code = "VM";
        }

        public int? SelectedVIndex
        {
            get { return base.SelectedId; }
            set { base.SelectedId = value; }
        }

        public override void Init()
        {
            IsVertical = false;

            TabData = new TabDS();

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

    public class TabRowModel 
    {
        public TabDS.TabDataRow Row { get; set; }
    }

    


}
