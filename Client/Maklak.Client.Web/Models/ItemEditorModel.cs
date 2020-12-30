using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase.
using Maklak.Client.Service;
using Maklak.Client.DataSets;


namespace Maklak.Client.Web.Models
{
	public class ItemEditorModel : ComponentBase
	{
		[Inject]
		public PopUpStateModel PopUpState { get; set; }

		[Inject]
		private grpcProxy serviceProxy { get; set; }

		[Parameter]
		public string FilterType { get; set; }


		[Parameter]
		public bool IsTreeMode { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();

			PopUpState.OnClose += PopUpState_OnClose;
		}

		private void PopUpState_OnClose()
		{
			serviceProxy.AddItem(PopUpState.InputParameters.FilterType, Text);
			//string res = this.Text;
		}

		[Parameter]
		public string Text 
		{
			get;
			//{
			//	return PopUpState.OutputParameters.Value;
			//}
			set;
			//{
			//	PopUpState.OutputParameters.Value = value;
			//}
		}
	}
}
