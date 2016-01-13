﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.Models.DataSets;

namespace Maklak.Models
{
    public class TabModel
    {
        protected int selectedIndex;

        public TabModel() 
        {
            
            IsVertical = false;

            TabData = new TabDS();

            TabDS.TabDataRow row = TabData.TabData.NewTabDataRow();
            row.Id = 1;
            row.Name = "item 1";
            row.IsActive = true;
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

        public bool IsVertical { get; set; }
        public TabDS TabData { get; set; }
        public int SelectedIndex 
        {
            get
            { 
                selectedIndex = TabData.TabData.AsEnumerable().Where(r => r.IsActive).Select(r => r.Id).FirstOrDefault(); 
                return selectedIndex; 
            }
            set
            {
                TabDS.TabDataRow newSelectedRow = TabData.TabData.AsEnumerable().Where(r => r.Id == value).FirstOrDefault();
                TabDS.TabDataRow currentSelectedRow = TabData.TabData.AsEnumerable().Where(r => r.IsActive).FirstOrDefault();
                if (newSelectedRow == null)
                    return;

                if (currentSelectedRow != null)
                    currentSelectedRow.IsActive = false;

                newSelectedRow.IsActive = true;

                selectedIndex = value;

            }

        }
    }

    public class TabHModel : TabModel 
    {
        public int SelectedHIndex 
        {
            get { return base.SelectedIndex; }
            set { base.SelectedIndex = value; }
        }
    }
    public class TabVModel : TabModel
    {
        public int SelectedVIndex
        {
            get { return base.SelectedIndex; }
            set { base.SelectedIndex = value; }
        }
    }

    public class TabRowModel 
    {
        public TabDS.TabDataRow Row { get; set; }
    }

    


}
