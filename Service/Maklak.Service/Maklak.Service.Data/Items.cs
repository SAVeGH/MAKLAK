using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Maklak.Service.DataSets;

using Maklak.Service.Data.Helpers;

namespace Maklak.Service.Data
{
	public static class Items
	{
		public static int AddItem(string itemType, string itemValue)
		{
			IDbCommand command = SqlHelper.GetDbCommand("sp_AddItem");

			command.AddInParameter("@ItemType", DbType.String, 20, itemType);
			command.AddInParameter("@ItemValue", DbType.String, 100, itemValue);

			object id = SqlHelper.ExecuteScalar(command);			

			return Convert.ToInt32(id);
		}

		public static ItemsTreeDS GetItems(string itemType, int itemId, string itemValue) 
		{
			IDbCommand command = SqlHelper.GetDbCommand("sp_GetItems");

			command.AddInParameter("@ItemId", DbType.Int32, itemId);
			command.AddInParameter("@ItemType", DbType.String, 20, itemType);
			command.AddInParameter("@ItemValue", DbType.String, 100, itemValue);

			ItemsTreeDS dataSet = new ItemsTreeDS();

			SqlHelper.FillDataTable(command, dataSet.Items);

			//ItemsTreeDS dataSet = SqlHelper.ExecuteDataset<ItemsTreeDS>(command);

			return dataSet;
		}

		public static int DeleteItem(string itemType, int itemId)
		{
			IDbCommand command = SqlHelper.GetDbCommand("sp_DeleteItem");

			command.AddInParameter("@ItemType", DbType.String, 20, itemType);
			command.AddInParameter("@ItemId", DbType.Int32, itemId);

			object id = SqlHelper.ExecuteScalar(command);

			return Convert.ToInt32(id);
		}

		public static int EditItem(string itemType, int itemId, string itemValue)
		{
			IDbCommand command = SqlHelper.GetDbCommand("sp_EditItem");

			command.AddInParameter("@ItemType", DbType.String, 20, itemType);
			command.AddInParameter("@ItemId", DbType.Int32, itemId);
			command.AddInParameter("@ItemValue", DbType.String, 100, itemValue);

			object id = SqlHelper.ExecuteScalar(command);

			return Convert.ToInt32(id);
		}
	}
}
