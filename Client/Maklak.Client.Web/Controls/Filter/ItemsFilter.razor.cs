using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maklak.Client.Web.Models;

using Microsoft.AspNetCore.Components; // for [Parameter] attribute

namespace Maklak.Client.Web.Controls.Filter
{
	public partial class ItemsFilter : Maklak.Client.Web.Models.FilterModel
	{
		//[Inject]
		//public StateModel StateStorage { get; set; }

		[Parameter]
		public string FilterType { get { return this.ItemsFilterType; } set { this.ItemsFilterType = value; } }

		private void Search_OnInput() 
		{
			int u = 0;
			//List<string> list = this.SearchList.Where(s => s == this.SearchText).ToList(); //new List<string>() { "3", "4", "5" };

			//StateStorage.AddSearchValue(this.FilterType, this.SearchText);

			//this.SearchList = list;

			//this.SearchText = "123";
		}
	}
}
