using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Client.Models
{
    public class TreeModel : BaseModel
    {
        TreeNodeModel rootNode;

        public TreeNodeModel RootNode { get { return rootNode; } }

    }
}
