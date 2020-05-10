using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Maklak.Client.Web.Services
{
	public class AppAuthenticationStateProvider : AuthenticationStateProvider
	{
		public override Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			List<string> usersList = new List<string>() { "1","2","3"};
			ClaimsIdentity identity = null;
			this.ErrorMessage = null;

			if (string.IsNullOrEmpty(UserName))
			{
				identity = new ClaimsIdentity();
			}
			else 
			{
				if (usersList.Contains(UserName))
				{
					identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, UserName) }, "App");
				}
				else 
				{
					identity = new ClaimsIdentity();
					this.ErrorMessage = "Invalid user";
				}
			
			}
			
			ClaimsPrincipal user = new ClaimsPrincipal(identity);

			Task<AuthenticationState> authTask = Task.FromResult(new AuthenticationState(user));

			base.NotifyAuthenticationStateChanged(authTask);

			return authTask;			
		}		

		public string UserName { get; set; }
		public string ErrorMessage { get; set; }
	}
}
