using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.Client.Data;
using Maklak.Client.DataSets;

namespace Maklak.Client.Models
{
    public class TreeNodeModel : BaseModel
    {
        ModelDS.TreeItemRow nodeRow;

        public TreeNodeModel()
        {
            ModelDS.TreeItemRow rootRow = this.data.TreeItem.Where(r => r.IsParent_IdNull()).FirstOrDefault();

            if (rootRow == null)
            { 
                rootRow = this.data.TreeItem.NewTreeItemRow();
                this.data.TreeItem.AddTreeItemRow(rootRow);
            }


            nodeRow = rootRow;
            rootRow.Id = 1;
        }

        public TreeNodeModel(int nodeId,TreeNodeModel parentNode)
        {
            ModelDS.TreeItemRow row = this.data.TreeItem.Where(r => !r.IsParent_IdNull() && r.Id == nodeId).FirstOrDefault();

            if (row == null)
            {
                row = this.data.TreeItem.NewTreeItemRow();
                this.data.TreeItem.AddTreeItemRow(row);
            }

            nodeRow = row;
            row.Id = nodeId;
            row.Parent_Id = parentNode.NodeId;

        }


        public int NodeId { get { return nodeRow.Id; } }

        public TreeNodeModel ParentNode { get; set; }

        public string Name {
            get { return data.TreeItem.Where(r => r.Id == NodeId).Select(r => r.Name).FirstOrDefault(); }
        }

        public List<TreeNodeModel> Nodes
        {
            get
            {
                return data.TreeItem.Where(r => !r.IsParent_IdNull() && r.Parent_Id == this.NodeId).Select(r=> new TreeNodeModel(r.Id,this)).ToList();
            }
        }
    }
}
