using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maklak.Client.Web.Services
{
	public class LoginNotificator
	{
		public event Func<bool, Task> LoginStateChanged;

        public async Task UpdateLoginState(bool isLogggedIn)
        {
            if (LoginStateChanged != null)
            {
                await LoginStateChanged.Invoke(isLogggedIn);
            }
        }
    }
}
