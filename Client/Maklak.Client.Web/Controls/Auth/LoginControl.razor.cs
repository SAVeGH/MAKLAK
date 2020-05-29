using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Maklak.Client.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;


namespace Maklak.Client.Web.Controls.Auth
{
	public partial class LoginControl
	{
		[Inject]
		AuthenticationStateProvider AuthenticationStateProvider { get; set; }

		public async Task OnLogOut()
		{
			AppAuthenticationStateProvider authStateProvider = this.AuthenticationStateProvider as AppAuthenticationStateProvider;
			authStateProvider.UserName = "";
			AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

			//await loginNotificator.UpdateLoginState(authState.User.Identity.IsAuthenticated);



			//this.AuthenticationStateProvider.NotifyAuthenticationStateChanged();

			//this.StateHasChanged(); // re-render the page
		}
	}
}
