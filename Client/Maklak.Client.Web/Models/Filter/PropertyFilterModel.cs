using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Maklak.Client.Service;
using Maklak.Client.DataSets;
using Maklak.Client.Web.Models.PopUp;

namespace Maklak.Client.Web.Models.Filter
{
	public class PropertyFilterModel : ItemsFilterBase
	{
		public PropertyFilterModel(PopUpStateModel popUpStateModel, grpcProxy srvProxy) : base(popUpStateModel, srvProxy) 
		{		

		}

		public override void AddItem()
		{
			if (base.CurrentItemRow != null && !base.CurrentItemRow.IsItemTypeNull() && base.CurrentItemRow.ItemType == "PropertyValue")
				return;

			PopUpInput popUpInput = popUpState.InputParameters;
			popUpInput.FilterType = this.ItemsFilterType;
			popUpInput.dialogType = typeof(Maklak.Client.Web.Controls.Filter.PropertyEditor);
			popUpInput.Height = 120;
			popUpInput.Width = 300;
			popUpInput.Title = "Add";
			popUpInput.SetDataRow(base.CurrentItemRow);
			popUpInput.Row.Name = null; // удалить name из копии строки
			popUpInput.Row.ItemType = popUpInput.Row.IsIdNull() ? this.ItemsFilterType : popUpInput.Row.ItemType;
			popUpInput.PopUpAction = PopUpInput.ActionType.Add;
			popUpState.OnClose += PopUpState_AddItemComplete;

			popUpState.Show();
		}

		private void PopUpState_AddItemComplete()
		{
			
			serviceProxy.AddPropertyItem(popUpState.InputParameters.Row);


			PrepareLoadRequestData();


			base.LoadItems(popUpState.InputParameters.Row);
		}

		private void PrepareLoadRequestData() 
		{
			//если Id null - добавление property в root
			int? parentId = null;

			// если id не null - добавление propertyvalue
			if (!popUpState.InputParameters.Row.IsIdNull())
				if (popUpState.InputParameters.Row.IsOpened)
				{
					// если строка развернута
					parentId = popUpState.InputParameters.Row.Id;
				}
				else
				{
					// если строка свернута
					parentId = popUpState.InputParameters.Row.Parent_Id;
					popUpState.InputParameters.Row.SetIdNull();
				}

			// установить Parent_Id если он не null
			if (parentId == null || parentId == int.MaxValue)
				popUpState.InputParameters.Row.SetParent_IdNull();
			else
				popUpState.InputParameters.Row.Parent_Id = parentId ?? int.MaxValue;
		}
	}
}
