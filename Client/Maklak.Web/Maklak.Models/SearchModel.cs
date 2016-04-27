using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Models
{
    public class SearchModel :  BaseModel
    {
        List<string> namesList;
        public string PropertyInput { get; set; }
        public SearchModel()
        {
            namesList = new List<string>();
        }

        public List<string> Names { get { return namesList; } }
    }
}
