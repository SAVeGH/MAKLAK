using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.DataAccess.DataSets;

namespace Maklak.DataAccess
{
	public static class NodeBuilder
	{
		//public static NodeDS FillNode(NodeDS NodeDS)
		//{
		//	NodeDS ds = new NodeDS();

		//	NodeDS.TreeRow row = ds.Tree.NewTreeRow();
		//	NodeDS.TreeRow parentRow = null;

		//	row.Id = 1;
		//	row.Name = "root";
		//	row.Branch_Id = 1;
		//	//row.		
		//	parentRow = row;
		//	ds.Tree.AddTreeRow(row);

		//	row = ds.Tree.NewTreeRow();
		//	row.Id = 2;
		//	row.Parent_Id = parentRow.Id;
		//	row.Name = "Name";
		//	row.Branch_Id = 2; // PRODUCT
		//	row.ParentBranch_Id = 1;
		//	ds.Tree.AddTreeRow(row);

		//	row = ds.Tree.NewTreeRow();
		//	row.Id = 3;
		//	row.Parent_Id = parentRow.Id;
		//	row.Name = "Category";
		//	row.Branch_Id = 3; // CATEGORY
		//	row.ParentBranch_Id = 1;
		//	ds.Tree.AddTreeRow(row);

		//	row = ds.Tree.NewTreeRow();
		//	row.Id = 4;
		//	row.Parent_Id = parentRow.Id;
		//	row.Name = "Producer";
		//	row.Branch_Id = 4; // PRODUCER
		//	row.ParentBranch_Id = 1;
		//	ds.Tree.AddTreeRow(row);

		//	row = ds.Tree.NewTreeRow();
		//	row.Id = 5;
		//	row.Parent_Id = parentRow.Id;
		//	row.Name = "Property";
		//	row.Branch_Id = 5; // PROPERTY
		//	row.ParentBranch_Id = 1;
		//	ds.Tree.AddTreeRow(row);

		//	row = ds.Tree.NewTreeRow();
		//	row.Id = 6;
		//	row.Parent_Id = parentRow.Id;
		//	row.Name = "Tag";
		//	row.Branch_Id = 6; // TAG
		//	row.ParentBranch_Id = 1;
		//	ds.Tree.AddTreeRow(row);

		//	return ds;
		//}

		public static NodeDS GetNode(NodeDS inputNodeDS)
		{
			NodeDS ds = DBMock();
			// всегда одна строка
			NodeDS.RootNodeDataRow inputRow = inputNodeDS.RootNodeData[0];

			int itemId = inputRow.Item_Id;
			int itemBranchId = inputRow.ItemBranch_Id;
			//находим строку от которой стартовать
			NodeDS.TreeRow rootRow = ds.Tree.Where(r => r.Item_Id == itemId && r.ItemBranch_Id == itemBranchId).FirstOrDefault();

			NodeDS outputDS = new NodeDS();			
			// заполняем узел и вложенные узлы
			FillNode(ds, inputNodeDS, outputDS, rootRow);

			return outputDS;

		}

		private static void FillNode(NodeDS dbDS, NodeDS inputDS, NodeDS outputDS,NodeDS.TreeRow rootRow)
		{
			if (rootRow != null)
			{
				bool hasChildNodes = dbDS.Tree.Any(r => !r.IsParent_IdNull() && r.Parent_Id == rootRow.Id);
				rootRow.HasChildNodes = hasChildNodes;
				outputDS.Tree.ImportRow(rootRow);

				int itemId = rootRow.Item_Id;
				int itemBranchId = rootRow.ItemBranch_Id;
				// проверка по списку открытых узлов
				bool isRowOpened = inputDS.Tree.Any(r => r.Item_Id == itemId && r.ItemBranch_Id == itemBranchId);

				if (!isRowOpened && !rootRow.IsParent_IdNull() /*верхний узел всегда открыт*/)
					return;// если узел не открыт - не заполняем данные
			}			

			foreach (NodeDS.TreeRow row in dbDS.Tree.Where(r=> !r.IsParent_IdNull() && r.Parent_Id == rootRow.Id))
			{
				FillNode(dbDS, inputDS, outputDS, row);
			}
		}

