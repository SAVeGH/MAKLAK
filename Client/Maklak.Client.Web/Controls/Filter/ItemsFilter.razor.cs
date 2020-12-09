using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maklak.Client.Web.Models;

using Microsoft.AspNetCore.Components; // for [Parameter] attribute

namespace Maklak.Client.Web.Controls.Filter
{
	public partial class ItemsFilter : Maklak.Client.Web.Models.ItemsFilterModel
	{
		//[Inject]
		//public StateModel StateStorage { get; set; }

		//[Inject]
		//public PopUpStateModel popUpState { get; set; }

		//string filterClass;

		//[Parameter]
		//public string FilterType { get { return this.ItemsFilterType; } set { this.ItemsFilterType = value; } }

		//[Parameter]
		//public string FilterClass { get { return this.filterClass; } set { this.filterClass = value; } }

		//[Parameter]
		//public bool ShowRoot { get; set; }

		//private void Search_OnInput() 
		//{
		//	int u = 0;
		//	//List<string> list = this.SearchList.Where(s => s == this.SearchText).ToList(); //new List<string>() { "3", "4", "5" };

		//	//StateStorage.AddSearchValue(this.FilterType, this.SearchText);

		//	//this.SearchList = list;

		//	//this.SearchText = "123";
		//}

		//public async Task OnAdd()
		//public void OnAdd()
		//{
		//	PopUpInput popUpInput = new PopUpInput();
		//	popUpInput.FilterType = this.FilterType;
		//	popUpState.InputParameters = popUpInput;
		//	popUpState.IsVisible = true;

		//	//popUpState.Update();
		//	//this.AuthenticationStateProvider.NotifyAuthenticationStateChanged();

		//	//this.StateHasChanged(); // re-render the page
		//}
	}
}
