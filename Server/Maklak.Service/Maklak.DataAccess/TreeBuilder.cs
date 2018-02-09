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
		public static TreeDS ConstructTree(TreeDS treeDS)
		{

			TreeDS ds = new TreeDS();

			TreeDS.TreeRow row = ds.Tree.NewTreeRow();
			TreeDS.TreeRow parentRow = null;

			row.Id = 1;
			row.Name = "root";
			row.Key = "ROOT";		
			parentRow = row;
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Id = 2;
			row.Parent_Id = parentRow.Id;
			row.Name = "Name";
			row.Key = "PRODUCT";
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Id = 3;
			row.Parent_Id = parentRow.Id;
			row.Name = "Producer";
			row.Key = "PRODUCER";
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Id = 4;
			row.Parent_Id = parentRow.Id;
			row.Name = "Property";
			row.Key = "PROPERTY";
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Id = 5;
			row.Parent_Id = parentRow.Id;
			row.Name = "Tag";
			row.Key = "TAG";
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Id = 6;
			row.Parent_Id = 2;
			row.Name = "Nail";
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Id = 7;
			row.Parent_Id = 2;
			row.Name = "Car";
			ds.Tree.AddTreeRow(row);

			row = ds.Tree.NewTreeRow();
			row.Id = 8;
			row.Parent_Id = 2;
			row.Name = "Ship";
			ds.Tree.AddTreeRow(row);

			return ds;
		}
	}
}
