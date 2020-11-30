using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase.
using Maklak.Client.Service;
using Maklak.Client.DataSets;


namespace Maklak.Client.Web.Models
{
	public class ItemsTreeModel : ComponentBase
	{
		private ItemsTreeDS itemsDS;

		public ItemsTreeModel()
		{
			itemsDS = new ItemsTreeDS();

			ItemsTreeDS.ItemsRow rootRow = Items.NewItemsRow();
			rootRow.Id = int.MaxValue;
			rootRow.Parent_Id = int.MinValue;
			rootRow.Name = "Root";
			Items.AddItemsRow(rootRow);

			ItemsTreeDS.ItemsRow row = null;

			for (int i = 0; i < 20; i++)
			{
				row = Items.NewItemsRow();
				row.Id = 1 + i;
				row.Parent_Id = rootRow.Id;
				row.Name = i.ToString();// + " " + this.ItemsFilterType;
				//row.Name = this.ItemsFilterType;
				Items.AddItemsRow(row);
			}
		}
		public ItemsTreeDS.ItemsDataTable Items { get { return itemsDS.Items; } }

		//[Parameter]
		//public bool ShowRoot { get; set; }
	}
}
