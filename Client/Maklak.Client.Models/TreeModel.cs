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
			row.ShowHeader = false;
			row.UseFilter = false;
            parentRow = row;
            base.data.TreeItem.AddTreeItemRow(row);

			row = base.data.TreeItem.NewTreeItemRow();
			row.Id = 2;
			row.Parent_Id = parentRow.Id;
			row.Name = "Name";
			row.Action = "ProductEditSection";
			row.Controller = "Search";
			base.data.TreeItem.AddTreeItemRow(row);

			row = base.data.TreeItem.NewTreeItemRow();
			row.Id = 3;
			row.Parent_Id = parentRow.Id;
			row.Name = "Property";
			base.data.TreeItem.AddTreeItemRow(row);

			row = base.data.TreeItem.NewTreeItemRow();
			row.Id = 4;
			row.Parent_Id = parentRow.Id;
			row.Name = "Tag";
			row.Action = "TagsSelectSection";
			row.Controller = "Search";
			base.data.TreeItem.AddTreeItemRow(row);
		}

       

        public TreeNodeModel RootNode { get { return rootNode; } }//test

    }
}
