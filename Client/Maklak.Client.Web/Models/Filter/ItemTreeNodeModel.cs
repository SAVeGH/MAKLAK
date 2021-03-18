using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase
using Maklak.Client.Service;
using Maklak.Client.DataSets;


namespace Maklak.Client.Web.Models.Filter
{
	public class ItemTreeNodeModel : ComponentBase
	{		

		public IEnumerable<ItemsTreeDS.ItemsRow> Items 
		{ 
			get 
			{
				
				int? parentId = ParentRow == null ? null : (int?)ParentRow.Id;				
				
				return ItemsSource.Where(i => parentId == null ? i.IsParent_IdNull() : ((!i.IsParent_IdNull()) && i.Parent_Id == parentId));
				
			}
			
		}

		[Parameter]
		public ItemsTreeDS.ItemsDataTable ItemsSource { get; set; }

		[Parameter]
		public ItemsTreeDS.ItemsRow ParentRow { get; set; }
		

		public void OnClick(int itemId) 
		{			

			ItemsTreeDS.ItemsRow currentSelectedRow = this.Items.FirstOrDefault(r => r.IsSelected);

			if (currentSelectedRow != null)
			{
				currentSelectedRow.IsSelected = false;

				if (currentSelectedRow.Id == itemId) // deselect
					return;
			}			

			ItemsTreeDS.ItemsRow row = Items.FirstOrDefault(r => r.Id == itemId);

			if (row == null)
				return;

			row.IsSelected = true;
		}

	}
}
