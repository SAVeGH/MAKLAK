using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase.
using Maklak.Client.Service;
using Maklak.Client.DataSets;

namespace Maklak.Client.Web.Models.PopUp
{
	public class PopUpScreenModel : ComponentBase
	{
		[Inject]
		public PopUpStateModel PopUpState { get; set; }		
		
		protected override void OnInitialized()
		{
			base.OnInitialized();

			PopUpState.OnRefresh += PopUpState_Update;
		}

		private void PopUpState_Update()
		{
			this.InvokeAsync(StateHasChanged); // Только асинхронно т.к. выполняется не в контексте вызова 			
		}

		public void ClosePopUp(bool isCancel = false) 
		{			
			PopUpState.Close(isCancel);
		}

		public string Height { get { return $"{PopUpState.InputParameters.Height.ToString()}px"; } }

		public string Width { get { return $"{PopUpState.InputParameters.Width.ToString()}px"; } }

		public string Title { get { return PopUpState.InputParameters.Title; } }

		public string VisibilityClass 
		{
			get 
			{
				return this.PopUpState.IsVisible ? "popUpShow" : "popUpHide";
			}
		}


	}
}
