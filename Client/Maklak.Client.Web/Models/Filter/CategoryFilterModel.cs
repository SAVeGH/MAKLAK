using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Maklak.Client.Service;
using Maklak.Client.DataSets;
using Maklak.Client.Web.Models.PopUp;

namespace Maklak.Client.Web.Models.Filter
{
	public class CategoryFilterModel : ItemsFilterBase
	{
		public CategoryFilterModel(PopUpStateModel popUpStateModel, grpcProxy srvProxy) : base(popUpStateModel, srvProxy)
		{

		}

		public override void AddItem()
		{
			PopUpInput popUpInput = popUpState.InputParameters;
			popUpInput.FilterType = this.ItemsFilterType;
			popUpInput.dialogType = typeof(Maklak.Client.Web.Controls.Filter.ItemEditor);
			popUpInput.Height = 120;
			popUpInput.Width = 300;
			popUpInput.Title = "Add";
			popUpInput.SetDataRow(base.CurrentItemRow);
			popUpInput.Row.Name = null; // удалить name из копии строки
			popUpInput.Row.ItemType = this.ItemsFilterType;
			popUpInput.PopUpAction = PopUpInput.ActionType.Add;
			popUpState.OnClose += PopUpState_AddCategoryItemComplete;

			popUpState.Show();
		}

		private void PopUpState_AddCategoryItemComplete()
		{
			serviceProxy.AddCategoryItem(popUpState.InputParameters.Row);

			PrepareLoadRequestDataAfterAdd();

			base.LoadItems(popUpState.InputParameters.Row);
		}

		private void PrepareLoadRequestDataAfterAdd()
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
