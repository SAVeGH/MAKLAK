using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maklak.Client.DataSets;

namespace Maklak.Client.Web.Models.PopUp
{
	public class PopUpStateModel
	{
		private bool popUpState = false;

		public event Action OnRefresh;
		public event Action OnClose;

		public PopUpStateModel() 
		{
			InputParameters = new PopUpInput();			
		}
		public PopUpInput InputParameters { get; private set; }		

		public bool IsVisible
		{
			get
			{
				return popUpState;
			}			
		}

		public void Show() 
		{
			popUpState = true;

			this.OnRefresh?.Invoke();
		}

		public void Close(bool isCancel = false) 
		{
			popUpState = false;

			if(!isCancel)
				this.OnClose?.Invoke();

			CleanSubscriptions();

			InputParameters.Clear();

			this.OnRefresh?.Invoke();
		}

		private void CleanSubscriptions() 
		{
			// освобождает все подписки на событие
			Delegate[] subscriptions = this.OnClose.GetInvocationList();

			foreach (Delegate closeDelegate in subscriptions)			
				this.OnClose -= (Action)closeDelegate;			
		}
	}	

	public class PopUpInput
	{
		public enum ActionType { None, Add, Edit, Delete};

		//public int? Id;
		//public int? ParentId;
		//public string Value;
		//ItemsTreeDS.ItemsDataTable table;
		//ItemsTreeDS.ItemsRow innerRow;
		ItemsTreeRowHelper rowHelper;

		public string FilterType;		
		public Type dialogType;
		public int Height;
		public int Width;
		public string Title;
		public ActionType PopUpAction = ActionType.None;

		public PopUpInput() 
		{
			//table = new ItemsTreeDS.ItemsDataTable();
			rowHelper = new ItemsTreeRowHelper();
		}

		public ItemsTreeDS.ItemsRow Row 
		{
			get 
			{
				return rowHelper.Row;
			}
		}

		//public int? Id 
		//{ 
		//	get 
		//	{
		//		//if (innerRow == null)
		//		//	return null;

		//		//if (!innerRow.Table.Columns.Contains("Id"))
		//		//	return null;

		//		//DataColumn column = innerRow.Table.Columns["Id"];

		//		//if (innerRow[column] == DBNull.Value)
		//		//	return null;

		//		//return (int?)innerRow[column];

		//		if (innerRow == null)
		//			return null;

		//		return innerRow.Id;
		//	} 
		//}

		//public int? ParentId
		//{
		//	get
		//	{
		//		//if (innerRow == null)
		//		//	return null;

		//		//if (!innerRow.Table.Columns.Contains("Parent_Id"))
		//		//	return null;

		//		//DataColumn column = innerRow.Table.Columns["Parent_Id"];

		//		//if (innerRow[column] == DBNull.Value)
		//		//	return null;

		//		//return (int?)innerRow[column];

		//		if (innerRow == null)
		//			return null;

		//		return innerRow.Parent_Id;
		//	}
		//}

		public void SetDataRow(ItemsTreeDS.ItemsRow row) 
		{
			// делается копия строки (иначе Row out of table exception)
			rowHelper.Row = row;
			//table.ImportRow(row);
			//innerRow = table.FirstOrDefault();
			//innerRow = row;
			//if (row == null)
			//	return;

			//foreach (DataColumn column in row.Table.Columns) 
			//{
			//	DataColumn newColumn = new DataColumn(column.ColumnName, column.DataType);
			//	table.Columns.Add(newColumn);
			//}

			//innerRow = table.NewRow();

			//foreach (DataColumn column in row.Table.Columns)
			//{
			//	innerRow[column.ColumnName] = row[column.ColumnName];				
			//}

			//table.Rows.Add(innerRow);
		}


		public void Clear() 
		{
			//Id = null;			
			//Value = null;
			//table = new ItemsTreeDS.ItemsDataTable();
			//innerRow = null;
			rowHelper.Clear();
			FilterType = null;
			dialogType = null;
		}
	}

}
