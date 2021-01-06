using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase.
using Maklak.Client.Service;
using Maklak.Client.DataSets;

namespace Maklak.Client.Web.Models
{
	public class ItemsFilterModel : ComponentBase
	{
		private ItemsTreeDS itemsDS;

		public ItemsFilterModel()
		{
			itemsDS = new ItemsTreeDS();
			//LoadItems();
		}

		[Inject]
		public StateModel StateStorage { get; set; }

		[Inject]
		private grpcProxy serviceProxy { get; set; }

		[Inject]
		public PopUpStateModel popUpState { get; set; }

		

		private string searchText;

		[Parameter]
		public string SearchText
		{
			get { return searchText; }
			set // устанавливается на oninput
			{
				searchText = value;
				LoadItems();
			}
		}	
		

		[Parameter]
		public string ItemsFilterType
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

		protected override void OnInitialized()
		{
			base.OnInitialized();

			popUpState.OnRefresh += PopUpState_OnRefresh;

			LoadItems();
		}

		private async Task LoadItems()
		{
			//itemsDS.Clear();

			//ItemsTreeDS.ItemsRow rootRow = itemsDS.Items.NewItemsRow();
			//rootRow.Id = int.MaxValue;        // не существующий Id
			//rootRow.Parent_Id = int.MinValue; // вместо NULL
			//rootRow.Name = "Root";
			//itemsDS.Items.AddItemsRow(rootRow);

			await serviceProxy.SearchAsync(ItemsFilterType, searchText, itemsDS);

			//await Task.Run(() => { serviceProxy.Search(ItemsFilterType, searchText, itemsDS); });

			//await serviceProxy.Search(ItemsFilterType, searchText, itemsDS);

			//this.InvokeAsync(StateHasChanged);	
			StateHasChanged();
		}

		public void OnAdd()
		{
			PopUpInput popUpInput = popUpState.InputParameters;
			popUpInput.FilterType = this.ItemsFilterType;
			popUpInput.dialogType = typeof(Maklak.Client.Web.Controls.Filter.ItemEditor);
			//popUpState.OnRefresh += PopUpState_OnRefresh;
			//popUpState.InputParameters = popUpInput;
			popUpState.IsVisible = true;			
		}

		public void OnDelete() 
		{
			DeleteItem();
		}

		private void DeleteItem()
		{
			ItemsTreeDS.ItemsRow currentSelectedRow = this.Items.FirstOrDefault(r => r.IsSelected);

			if (currentSelectedRow == null)
				return;

			serviceProxy.DeleteItem(ItemsFilterType, currentSelectedRow.Id);

			LoadItems();
		}

		private void PopUpState_OnRefresh()
		{
			LoadItems();

			//this.InvokeAsync(StateHasChanged);

			//this.StateHasChanged(); // Иначе содержимое не отображается		

		}
	}
}
