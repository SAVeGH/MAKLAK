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

		protected virtual void LoadItems(int? itemId = null)
		{
			SearchParameters searchParameters = new SearchParameters();
			searchParameters.FilterType = ItemsFilterType;
			searchParameters.ItemId = itemId;

			ProcessParameters(searchParameters);
			// синхронная загрузка
			serviceProxy.Search(searchParameters.FilterType, itemId, searchText, itemsDS);

			//StateHasChanged();
			OnStateHasChanged?.Invoke();
		}

		protected virtual void ProcessParameters(SearchParameters searchParameters) 
		{
			// функция нужна для подстановки 'PropertyValue' вместо 'Propety'. Перегружена в классе PropertyFilterModel
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
			LoadItems(row.Id);
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
