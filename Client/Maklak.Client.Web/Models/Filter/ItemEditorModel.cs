using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components; // for ComponentBase.
using Maklak.Client.Service;
using Maklak.Client.DataSets;
using Maklak.Client.Web.Models.PopUp;


namespace Maklak.Client.Web.Models.Filter
{
	public class ItemEditorModel : ComponentBase
	{
		[Inject]
		public PopUpStateModel PopUpState { get; set; }

		[Inject]
		protected grpcProxy serviceProxy { get; set; }

		[Parameter]
		public string FilterType { get; set; }


		[Parameter]
		public bool IsTreeMode { get; set; }

		protected override void OnInitialized()
		{
			Init();
		}

		private void Init() 
		{
			//PopUpState.OnClose += PopUpState_OnClose;

			if (PopUpState.InputParameters.Row == null || PopUpState.InputParameters.Row.IsIdNull())
				return;

			// для Edit режима	

			ItemsTreeDS itemData = serviceProxy.Search(PopUpState.InputParameters.FilterType, PopUpState.InputParameters.Row, null);			
			Text = itemData.Items.FirstOrDefault().Name;
		}

		//private void PopUpState_OnClose()
		//{
		//	if (PopUpState.InputParameters.Row == null) 
		//		serviceProxy.AddItem(PopUpState.InputParameters.FilterType, Text);
		//	else
		//		serviceProxy.EditItem(PopUpState.InputParameters.FilterType, PopUpState.InputParameters.Row, Text);
			
		//}

		[Parameter]
		public string Text 
		{
			get
			{
				if (PopUpState.InputParameters.Row == null || PopUpState.InputParameters.Row.IsNameNull())
					return null;

				return PopUpState.InputParameters.Row.Name;
			}
			set
			{
				if (PopUpState.InputParameters.Row == null)
					return;

				PopUpState.InputParameters.Row.Name = value;
			}
		}
	}
}
