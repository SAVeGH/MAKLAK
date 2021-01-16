﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase.
using Maklak.Client.Service;
using Maklak.Client.DataSets;
using Maklak.Client.Web.Models.PopUp;

namespace Maklak.Client.Web.Models.Filter
{
	public class ItemsFilterModel : ComponentBase
	{
		private ItemsTreeDS itemsDS;

		public ItemsFilterModel()
		{
			itemsDS = new ItemsTreeDS();
			this.IsEditable = true;
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

		[Parameter]
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

		protected override void OnInitialized()
		{
			base.OnInitialized();

			popUpState.OnRefresh += PopUpState_OnRefresh;

			//this.IsEditable = true;

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

			await serviceProxy.SearchAsync(ItemsFilterType, null, searchText, itemsDS);

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
			popUpInput.Height = 120;
			popUpInput.Width = 300;
			popUpInput.Title = "Add";
			//popUpState.OnRefresh += PopUpState_OnRefresh;
			//popUpState.InputParameters = popUpInput;
			//popUpState.IsVisible = true;	
			popUpState.Show();
		}

		public void OnEdit()
		{
			EditItem();
		}

		private void EditItem()
		{
			if (this.CurrentItemRow == null)
				return;

			PopUpInput popUpInput = popUpState.InputParameters;
			popUpInput.FilterType = this.ItemsFilterType;
			popUpInput.Id = this.CurrentItemRow.Id;
			popUpInput.dialogType = typeof(Maklak.Client.Web.Controls.Filter.ItemEditor);
			popUpInput.Height = 120;
			popUpInput.Width = 300;
			popUpInput.Title = "Edit";

			//popUpState.IsVisible = true;
			popUpState.Show();
		}

		public void OnDelete() 
		{
			DeleteItem();
		}

		private void DeleteItem()
		{
			//ItemsTreeDS.ItemsRow currentSelectedRow = this.Items.FirstOrDefault(r => r.IsSelected);

			if (this.CurrentItemRow == null)
				return;

			serviceProxy.DeleteItem(ItemsFilterType, this.CurrentItemRow.Id);

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