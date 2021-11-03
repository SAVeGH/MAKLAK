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
			// SelectedMeasureId не нужен т.к. есть в строке
			serviceProxy.AddPropertyItem(popUpState.InputParameters.Row);

			//popUpState.InputParameters.Row

			base.LoadItems(popUpState.InputParameters.Row);
		}
	}
}
