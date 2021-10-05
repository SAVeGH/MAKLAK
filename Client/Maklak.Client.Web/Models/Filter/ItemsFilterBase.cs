using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//using Microsoft.AspNetCore.Components; // for ComponentBase.
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

		//[Inject]
		//public StateModel StateStorage { get; set; }

		//[Inject]
		protected grpcProxy serviceProxy { get; set; }

		//[Inject]
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

		public ItemsTreeDS.ItemsRow CurrentItemRow
		{
			get
			{
				return this.Items.FirstOrDefault(r => r.IsSelected);
			}
		}

		public void OnInitialized()
		{
			popUpState.OnRefresh += PopUpState_OnRefresh;			

			LoadItems();
		}		

		protected virtual void LoadItems(ItemsTreeDS.ItemsRow row = null)
		{
			//SearchParameters searchParameters = new SearchParameters();
			//searchParameters.FilterType = ItemsFilterType;
			//searchParameters.ItemId = itemId;

			//ProcessParameters(searchParameters);
			// синхронная загрузка
			ItemsTreeDS searchData = serviceProxy.Search(ItemsFilterType, row, searchText);

			CleanUpNodes(row);

			AddRootRow();

			AddChildNodes(row, searchData);

			//StateHasChanged();
			OnStateHasChanged?.Invoke();
		}

		protected virtual void ProcessParameters(SearchParameters searchParameters) 
		{
			// функция нужна для подстановки 'PropertyValue' вместо 'Propety'. Перегружена в классе PropertyFilterModel
		}

		//protected void CleanUpItems(int? itemId) 
		//{
		//	serviceProxy.CleanUp(itemId, itemsDS);
		//}

		private void CleanUpNodes(ItemsTreeDS.ItemsRow rootRow)
		{
			if (rootRow == null || rootRow.IsParent_IdNull())
			{				
				itemsDS.Items.Clear();
				return; // перезапись всех узлов
			}

			//ItemsTreeDS.ItemsRow rootRow = itemsDS.Items.FirstOrDefault(item => item.Id == parentItemId && item.ItemType == itemType);
			List<ItemsTreeDS.ItemsRow> deleteList = itemsDS.Items.Where(item => !item.IsParent_IdNull() &&
																			 item.Parent_Id == rootRow.Id &&
																			 !item.IsParentItemTypeNull() &&
																			 item.ParentItemType == rootRow.ItemType)
																  .ToList();
			// удаление всех дочерних узлов. Сам rootRow не удаляется
			foreach (ItemsTreeDS.ItemsRow row in deleteList)
				CleanUpChildNodes(row);

			//if(stateChanged)
			//	OnStateHasChanged?.Invoke();

		}

		private void CleanUpChildNodes(ItemsTreeDS.ItemsRow rootRow)
		{
			if (rootRow == null)
				return;

			//ItemsTreeDS.ItemsRow rootRow = itemsDS.Items.FirstOrDefault(item => item.Id == itemId && item.ItemType == itemType);
			List<ItemsTreeDS.ItemsRow> deleteList = itemsDS.Items.Where(item => !item.IsParent_IdNull() &&
																			 item.Parent_Id == rootRow.Id &&
																			 !item.IsParentItemTypeNull() &&
																			 item.ParentItemType == rootRow.ItemType)
																 .ToList();

			foreach (ItemsTreeDS.ItemsRow row in deleteList)
				CleanUpChildNodes(row);

			itemsDS.Items.RemoveItemsRow(rootRow);
		}

		private void AddChildNodes(ItemsTreeDS.ItemsRow rootRow, ItemsTreeDS searchData)
		{
			int parentRowId = rootRow == null ? int.MaxValue : rootRow.Id;
			string parentRowItemType = rootRow == null ? null : rootRow.ItemType;
			//itemsDS.Items.Im

			foreach (ItemsTreeDS.ItemsRow item in searchData.Items)
			{


				ItemsTreeDS.ItemsRow row = itemsDS.Items.NewItemsRow();

				row.Id = item.Id;// + (itemId == null ? 0 : 300) ;
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
			popUpInput.dialogType = typeof(Maklak.Client.Web.Controls.Filter.ItemEditor);
			popUpInput.Height = 120;
			popUpInput.Width = 300;
			popUpInput.Title = "Add";
			
			popUpState.Show();
		}		

		public virtual void EditItem()
		{
			if (this.CurrentItemRow == null)
				return;

			PopUpInput popUpInput = popUpState.InputParameters;
			popUpInput.FilterType = this.ItemsFilterType;
			//popUpInput.Id = this.CurrentItemRow.Id;
			//popUpInput.Row = this.CurrentItemRow;
			popUpInput.SetDataRow(this.CurrentItemRow);
			popUpInput.dialogType = typeof(Maklak.Client.Web.Controls.Filter.ItemEditor);
			popUpInput.Height = 120;
			popUpInput.Width = 300;
			popUpInput.Title = "Edit";
			
			popUpState.Show();
		}		

		public virtual void DeleteItem()
		{
			//ItemsTreeDS.ItemsRow currentSelectedRow = this.Items.FirstOrDefault(r => r.IsSelected);

			if (this.CurrentItemRow == null)
				return;

			serviceProxy.DeleteItem(ItemsFilterType, this.CurrentItemRow.Id);

			LoadItems();
		}

		public virtual void Toggle(ItemsTreeDS.ItemsRow row) 
		{
			if (!row.IsOpened)
			{
				
				ItemsTreeDS.ItemsRow openRow = new ItemsTreeRowHelper(row).Row;
				openRow.Parent_Id = openRow.Id;
				LoadItems(openRow);
			}
			else
				CleanUpNodes(row); //CleanUpItems(row.Id);

			row.IsOpened = !row.IsOpened;

			OnStateHasChanged?.Invoke();
		}

		private void PopUpState_OnRefresh()
		{
			LoadItems();

			//this.InvokeAsync(StateHasChanged);

			//this.StateHasChanged(); // Иначе содержимое не отображается		

		}
	}

	public class SearchParameters 
	{
		public string FilterType { get; set; }
		public int? ItemId { get; set; }
	}

}
