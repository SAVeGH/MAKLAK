﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Maklak.Service.DataSets;

using Maklak.Service.Data.Helpers;

namespace Maklak.Service.Data
{
	public static class Items
	{
		

		public static ItemsTreeDS GetItems(string itemType, int? itemId, int? parentItemId, string itemValue) 
		{
			IDbCommand command = SqlHelper.GetDbCommand("sp_GetItems");

			command.AddInParameter("@ItemId", DbType.Int32, (itemId == null ? DBNull.Value : (object)itemId));
			command.AddInParameter("@ParentItemId", DbType.Int32, (parentItemId == null ? DBNull.Value : (object)parentItemId));
			command.AddInParameter("@ItemType", DbType.String, 20, itemType);
			command.AddInParameter("@ItemValue", DbType.String, 100, itemValue);

			ItemsTreeDS dataSet = new ItemsTreeDS();

			SqlHelper.FillDataTable(command, dataSet.Items);

			//ItemsTreeDS dataSet = SqlHelper.ExecuteDataset<ItemsTreeDS>(command);

			return dataSet;
		}

		public static int AddItem(string itemType, int? itemId, int? measureUnitId, string itemValue)
		{
			IDbCommand command = SqlHelper.GetDbCommand("sp_AddItem");

			command.AddInParameter("@ItemType", DbType.String, 20, itemType);
			command.AddInParameter("@ItemValue", DbType.String, 100, itemValue);
			command.AddInParameter("@ItemId", DbType.Int32, itemId?? (object)DBNull.Value);
			command.AddInParameter("@MeasureUnitId", DbType.Int32, measureUnitId?? (object)DBNull.Value);

			object id = SqlHelper.ExecuteScalar(command);

			return Convert.ToInt32(id);
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

		public static LookupDS GetLookupItems(string lookupType)
		{
			IDbCommand command = SqlHelper.GetDbCommand("sp_GetLookupItems");
			
			command.AddInParameter("@LookupType", DbType.String, 20, lookupType);

			LookupDS dataSet = new LookupDS();

			SqlHelper.FillDataTable(command, dataSet.Items);

			//ItemsTreeDS dataSet = SqlHelper.ExecuteDataset<ItemsTreeDS>(command);

			return dataSet;
		}
	}
}
