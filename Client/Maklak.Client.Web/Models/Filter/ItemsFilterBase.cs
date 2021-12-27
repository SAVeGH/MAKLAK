using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Maklak.Client.Service;
using Maklak.Client.DataSets;
using Maklak.Client.Web.Models.PopUp;

namespace Maklak.Client.Web.Models.Filter
{
	public class ItemsFilterBase 
	{
		private ItemsTreeDS itemsDS;		

		public event Action OnStateHasChanged;

		//тут нельзя внедрить через [Inject] - поэтому передача через конструктор из компонента
		public ItemsFilterBase(PopUpStateModel popUpStateModel, grpcProxy srvProxy)
		{
			itemsDS = new ItemsTreeDS();
			this.IsEditable = true;

			serviceProxy = srvProxy;
			popUpState = popUpStateModel;			
			
		}		

		protected grpcProxy serviceProxy { get; set; }		
		protected PopUpStateModel popUpState { get; set; }

		private string searchText;
		
		public string SearchText
		{
			get { return searchText; }
			set // устанавливается на oninput
			{
				searchText = value;
				LoadItems();
			}
		}
		
		public virtual string ItemsFilterType
		{
			get; set;
		}
		
		public bool IsEditable
		{
			get; set;
		}

		public ItemsTreeDS.ItemsDataTable Items
		{
			get
			{
				return itemsDS.Items;
			}
		}

		//[Parameter]
		//public ItemsTreeDS.ItemsDataTable CheckedItems
		//{
		//	get; set;
		//}

		public ItemsTreeDS.ItemsRow CurrentItemRow
		{
			get
			{
				return this.Items.FirstOrDefault(r => r.IsSelected);
			}
		}

		public void OnInitialized()
		{
			LoadItems();
		}		

		protected virtual void LoadItems(ItemsTreeDS.ItemsRow row = null)
		{			
			// синхронная загрузка
			ItemsTreeDS searchData = serviceProxy.Search(ItemsFilterType, row, searchText);

			CleanUpNodes(row);

			AddRootRow();

			AddChildNodes(row, searchData);

			AfterLoad();

			OnStateHasChanged?.Invoke();
		}

		protected virtual void AfterLoad() 
		{
			// метод для обработки данных после загрузки с сервера
		}

		protected virtual void CleanUpNodes(ItemsTreeDS.ItemsRow rootRow)
		{
			if (rootRow == null || rootRow.IsParent_IdNull())
			{				
				itemsDS.Items.Clear();
				return; // перезапись всех узлов
			}
			
			List<ItemsTreeDS.ItemsRow> deleteList = itemsDS.Items.Where(item => !item.IsParent_IdNull() &&
																			 item.Parent_Id == rootRow.Id &&
																			 !item.IsParentItemTypeNull() &&
																			 item.ParentItemType == rootRow.ItemType)
																  .ToList();
			// удаление всех дочерних узлов. Сам rootRow не удаляется
			foreach (ItemsTreeDS.ItemsRow row in deleteList)
				CleanUpChildNodes(row);			

		}

		protected void CleanUpChildNodes(ItemsTreeDS.ItemsRow rootRow)
		{
			if (rootRow == null)
				return;
			
			List<ItemsTreeDS.ItemsRow> deleteList = itemsDS.Items.Where(item => !item.IsParent_IdNull() &&
																			 item.Parent_Id == rootRow.Id &&
																			 !item.IsParentItemTypeNull() &&
																			 item.ParentItemType == rootRow.ItemType)
																 .ToList();

			foreach (ItemsTreeDS.ItemsRow row in deleteList)
				CleanUpChildNodes(row);

			itemsDS.Items.RemoveItemsRow(rootRow);
		}

		// загрузка всегда в один уровень без рекурсии т.к. приходят данные parent-a
		// подходит всем кроме Category. Там перегружена
		protected virtual void AddChildNodes(ItemsTreeDS.ItemsRow rootRow, ItemsTreeDS searchData)
		{
			int parentRowId = rootRow == null || rootRow.IsIdNull() ? int.MaxValue : rootRow.Id;
			string parentRowItemType = rootRow == null || rootRow.IsIdNull() ? null : rootRow.ItemType;

			foreach (ItemsTreeDS.ItemsRow item in searchData.Items)
			{

				ItemsTreeDS.ItemsRow row = itemsDS.Items.NewItemsRow();

				row.Id = item.Id;
				row.Parent_Id = parentRowId;
				row.Name = item.Name;
				row.ItemType = item.ItemType;
				row.ParentItemType = parentRowItemType;
				if (!item.IsMeasureUnit_IdNull())
					row.MeasureUnit_Id = (int)item.MeasureUnit_Id;
				if (!item.IsHasChildrenNull())
					row.HasChildren = (bool)item.HasChildren;

				itemsDS.Items.AddItemsRow(row);
			}
		}

