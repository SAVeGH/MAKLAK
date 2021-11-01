﻿using System;
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

		[Parameter]
		public Action OnRefreshTree { get; set; } // делегат инициализируется функцией из ItemsTree в разметке
		public IEnumerable<ItemsTreeDS.ItemsRow> Items 
		{ 
			get 
			{
				
				int? parentId = ParentRow == null ? null : (int?)ParentRow.Id;				
				string parentType = ParentRow == null ? null : ParentRow.IsItemTypeNull() ? null : ParentRow.ItemType;

				return ItemsSource.Where(i => parentId == null ? i.IsParent_IdNull() : ((!i.IsParent_IdNull()) && i.Parent_Id == parentId && (string.IsNullOrEmpty(parentType) ? i.IsParentItemTypeNull() : (!i.IsParentItemTypeNull() && i.ParentItemType == parentType))));
				
			}
			
		}

		[Parameter]
		public ItemsTreeDS.ItemsDataTable ItemsSource { get; set; }

		[Parameter]
		public ItemsTreeDS.ItemsRow ParentRow { get; set; }

		public void OnNodeRowClicked(ItemsTreeDS.ItemsRow row)
		{
			ItemsTreeDS.ItemsRow currentSelectedRow = this.ItemsSource.FirstOrDefault(r => r.IsSelected);

			if (currentSelectedRow != null)
			{
				currentSelectedRow.IsSelected = false;

				if (currentSelectedRow == row) // deselect
					return;
			}

			row.IsSelected = true;

			// вызов рефреша всего дерева т.к. снятие/установка селекта могут происходить в разных компонентах (вложенных иерархически)
			OnRefreshTree?.Invoke();
		}

		//private void InvokeStateHasChangedAsync()
		//{
		//	this.InvokeAsync(this.StateHasChanged); // нужно вызывать только асинхроно
		//}

		//public void OnNodeRowClicked(ItemsTreeDS.ItemsRow row) 
		//{			

		//	ItemsTreeDS.ItemsRow currentSelectedRow = this.ItemsSource.FirstOrDefault(r => r.IsSelected);

		//	if (currentSelectedRow != null)
		//	{
		//		currentSelectedRow.IsSelected = false;

		//		if (currentSelectedRow.Id == itemId) // deselect
		//			return;
		//	}			

		//	ItemsTreeDS.ItemsRow row = ItemsSource.FirstOrDefault(r => r.Id == itemId);

		//	if (row == null)
		//		return;

		//	row.IsSelected = true;
		//}

		public void OnToggleNodeClicked(ItemsTreeDS.ItemsRow row) 
		{			
			OnNodeToggle?.Invoke(row); // вызов функции из ItemsTree. Дерево извещает о клике на узле вызовом события.
		}

	}
}
