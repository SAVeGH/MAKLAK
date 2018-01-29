using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Maklak.Client.Data;
using Maklak.Client.DataSets;

namespace Maklak.Client.Models
{
    public class TreeNodeModel 
    {
        ModelDS.TreeItemRow nodeRow;
        TreeNodeModel parentNode;
        List<TreeNodeModel> nodes;

        public TreeNodeModel(ModelDS.TreeItemRow row, TreeNodeModel parentItem = null)
        {
            nodeRow = row;
            parentNode = parentItem;
            nodes = new List<TreeNodeModel>();
        }

		public bool IsVisibleRoot(ModelDS.TreeItemRow row)
		{
			if (row.IsParent_IdNull())
				return false;

			return row.Parent_Id == 6;
		}


		public ModelDS.TreeItemRow NodeRow
        {
            get { return nodeRow; }
        }
        

        public TreeNodeModel ParentNode { get { return parentNode; } }        

        public List<TreeNodeModel> Nodes
        {
            get
            {
                return nodes;
            }
        }
    }
}
