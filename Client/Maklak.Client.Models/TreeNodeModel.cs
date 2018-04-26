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
        TreeNodeModel parentNode;
        List<TreeNodeModel> nodes;
		Maklak.Client.Data.DataSource dataSource;

		public TreeNodeModel()
		{
			nodeRow = null;
			parentNode = null;
			nodes = new List<TreeNodeModel>();

			this.OnModelReady += TreeNodeModel_OnModelReady;
		}

		private void TreeNodeModel_OnModelReady()
		{
			dataSource = new DataSource(this.data);

			nodeRow = dataSource.FillNode();

			FillModel();

		}

		public TreeNodeModel(ModelDS.TreeItemRow row, TreeNodeModel parentItem = null)
        {
            nodeRow = row;
            parentNode = parentItem;
            nodes = new List<TreeNodeModel>();
        }

		private void FillModel()
		{


		}

		public bool IsRoot
		{
			get { return nodeRow.IsParent_IdNull(); }
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