		private void AddRootRow()
		{
			ItemsTreeDS.ItemsRow rootRow = itemsDS.Items.FirstOrDefault(item => item.Id == int.MaxValue);

			if (rootRow != null)
				return;

			rootRow = itemsDS.Items.NewItemsRow();
			rootRow.Id = int.MaxValue;        // не существующий Id

			rootRow.SetParent_IdNull();
			rootRow.Name = "Root";
			rootRow.SetItemTypeNull();
			itemsDS.Items.AddItemsRow(rootRow);
		}

		//private async Task LoadItems() 
		//{
		//	//itemsDS.Clear();

		//	//ItemsTreeDS.ItemsRow rootRow = itemsDS.Items.NewItemsRow();
		//	//rootRow.Id = int.MaxValue;        // не существующий Id
		//	//rootRow.Parent_Id = int.MinValue; // вместо NULL
		//	//rootRow.Name = "Root";
		//	//itemsDS.Items.AddItemsRow(rootRow);

		//	await serviceProxy.SearchAsync(ItemsFilterType, null, searchText, itemsDS);

		//	//await Task.Run(() => { serviceProxy.Search(ItemsFilterType, searchText, itemsDS); });

		//	//await serviceProxy.Search(ItemsFilterType, searchText, itemsDS);

		//	//this.InvokeAsync(StateHasChanged);	
		//	StateHasChanged();
		//}



		public virtual void AddItem()
		{
			PopUpInput popUpInput = popUpState.InputParameters;
			popUpInput.FilterType = this.ItemsFilterType;

			popUpInput.SetDataRow(null); // установка пустой строки
			popUpInput.Row.ItemType = this.ItemsFilterType;
			popUpInput.dialogType = typeof(Maklak.Client.Web.Controls.Filter.ItemEditor);
			popUpInput.Height = 120;
			popUpInput.Width = 300;
			popUpInput.Title = "Add";
			popUpState.OnClose += PopUpState_AddItemComplete;

			popUpState.Show();
		}

		public virtual void AddItemComplete() 
		{
			serviceProxy.AddItem(popUpState.InputParameters.FilterType, popUpState.InputParameters.Row);

			LoadItems();
		}

		public virtual void DeleteItemComplete() 
		{
			if (this.CurrentItemRow == null)
				return;

			serviceProxy.DeleteItem(ItemsFilterType, this.CurrentItemRow.Id);

			LoadItems();
		}

		public virtual void EditItem()
		{
			if (this.CurrentItemRow == null)
				return;

			PopUpInput popUpInput = popUpState.InputParameters;
			popUpInput.FilterType = this.ItemsFilterType;
			
			popUpInput.SetDataRow(this.CurrentItemRow);
			popUpInput.dialogType = typeof(Maklak.Client.Web.Controls.Filter.ItemEditor);
			popUpInput.Height = 120;
			popUpInput.Width = 300;
			popUpInput.Title = "Edit";
			popUpState.OnClose += PopUpState_EditItemComplete;

			popUpState.Show();
		}

		public virtual void EditItemComplete() 
		{
			serviceProxy.EditItem(popUpState.InputParameters.FilterType, popUpState.InputParameters.Row);			

			LoadItems();
		}

		private void PopUpState_AddItemComplete()
		{
			AddItemComplete(); // нужно перегружать для иерархических списков т.к. требует вызова LoadItems с параметрами. По умолчанию грузит только верхний уровень. 
		}

		private void PopUpState_EditItemComplete()
		{
			EditItemComplete();
		}

		public virtual void DeleteItem()
		{
			DeleteItemComplete();
		}

		public virtual void Toggle(ItemsTreeDS.ItemsRow row) 
		{
			if (!row.IsOpened)
			{
				// делается копия строки
				ItemsTreeDS.ItemsRow openRow = new ItemsTreeRowHelper(row).Row;
				openRow.Parent_Id = openRow.Id;
				LoadItems(openRow);
			}
			else
				CleanUpNodes(row); 

			row.IsOpened = !row.IsOpened;

			OnStateHasChanged?.Invoke();
		}		
	}
}
