using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Maklak.Client.Data;
using Maklak.Client.DataSets;

namespace Maklak.Client.Models
{
    public class TreeNodeModel : BaseModel
    {
        ModelDS.TreeItemRow nodeRow;
        TreeNodeModel parentNode;
        List<TreeNodeModel> nodes;
		//Maklak.Client.Data.DataSource dataSource;

		public TreeNodeModel()
		{
			nodeRow = null;
			parentNode = null;
			nodes = new List<TreeNodeModel>();

			//this.OnModelReady += TreeNodeModel_OnModelReady;
		}

		//private void TreeNodeModel_OnModelReady()
		//{
		//	//dataSource = new DataSource(this.data);

		//	//nodeRow = dataSource.FillNode();

		//	//FillModel();

		//}

		public override void ModelBound(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			dataSource = new DataSource(this.data);

			nodeRow = dataSource.FillNode(this.BranchID,this.NodeID);

			FillModel();
		}

		//public override void InitializeData(ControllerContext controllerContext, ModelBindingContext bindingContext)
		//{
		//	//dataSource = new DataSource(this.data);

		//	//nodeRow = dataSource.FillNode();

		//	//FillModel();
		//}

		public TreeNodeModel(ModelDS.TreeItemRow row, TreeNodeModel parentItem = null)
        {
            nodeRow = row;
            parentNode = parentItem;
            nodes = new List<TreeNodeModel>();
        }

		private void FillModel()
		{
			

			FillNodes(this, null);

		}

		private void FillNodes(TreeNodeModel nodeItem, TreeNodeModel parentItem)
		{
			if (parentItem != null)
				parentItem.Nodes.Add(nodeItem);

			if (nodeItem.NodeRow.IsBranch_IdNull())
				return; //  у узла нет потомков

			foreach (ModelDS.TreeItemRow rowItem in base.data.TreeItem.Where(r => !r.IsParent_IdNull() &&
																				  !r.IsParentBranch_IdNull() &&
																				  r.Parent_Id == nodeItem.NodeRow.Id &&
																				  r.ParentBranch_Id == nodeItem.NodeRow.Branch_Id))
			{
				TreeNodeModel itemNode = new TreeNodeModel(rowItem, nodeItem);
				FillNodes(itemNode, nodeItem);
			}
		}

		public bool IsRoot
		{
			get { return nodeRow == null ? false : nodeRow.IsParent_IdNull(); }
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

		public int BranchID { get; set; }
		public int NodeID { get; set; }

		public bool UseCustomPanel
		{
			get { return this.NodeRow == null ? false : (this.NodeRow.UseFilterPanel || this.NodeRow.UseSelectionPanel); }
		}

	}
}
