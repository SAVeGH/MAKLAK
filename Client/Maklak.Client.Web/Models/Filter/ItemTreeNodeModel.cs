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
		[Parameter]
		public Action<ItemsTreeDS.ItemsRow> OnNodeToggle { get; set; } // делегат инициализируется функцией из ItemsTree в разметке
		public IEnumerable<ItemsTreeDS.ItemsRow> Items 
		{ 
			get 
			{
				
				int? parentId = ParentRow == null ? null : (int?)ParentRow.Id;				
				string parentType = ParentRow == null ? null : ParentRow.ItemType;

				return ItemsSource.Where(i => parentId == null ? i.IsParent_IdNull() : ((!i.IsParent_IdNull()) && i.Parent_Id == parentId && i.ItemType == parentType));
				
			}
			
		}

		[Parameter]
		public ItemsTreeDS.ItemsDataTable ItemsSource { get; set; }

		[Parameter]
		public ItemsTreeDS.ItemsRow ParentRow { get; set; }
		

		public void OnNodeRowClicked(int itemId) 
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

		public void OnToggleNodeClicked(ItemsTreeDS.ItemsRow row) 
		{			
			OnNodeToggle?.Invoke(row); // вызов функции из ItemsTree. Дерево извещает о клике на узле вызовом события.
		}

	}
}
