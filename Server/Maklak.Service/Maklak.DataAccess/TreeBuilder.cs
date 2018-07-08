using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.DataAccess.DataSets;

namespace Maklak.DataAccess
{
	public static class TreeBuilder
	{
		//public static TreeDS FillNode(TreeDS treeDS)
		//{
		//	TreeDS ds = new TreeDS();

		//	TreeDS.TreeRow row = ds.Tree.NewTreeRow();
		//	TreeDS.TreeRow parentRow = null;

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

		public static TreeDS ConstructTree(TreeDS treeDS)
		{
			TreeDS ds = DBMock();

			TreeDS.RootNodeDataRow inputRow = treeDS.RootNodeData[0];

			int branchID = inputRow.IsBranchIDNull() ? 0 : inputRow.BranchID;
			int nodeID = inputRow.IsNodeIDNull() ? 0 : inputRow.NodeID;

			TreeDS.TreeRow rootRow = ds.Tree.Where(r => r.Branch_Id == branchID && r.Id == nodeID).FirstOrDefault();

			TreeDS outputDS = new TreeDS();

			//outputDS.Tree.ImportRow(rootRow);



			foreach (TreeDS.TreeRow row in ds.Tree.Where(r => (rootRow == null && r.IsParent_IdNull()) || (rootRow != null && !r.IsParent_IdNull() && r.Parent_Id == rootRow.Id && r.Branch_Id == rootRow.Branch_Id)))
			{
				outputDS.Tree.ImportRow(row);
			}
			

			return outputDS;

		}

		private static TreeDS DBMock()
		{
			TreeDS ds = new TreeDS();

			TreeDS.TreeRow row = null;//ds.Tree.NewTreeRow();
			//TreeDS.TreeRow parentRow = null;

			//row.Id = 0;
			//row.Branch_Id = 0;
			//row.Name = "root";			
			//ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Id = 0;
			//row.Parent_Id = parentRow.Id;
			row.Name = "Name";
			row.Branch_Id = 1; // PRODUCT
			//row.ParentBranch_Id = 1;
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Id = 0;
			//row.Parent_Id = parentRow.Id;
			row.Name = "Category";
			row.Branch_Id = 2; // CATEGORY
			//row.ParentBranch_Id = 1;
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Id = 0;
			//row.Parent_Id = parentRow.Id;
			row.Name = "Producer";
			row.Branch_Id = 3; // PRODUCER
			//row.ParentBranch_Id = 1;
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Id = 0;
			//row.Parent_Id = parentRow.Id;
			row.Name = "Property";
			row.Branch_Id = 4; // PROPERTY
			//row.ParentBranch_Id = 1;
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Id = 0;
			//row.Parent_Id = parentRow.Id;
			row.Name = "Tag";
			row.Branch_Id = 5; // TAG
			//row.ParentBranch_Id = 1;
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Id = 1;
			row.Parent_Id = 0;
			row.Name = "Nail";
			row.Branch_Id = 1;
			//row.ParentBranch_Id = 2;
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Id = 2;
			row.Parent_Id = 0;
			row.Name = "Car";
			row.Branch_Id = 1;
			//row.ParentBranch_Id = 2;
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Id = 3;
			row.Parent_Id = 0;
			row.Name = "Ship";
			row.Branch_Id = 1;
			//row.ParentBranch_Id = 2;
			ds.Tree.AddTreeRow(row);

			return ds;
		}
	}
}
