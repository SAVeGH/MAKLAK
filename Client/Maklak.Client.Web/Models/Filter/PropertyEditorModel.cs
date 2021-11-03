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
	public class PropertyEditorModel : ItemEditorModel
	{

		public LookupDS MeasureUnits { get; set; }

		protected override void OnInitialized()
		{
			//base.OnInitialized();

			Init();
		}

		private void Init() 
		{
			FillLookup();

			//PopUpState.OnClose += PopUpState_OnClose;

			//if (PopUpState.InputParameters.Id == null)
			//	return;
		}

		private void FillLookup()
		{
			MeasureUnits = new LookupDS();

			base.serviceProxy.GetLookupItems("MeasureUnits", MeasureUnits);

			if (PopUpState.InputParameters.Row == null)
				return;			

			if (PopUpState.InputParameters.Row.IsMeasureUnit_IdNull())
				return;

			// Делается Add с заселекченой строкой или Edit

			int selectedMeasureId = (int)PopUpState.InputParameters.Row.MeasureUnit_Id;

			LookupDS.ItemsRow measureRow = MeasureUnits.Items.Where(mu => mu.Id == selectedMeasureId).FirstOrDefault();

			measureRow.IsSelected = true;			

			SelectedMeasureId = selectedMeasureId;
		}

		//private void PopUpState_OnClose()
		//{
		//	if (PopUpState.InputParameters.PopUpAction == PopUpInput.ActionType.Add) 
		//	{				
		//		serviceProxy.AddPropertyItem(PopUpState.InputParameters.Row, SelectedMeasureId, Text);
		//	}

		//	if (PopUpState.InputParameters.PopUpAction == PopUpInput.ActionType.Edit)
		//	{
		//		serviceProxy.EditPropertyItem(PopUpState.InputParameters.Row, Text);
		//	}

		//	//if (PopUpState.InputParameters.Id == null)
		//	//	serviceProxy.AddPropertyItem(PopUpState.InputParameters.Id, SelectedMeasureId, Text);
		//	//else
		//	//	serviceProxy.EditPropertyItem(PopUpState.InputParameters.FilterType, null, PopUpState.InputParameters.Id, Text);
		//}

		//private int selectedMeasureId;
		public int SelectedMeasureId 
		{ 
			get 
			{
				if (PopUpState.InputParameters.Row.IsMeasureUnit_IdNull())
					return int.MinValue;

				return PopUpState.InputParameters.Row.MeasureUnit_Id; 
			}
			set 
			{
				PopUpState.InputParameters.Row.MeasureUnit_Id = value;

				OnSelectedMeasureChanged();
			}
		}

		public bool IsMeasureDisabled // толко для Add опреции
		{
			get
			{
				return !PopUpState.InputParameters.Row.IsIdNull();					
			}
			
		}


		public void OnSelectedMeasureChanged()
		{
			// сбрость текущий флаг selected и установить на новый record
			LookupDS.ItemsRow currentSelectedRow = this.MeasureUnits.Items.FirstOrDefault(r => r.IsSelected);

			if (currentSelectedRow != null)			
				currentSelectedRow.IsSelected = false;			

			LookupDS.ItemsRow row = this.MeasureUnits.Items.FirstOrDefault(r => r.Id == SelectedMeasureId);

			if (row == null)
				return;

			row.IsSelected = true;
		}
	}
}
