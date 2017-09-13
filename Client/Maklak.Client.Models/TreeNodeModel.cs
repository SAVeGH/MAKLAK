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
        TreeNodeModel parent;
        //List<TreeItemModel> children;

        //public TreeItemModel()
        //{
        //    children = new List<TreeItemModel>();
        //}        

        public int NodeId { get; set; }

        public TreeNodeModel ParentNode { get; set; }

        public string Name {
            get { return data.TreeItem.Where(r => r.Id == NodeId).Select(r => r.Name).FirstOrDefault(); }
        }

        public List<TreeNodeModel> Nodes
        {
            get
            {
                return data.TreeItem.Where(r => !r.IsParent_IdNull() && r.Parent_Id == this.NodeId).Select(r=> new TreeNodeModel() { ParentNode = this,NodeId = r.Id}).ToList();
            }
        }
    }
}
