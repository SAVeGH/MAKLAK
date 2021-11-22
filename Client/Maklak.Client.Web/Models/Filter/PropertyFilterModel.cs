﻿using System;
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
			popUpState.OnClose += PopUpState_EditItemComplete;

			popUpState.Show();
		}

		public void PopUpState_EditItemComplete()
		{
			serviceProxy.EditItem(popUpState.InputParameters.FilterType, popUpState.InputParameters.Row);

			PrepareLoadRequestDataAfterEdit();

			LoadItems(popUpState.InputParameters.Row);
		}

		private void PopUpState_AddItemComplete()
		{
			
			serviceProxy.AddPropertyItem(popUpState.InputParameters.Row);


			PrepareLoadRequestDataAfterAdd();


			base.LoadItems(popUpState.InputParameters.Row);
		}

		protected override void AfterLoad() 
		{
			foreach(ItemsTreeDS.ItemsRow row in this.Items.Where(i => !i.IsItemTypeNull() && i.ItemType == "Property"))
				row.IsCheckable = false;
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

		private void PrepareLoadRequestDataAfterEdit()
		{
			if (popUpState.InputParameters.Row.ItemType == "Property")
			{
				popUpState.InputParameters.Row.SetIdNull(); // выбирать все property, а не конкретный item
				popUpState.InputParameters.Row.SetParent_IdNull(); // зачистка всех узлов
			}

			if (popUpState.InputParameters.Row.ItemType == "PropertyValue") 
			{
				popUpState.InputParameters.Row.ItemType = "Property"; // родительский тип для PropertyValue
				// установить Id property (root) от которого будут отбираться дочерние узлы. От этого же Id будет зачистка узлов.
				popUpState.InputParameters.Row.Id = popUpState.InputParameters.Row.Parent_Id;
			}			
		}

		public override void DeleteItem() 
		{
			if (this.CurrentItemRow == null)
				return;

			ItemsTreeRowHelper rowHelper = new ItemsTreeRowHelper(this.CurrentItemRow);

			serviceProxy.DeleteItem(rowHelper.Row.ItemType, rowHelper.Row.Id);

			if (rowHelper.Row.ItemType == "Property") 
			{
				rowHelper.Row.SetIdNull();
				rowHelper.Row.SetParent_IdNull();
			}

			if (rowHelper.Row.ItemType == "PropertyValue") 
			{
				rowHelper.Row.ItemType = rowHelper.Row.ParentItemType;
				rowHelper.Row.Id = rowHelper.Row.Parent_Id;
			}

			LoadItems(rowHelper.Row);
		}
	}
}
