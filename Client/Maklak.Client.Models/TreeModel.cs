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
        //TreeNodeModel rootNode;

        public TreeModel()
        {
            this.OnModelInitialized += TreeModel_OnModelInitialized;
            
        }

        private void TreeModel_OnModelInitialized()
        {
            ModelDS.TreeItemRow row = base.data.TreeItem.NewTreeItemRow();
            ModelDS.TreeItemRow parentRow = null;

            row.Id = 1;
            row.Name = "root";
            parentRow = row;
            base.data.TreeItem.AddTreeItemRow(row);

            row = base.data.TreeItem.NewTreeItemRow();
            row.Id = 2;
            row.Parent_Id = parentRow.Id;
            row.Name = "Name";
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
            base.data.TreeItem.AddTreeItemRow(row);


        }

        protected override bool IsModelInitialized()
        {
            return base.IsModelInitialized() && RootNode != null;
        }

        public TreeNodeModel RootNode { get { return this.data.TreeItem.Where(r => r.IsParent_IdNull()).Select(r => new TreeNodeModel()).FirstOrDefault(); } }

    }
}
