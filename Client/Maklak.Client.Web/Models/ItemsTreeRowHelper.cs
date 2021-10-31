using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maklak.Client.DataSets;

namespace Maklak.Client.Web.Models
{
	public class ItemsTreeRowHelper
	{
		ItemsTreeDS.ItemsDataTable innerTable;		

		public ItemsTreeRowHelper(ItemsTreeDS.ItemsRow row) 
		{
			Row = row;
		}

		public void Clear() 
		{
			if(innerTable != null)
				innerTable.Clear();

			innerTable = new ItemsTreeDS.ItemsDataTable();
		}
		private void SetEmptyRow() 
		{
			if (innerTable == null)
				return;

			ItemsTreeDS.ItemsRow row = innerTable.NewItemsRow();
			innerTable.AddItemsRow(row);
		}

		public ItemsTreeDS.ItemsRow Row
		{
			get
			{
				return innerTable.FirstOrDefault();
			}

			set 
			{				
				Clear();

				if (value == null) 
				{
					SetEmptyRow();
					return;
				}

				innerTable.ImportRow(value);
			}
		}
	}
}
