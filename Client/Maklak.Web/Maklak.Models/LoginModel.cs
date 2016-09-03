using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Models
{
    public class LoginModel : BaseModel
    {
        public LoginModel(Guid sID)
        {
            base.Initialize(sID);
            base.Action = "Login";
        }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
