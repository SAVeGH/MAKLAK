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

		public override void EditItem()
		{
			if (this.CurrentItemRow == null)
				return;

			PopUpInput popUpInput = popUpState.InputParameters;
			popUpInput.FilterType = this.CurrentItemRow.ItemType;

			popUpInput.SetDataRow(this.CurrentItemRow);
			popUpInput.dialogType = typeof(Maklak.Client.Web.Controls.Filter.ItemEditor);
			popUpInput.Height = 120;
			popUpInput.Width = 300;
			popUpInput.Title = "Edit";
			popUpState.OnClose += PopUpState_EditCategoryItemComplete;

			popUpState.Show();
		}

		public void PopUpState_EditCategoryItemComplete()
		{
			serviceProxy.EditItem(popUpState.InputParameters.FilterType, popUpState.InputParameters.Row);

			PrepareLoadRequestDataAfterEdit();

			LoadItems(popUpState.InputParameters.Row);
		}

		private void PopUpState_AddCategoryItemComplete()
		{
			serviceProxy.AddCategoryItem(popUpState.InputParameters.Row);

			PrepareLoadRequestDataAfterAdd();

			base.LoadItems(popUpState.InputParameters.Row);
		}

		private void PrepareLoadRequestDataAfterAdd()
		{
			//если Id null - добавление category в root
			int? parentId = null;

			// если id не null - добавление к другой category
			if (!popUpState.InputParameters.Row.IsIdNull())
				if (popUpState.InputParameters.Row.IsOpened)
				{
					// если строка развернута - то она является parent-ом для получаемой ветки дерева
					parentId = popUpState.InputParameters.Row.Id;
				}
				else
				{
					// если строка свернута - то нужно взять её parent и вывети ветку дерева в котором она сама
					parentId = popUpState.InputParameters.Row.Parent_Id;
					popUpState.InputParameters.Row.Id = parentId ?? int.MaxValue; // id определяет от какого узла вниз будет зачистка. Для закрытого узла это ветка его же парента


				}

			// установить Parent_Id если он не null
			if (parentId == null || parentId == int.MaxValue)
			{
				popUpState.InputParameters.Row.SetParent_IdNull();
				popUpState.InputParameters.Row.SetIdNull(); // зачистятся все узлы и запрос уйдет NULL,NULL
			}
			else
				popUpState.InputParameters.Row.Parent_Id = parentId ?? int.MaxValue;
		}

		private void PrepareLoadRequestDataAfterEdit()
		{
			//если Id null - добавление category в root
			int? parentId = null;

			// если строка свернута - то нужно взять её parent и вывети ветку дерева в котором она сама
			parentId = popUpState.InputParameters.Row.Parent_Id;
			popUpState.InputParameters.Row.Id = parentId ?? int.MaxValue; // id определяет от какого узла вниз будет зачистка. Для закрытого узла это ветка его же парента

			// установить Parent_Id если он не null
			if (parentId == null || parentId == int.MaxValue)
			{
				popUpState.InputParameters.Row.SetParent_IdNull();
				popUpState.InputParameters.Row.SetIdNull(); // зачистятся все узлы и запрос уйдет NULL,NULL
			}
			else
				popUpState.InputParameters.Row.Parent_Id = parentId ?? int.MaxValue;
		}

		public override void DeleteItem()
		{
			if (this.CurrentItemRow == null)
				return;

			ItemsTreeRowHelper rowHelper = new ItemsTreeRowHelper(this.CurrentItemRow);

			serviceProxy.DeleteItem(rowHelper.Row.ItemType, rowHelper.Row.Id);

			//если Id null - добавление category в root
			int? parentId = null;

			// если строка свернута - то нужно взять её parent и вывети ветку дерева в котором она сама
			parentId = rowHelper.Row.Parent_Id;
			rowHelper.Row.Id = parentId ?? int.MaxValue; // id определяет от какого узла вниз будет зачистка. Для закрытого узла это ветка его же парента

			// установить Parent_Id если он не null
			if (parentId == null || parentId == int.MaxValue)
			{
				rowHelper.Row.SetParent_IdNull();
				rowHelper.Row.SetIdNull(); // зачистятся все узлы и запрос уйдет NULL,NULL
			}
			else
				rowHelper.Row.Parent_Id = parentId ?? int.MaxValue;
					

			LoadItems(rowHelper.Row);
		}
		
	}
}
