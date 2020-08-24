using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using Maklak.Client.Service;

namespace Maklak.Client.Web.Services
{
	public class AppAuthenticationStateProvider : AuthenticationStateProvider
	{		
		grpcProxy serviceProxy;

		public AppAuthenticationStateProvider(grpcProxy srvProxy /*from DI container*/) 
		{
			serviceProxy = srvProxy;
		}
		public override Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			ClaimsPrincipal user = null;
			this.ErrorMessage = null;

			if (!this.IsRegister)
				user = AuthenticateUser();
			else
				user = RegisterUser();

			Task<AuthenticationState> authTask = Task.FromResult(new AuthenticationState(user));

			base.NotifyAuthenticationStateChanged(authTask);

			return authTask;			
		}

		private ClaimsPrincipal RegisterUser()
		{
			ClaimsIdentity identity = null;
			this.ErrorMessage = string.Empty;

			if (string.IsNullOrEmpty(UserName))
			{
				identity = new ClaimsIdentity();
			}
			else
			{
				if (serviceProxy.RegisterUser(UserName, UserPassword))
				{
					identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, UserName) }, "App");
				}
				else
				{
					identity = new ClaimsIdentity();
					this.ErrorMessage = "User exists";
				}

			}

			ClaimsPrincipal user = new ClaimsPrincipal(identity);

			return user;
		}

		private ClaimsPrincipal AuthenticateUser() 
		{
			ClaimsIdentity identity = null;
			this.ErrorMessage = string.Empty;

			if (string.IsNullOrEmpty(UserName))
			{
				identity = new ClaimsIdentity();
			}
			else
			{
				if (serviceProxy.AuthenticateUser(UserName, UserPassword))
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

			return user;
		}

		public string UserName { get; set; }
		public bool IsRegister { get; set; }

		public string UserPassword { get; set; }
		public string ErrorMessage { get; set; }
	}
}
