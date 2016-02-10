using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maklak.Models
{
    public class LoginModel : BaseModel
    {
        public LoginModel()
        {
            base.Action = "Login";
        }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
