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
			set 
			{ 
				searchText = value;

				StateStorage.AddSearchValue(this.ItemsFilterType, this.SearchText);

				serviceProxy.Search(StateStorage.SearchData);

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
		public FilterDS.ItemsDataTable SearchResult { get { return StateStorage.SearchData.Items; } }

		public string ItemsFilterType 
		{
			get; set;
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();

			//SearchList = new List<string>() {"1","2","3" };
			FilterDS.ItemsRow row = SearchResult.NewItemsRow();
			row.ItemId = 1;
			row.ItemValue = "1";
			row.Name = this.ItemsFilterType;
			SearchResult.AddItemsRow(row);

			row = SearchResult.NewItemsRow();
			row.ItemId = 2;
			row.ItemValue = "2";
			row.Name = this.ItemsFilterType;
			SearchResult.AddItemsRow(row);
		}
	}
}
