using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maklak.Models.Helpers;

namespace Maklak.Models
{
    public class BaseModel
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public Guid SID { get; set; }
        
    }
}
