using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase
using Maklak.Client.Service;
using Maklak.Client.DataSets;

namespace Maklak.Client.Web.Models
{
	
	public class FilterModel : ComponentBase // component inherits from this class. To do it  - class must be inherited from ComponentBase
	{
		private FilterItemsDS itemsDS;

		public FilterModel() 
		{
			itemsDS = new FilterItemsDS();
		}

		[Inject]
		public StateModel StateStorage { get; set; }

		[Inject]
		private grpcProxy serviceProxy { get; set; }

		//[Parameter]
		//public string UserLogin { get; set; }

		private string searchText;

		[Parameter]
		public string SearchText 
		{
			get { return searchText; }
			set // устанавливается на oninput
			{ 
				searchText = value;

				//StateStorage.AddSearchValue(this.ItemsFilterType, this.SearchText);
				itemsDS.Input.Clear();

				FilterItemsDS.InputRow row = itemsDS.Input.NewInputRow();
				row.InputName = this.ItemsFilterType;
				row.InputValue = searchText;
				itemsDS.Input.AddInputRow(row);

				serviceProxy.Search(itemsDS);

				//List<string> list = this.SearchList.Where(s => s == this.SearchText).ToList(); //new List<string>() { "3", "4", "5" };

				//this.SearchList = list;

				//this.StateHasChanged();

				//List<string> list = new List<string>();

				//foreach () { }
				
			}
		}

		[Parameter]
		public List<string> SearchList { get; set; }

		//[Parameter]
		public FilterItemsDS.ItemsDataTable Items { get { return itemsDS.Items; } }

		public string ItemsFilterType 
		{
			get; set;
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();

			//SearchList = new List<string>() {"1","2","3" };
			//FilterItemsDS.ItemsRow row = Items.NewItemsRow();
			//row.ItemId = 1;
			//row.ItemValue = "1" + this.ItemsFilterType;
			////row.Name = this.ItemsFilterType;
			//Items.AddItemsRow(row);

			//row = Items.NewItemsRow();
			//row.ItemId = 2;
			//row.ItemValue = "2" + this.ItemsFilterType;
			////row.Name = this.ItemsFilterType;
			//Items.AddItemsRow(row);

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
	}
}
