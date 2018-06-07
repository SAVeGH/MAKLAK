using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.Client.Data;
using Maklak.Client.DataSets;


namespace Maklak.Client.Models
{
    public class TreeModel : BaseModel
    {
        TreeNodeModel rootNode;
		//Maklak.Client.Data.DataSource dataSource; 

		public TreeModel()
        {
            
            this.OnModelReady += TreeModel_OnModelReady;

			//dataSource = new DataSource(this.data);

		}

        private void TreeModel_OnModelReady()
        {
			dataSource = new DataSource(this.data);

			FillData();

            FillNodes();
        }

        private void FillNodes()
        {
            ModelDS.TreeItemRow rootRow = base.data.TreeItem.Where(r => r.IsParent_IdNull()).FirstOrDefault();
            rootNode = new TreeNodeModel(rootRow);

            FillNodes(rootNode, null);                    

        }

        private void FillNodes(TreeNodeModel nodeItem, TreeNodeModel parentItem)
        {
            if(parentItem != null)
                parentItem.Nodes.Add(nodeItem);

			if (nodeItem.NodeRow.IsBranch_IdNull())
				return; //  у узла нет потомков

            foreach (ModelDS.TreeItemRow rowItem in base.data.TreeItem.Where(r => !r.IsParent_IdNull() && 
			                                                                      !r.IsParentBranch_IdNull() && 
																				  r.Parent_Id == nodeItem.NodeRow.Id && 
																				  r.ParentBranch_Id == nodeItem.NodeRow.Branch_Id ))
            {
                TreeNodeModel itemNode = new TreeNodeModel(rowItem, nodeItem);                
                FillNodes(itemNode, nodeItem);
            }
        }

		public TreeNodeModel FindNode()
		{
			return FindNode(this.rootNode);
		}

		private TreeNodeModel FindNode(TreeNodeModel node)
		{
			if (node.NodeRow.Id == this.NodeID && node.NodeRow.Branch_Id == this.BranchID)
				return node;

			foreach (TreeNodeModel n in node.Nodes)
			{
				TreeNodeModel tnm = FindNode(n);

				if (tnm != null)
					return tnm;
			}

			return null;
		}

		private void FillData()
		{
			dataSource.ConstructTree();

			foreach (ModelDS.TreeItemRow row in this.data.TreeItem.Rows)
			{
				bool isRoot = row.IsParent_IdNull();
				


				if (isRoot)
				{
					row.Visible = false;
					row.UseFilterPanel = false;
					row.UseSelectionPanel = false;
					row.Expanded = true;
					continue;
				}

				if(row.ParentBranch_Id == 1 /*ROOT*/)
				{
					
					row.Selectable = false;
					row.UseSeparator = true;
					row.UseNodesBorder = true;
					
				}
				else
				{
					row.Expandable = false;
					row.Opened = true;
					row.Visible = true;
					row.UseSelectionPanel = false;
					row.UseFilterPanel = false;
					row.Selectable = true;
				}
			}
		}

        private void FillData1()
        {
            base.data.TreeItem.Clear();

            ModelDS.TreeItemRow row = base.data.TreeItem.NewTreeItemRow();
            ModelDS.TreeItemRow parentRow = null;

            row.Id = 1;
            row.Name = "root";
			row.Visible = false;			
            parentRow = row;
            base.data.TreeItem.AddTreeItemRow(row);

			row = base.data.TreeItem.NewTreeItemRow();
			row.Id = 2;
			row.Parent_Id = parentRow.Id;
			row.Name = "Name";
			row.Selectable = true;
			row.UseSeparator = true;
			row.UseNodesBorder = true;
			base.data.TreeItem.AddTreeItemRow(row);

			row = base.data.TreeItem.NewTreeItemRow();
			row.Id = 3;
			row.Parent_Id = parentRow.Id;
			row.Name = "Producer";
			row.UseSeparator = true;
			row.UseNodesBorder = true;
			base.data.TreeItem.AddTreeItemRow(row);

			row = base.data.TreeItem.NewTreeItemRow();
			row.Id = 4;
			row.Parent_Id = parentRow.Id;
			row.Name = "Property";
			row.UseSeparator = true;
			row.UseNodesBorder = true;
			base.data.TreeItem.AddTreeItemRow(row);

			row = base.data.TreeItem.NewTreeItemRow();
			row.Id = 5;
			row.Parent_Id = parentRow.Id;
			row.Name = "Tag";
			row.UseSeparator = true;
			row.UseNodesBorder = true;
			base.data.TreeItem.AddTreeItemRow(row);

			row = base.data.TreeItem.NewTreeItemRow();
			row.Id = 6;
			row.Parent_Id = 2;
			row.Name = "Nail";
			row.Expandable = false;
			row.Opened = true;
			row.Visible = true;
			row.UseSelectionPanel = false;
			row.UseFilterPanel = false;
			row.Selectable = true;
			base.data.TreeItem.AddTreeItemRow(row);

			row = base.data.TreeItem.NewTreeItemRow();
			row.Id = 7;
			row.Parent_Id = 2;
			row.Name = "Car";
			row.Expandable = false;
			row.Opened = true;
			row.Visible = true;
			row.UseSelectionPanel = false;
			row.UseFilterPanel = false;
			row.Selectable = true;
			base.data.TreeItem.AddTreeItemRow(row);

			row = base.data.TreeItem.NewTreeItemRow();
			row.Id = 8;
			row.Parent_Id = 2;
			row.Name = "Ship";
			row.Expandable = false;
			row.Opened = true;
			row.Visible = true;
			row.UseSelectionPanel = false;
			row.UseFilterPanel = false;
			row.Selectable = true;
			base.data.TreeItem.AddTreeItemRow(row);
		}

		

        public TreeNodeModel RootNode { get { return rootNode; } }//test

		public int BranchID { get; set; }
		public int NodeID { get; set; }

    }
}
