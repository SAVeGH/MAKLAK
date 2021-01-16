using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase.
using Maklak.Client.Service;
using Maklak.Client.DataSets;

using Maklak.Client.Web.Controls.Filter;

namespace Maklak.Client.Web.Models.PopUp
{
	public class PopUpContentModel : ComponentBase
	{
		[Inject]
		public PopUpStateModel PopUpState { get; set; }		

		private void PopUpState_OnRefresh()
		{
			this.InvokeAsync(StateHasChanged); // Иначе содержимое не отображается		
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();

			PopUpState.OnRefresh += PopUpState_OnRefresh;
		}

		public RenderFragment Content 
		{ 
			get 
			{				
				RenderFragment content = PopUpState.InputParameters.dialogType == null ? null : new RenderFragment(x => { x.OpenComponent(1, PopUpState.InputParameters.dialogType); x.CloseComponent(); });
				return content; 
			} 
		}		

	}
}
