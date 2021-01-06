using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase
using Maklak.Client.Service;
using Maklak.Client.DataSets;


namespace Maklak.Client.Web.Models
{
	public class ItemTreeNodeModel : ComponentBase
	{
		//int selectedId;

		public IEnumerable<ItemsTreeDS.ItemsRow> Items 
		{ 
			get 
			{
				// что бы не работать с DBNull использую int.MinValue
				int parentId = ParentRow == null ? int.MinValue : ParentRow.Id;
				return ItemsSource.Where(i => i.Parent_Id == parentId);
			}
			
		}

		[Parameter]
		public ItemsTreeDS.ItemsDataTable ItemsSource { get; set; }

		[Parameter]
		public ItemsTreeDS.ItemsRow ParentRow { get; set; }

		//public int SelectedId { get { return selectedId; } }

		public void OnClick(int itemId) 
		{
			//selectedId = itemId;

			ItemsTreeDS.ItemsRow currentSelectedRow = this.Items.FirstOrDefault(r => r.IsSelected);

			if (currentSelectedRow != null)
				currentSelectedRow.IsSelected = false;

			ItemsTreeDS.ItemsRow row = Items.FirstOrDefault(r => r.Id == itemId);

			if (row == null)
				return;

			row.IsSelected = true;
		}

	}
}
