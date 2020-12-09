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
		private FilterItemsDS itemsDS;

		public ItemsFilterModel()
		{
			itemsDS = new FilterItemsDS();
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

				
				itemsDS.Input.Clear();

				FilterItemsDS.InputRow row = itemsDS.Input.NewInputRow();
				row.InputName = this.ItemsFilterType;
				row.InputValue = searchText;
				itemsDS.Input.AddInputRow(row);

				serviceProxy.Search(itemsDS);				

			}
		}
		
		public FilterItemsDS.ItemsDataTable Items { get { return itemsDS.Items; } }

		[Parameter]
		public string ItemsFilterType
		{
			get; set;
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();
			

			FilterItemsDS.ItemsRow row = null;

			for (int i = 0; i < 20; i++)
			{
				row = Items.NewItemsRow();
				row.ItemId = 1 + i;
				row.ItemValue = i.ToString() + " " + this.ItemsFilterType;
				//row.Name = this.ItemsFilterType;
				Items.AddItemsRow(row);
			}
		}

		public void OnAdd()
		{
			PopUpInput popUpInput = new PopUpInput();
			popUpInput.FilterType = this.ItemsFilterType;
			popUpState.InputParameters = popUpInput;
			popUpState.IsVisible = true;			
		}
	}
}
