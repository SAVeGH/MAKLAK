using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase
using Maklak.Client.Service;


namespace Maklak.Client.Web.Models
{
	
	public class FilterModel : ComponentBase // component inherits from this class. To do it  - class must be inherited from ComponentBase
	{
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

				List<string> list = this.SearchList.Where(s => s == this.SearchText).ToList(); //new List<string>() { "3", "4", "5" };

				this.SearchList = list;

				//this.StateHasChanged();
			}
		}

		[Parameter]
		public List<string> SearchList { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();

			SearchList = new List<string>() {"1","2","3" };
		}
	}
}