		private static NodeDS DBMock()
		{
			
			NodeDS ds = new NodeDS();

			NodeDS.TreeRow row = ds.Tree.NewTreeRow();

			row.Item_Id = 0;
			row.ItemBranch_Id = 0;
			row.Name = "root";
			ds.Tree.AddTreeRow(row);

			NodeDS.TreeRow parentRow = row;

			row = ds.Tree.NewTreeRow();
			
			row.Parent_Id = parentRow.Id;
			row.Name = "Name";
			row.Item_Id = 0;
			row.ItemBranch_Id = 1; // PRODUCT			
			ds.Tree.AddTreeRow(row);

			NodeDS.TreeRow productRow = row;
			/*
			row = ds.Tree.NewTreeRow();
			row.Item_Id = 0;
			row.Parent_Id = parentRow.Id;
			row.Name = "Category";
			row.ItemBranch_Id = 2; // CATEGORY			
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Item_Id = 0;
			row.Parent_Id = parentRow.Id;
			row.Name = "Producer";
			row.ItemBranch_Id = 3; // PRODUCER			
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Item_Id = 0;
			row.Parent_Id = parentRow.Id;
			row.Name = "Property";
			row.ItemBranch_Id = 4; // PROPERTY			
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Item_Id = 0;
			row.Parent_Id = parentRow.Id;
			row.Name = "Tag";
			row.ItemBranch_Id = 5; // TAG			
			ds.Tree.AddTreeRow(row);
			*/
			row = ds.Tree.NewTreeRow();
			row.Item_Id = 1;
			row.Parent_Id = productRow.Id;
			row.Name = "Nail";
			row.ItemBranch_Id = 1;			
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Item_Id = 2;
			row.Parent_Id = productRow.Id;
			row.Name = "Car";
			row.ItemBranch_Id = 1;			
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Item_Id = 3;
			row.Parent_Id = productRow.Id;
			row.Name = "Ship";
			row.ItemBranch_Id = 1;			
			ds.Tree.AddTreeRow(row);

			return ds;
		}

		//private static NodeDS DBMock()
		//{
		//	//TODO: Нужна сквозная автонумерация узлов и связь по Parent_Id
		//	NodeDS ds = new NodeDS();

		//	NodeDS.TreeRow row = ds.Tree.NewTreeRow();
		//	//NodeDS.TreeRow parentRow = null;

		//	//row.Id = 0;
		//	row.Branch_Id = 0;
		//	row.Name = "root";
		//	ds.Tree.AddTreeRow(row);

		//	row = ds.Tree.NewTreeRow();
		//	row.Id = 0;
		//	//row.Parent_Id = parentRow.Id;
		//	row.Name = "Name";
		//	row.Branch_Id = 1; // PRODUCT
		//	row.ParentBranch_Id = 0;
		//	ds.Tree.AddTreeRow(row);

		//	row = ds.Tree.NewTreeRow();
		//	row.Id = 0;
		//	//row.Parent_Id = parentRow.Id;
		//	row.Name = "Category";
		//	row.Branch_Id = 2; // CATEGORY
		//	row.ParentBranch_Id = 0;
		//	ds.Tree.AddTreeRow(row);

		//	row = ds.Tree.NewTreeRow();
		//	row.Id = 0;
		//	//row.Parent_Id = parentRow.Id;
		//	row.Name = "Producer";
		//	row.Branch_Id = 3; // PRODUCER
		//	row.ParentBranch_Id = 0;
		//	ds.Tree.AddTreeRow(row);

		//	row = ds.Tree.NewTreeRow();
		//	row.Id = 0;
		//	//row.Parent_Id = parentRow.Id;
		//	row.Name = "Property";
		//	row.Branch_Id = 4; // PROPERTY
		//	row.ParentBranch_Id = 0;
		//	ds.Tree.AddTreeRow(row);

		//	row = ds.Tree.NewTreeRow();
		//	row.Id = 0;
		//	//row.Parent_Id = parentRow.Id;
		//	row.Name = "Tag";
		//	row.Branch_Id = 5; // TAG
		//	row.ParentBranch_Id = 0;
		//	ds.Tree.AddTreeRow(row);

		//	row = ds.Tree.NewTreeRow();
		//	row.Id = 1;
		//	row.Parent_Id = 0;
		//	row.Name = "Nail";
		//	row.Branch_Id = 1;
		//	row.ParentBranch_Id = 1;
		//	ds.Tree.AddTreeRow(row);

		//	row = ds.Tree.NewTreeRow();
		//	row.Id = 2;
		//	row.Parent_Id = 0;
		//	row.Name = "Car";
		//	row.Branch_Id = 1;
		//	row.ParentBranch_Id = 1;
		//	ds.Tree.AddTreeRow(row);

		//	row = ds.Tree.NewTreeRow();
		//	row.Id = 3;
		//	row.Parent_Id = 0;
		//	row.Name = "Ship";
		//	row.Branch_Id = 1;
		//	row.ParentBranch_Id = 1;
		//	ds.Tree.AddTreeRow(row);
		// GH test

		//	return ds;
		//}
	}
}
