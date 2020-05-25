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

		public AppAuthenticationStateProvider(grpcProxy proxy) 
		{
			serviceProxy = proxy;
		}
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
				if (serviceProxy.AuthenticateUser(UserName, UserPassword))
				{
					//string t = serviceProxy.SayHello(UserName);
					//string v = serviceProxy.SayHelloExt(UserName);

					//serviceProxy.A
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

		public string UserPassword { get; set; }
		public string ErrorMessage { get; set; }
	}
}
