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

			//если Id null - добавление property в root
			int? parentId = null;

			// если id не null - добавление propertyvalue
			if (!popUpState.InputParameters.Row.IsIdNull())			
				parentId = popUpState.InputParameters.Row.IsOpened ? popUpState.InputParameters.Row.Id : popUpState.InputParameters.Row.Parent_Id;

			if (parentId == null)
				popUpState.InputParameters.Row.SetParent_IdNull();
			else
				popUpState.InputParameters.Row.Parent_Id = parentId??int.MaxValue;

			popUpState.InputParameters.Row.SetIdNull();
			//base.Toggle(popUpState.InputParameters.Row);
			base.LoadItems(popUpState.InputParameters.Row);
		}
	}
}
