using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase.
using Maklak.Client.Service;
using Maklak.Client.DataSets;

namespace Maklak.Client.Web.Models
{
	public class PopUpModel : ComponentBase
	{
		[Inject]
		public PopUpStateModel PopUpState { get; set; }

		//[Parameter]
		//public bool IsVisible { get { return popUpState.IsVisible; } set { popUpState.IsVisible = value; } }

		
		protected override void OnInitialized()
		{
			base.OnInitialized();

			PopUpState.OnRefresh += PopUpState_Update;
		}

		private void PopUpState_Update()
		{
			this.InvokeAsync(StateHasChanged); // Только асинхронно т.к. выполняется не в контексте вызова 			
		}

		public void HidePopUp() 
		{
			PopUpState.IsVisible = false;
			//PopUpState.Update();
		}

	}
}
