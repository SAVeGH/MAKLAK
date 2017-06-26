using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Client.Models
{
    public class SearchModel :  BaseModel
    {
        List<string> namesList;
        public string PropertyInput { get; set; }
        public SearchModel() 
        {
            //base.Initialize(sID);
            namesList = new List<string>();
        }

        public List<string> Names { get { return namesList; } }
    }
}
