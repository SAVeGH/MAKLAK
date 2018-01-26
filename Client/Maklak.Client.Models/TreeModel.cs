using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.Client.DataSets;

namespace Maklak.Client.Models
{
    public class TreeModel : BaseModel
    {
        TreeNodeModel rootNode;

        public TreeModel()
        {
            
            this.OnModelReady += TreeModel_OnModelReady;
        }

        private void TreeModel_OnModelReady()
        {
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

            foreach (ModelDS.TreeItemRow rowItem in base.data.TreeItem.Where(r => !r.IsParent_IdNull() && r.Parent_Id == nodeItem.NodeRow.Id))
            {
                TreeNodeModel itemNode = new TreeNodeModel(rowItem, nodeItem);                
                FillNodes(itemNode, nodeItem);
            }
        }

        private void FillData()
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

    }
}
