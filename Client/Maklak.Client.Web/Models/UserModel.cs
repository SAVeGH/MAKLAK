using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maklak.Client.Web.Models
{
	public class UserModel
	{
		public string UserName { get; set; }

		public string UserPassword { get; set; }
		public bool IsAuthenticated { get; set; }
	}
}
