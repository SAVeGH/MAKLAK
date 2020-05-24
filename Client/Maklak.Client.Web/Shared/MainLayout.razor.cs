using Microsoft.AspNetCore.Components.Routing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Maklak.Client.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Maklak.Client.Web.Shared
{
	public partial class MainLayout //: IDisposable
	{
		//string linkText = "Login";

		//[Inject]
		//LoginNotificator loginNotificator { get; set; }

		//[CascadingParameter]
		//private Task<AuthenticationState> authenticationStateTask { get; set; }

		//protected override void OnInitialized()
		//{
		//	//base.OnInitialized();

		//	loginNotificator.LoginStateChanged += LoginNotificator_LoginStateChanged;
		//}

		//private async Task LoginNotificator_LoginStateChanged(bool isLoggedIn)
		//{

		//	//var authState = await authenticationStateTask;
		//	//var user = authState.User;

		//	await this.InvokeAsync(() =>
		//	{
		//		//linkText = isLoggedIn ? "Logout" : "Login";
		//		//this.StateHasChanged(); // re-render the page
		//	});
		//}

		//public void Dispose()
		//{
		//	loginNotificator.LoginStateChanged -= LoginNotificator_LoginStateChanged;
		//}

		//[Inject]
		//AuthenticationStateProvider AuthenticationStateProvider { get; set; }

		//public async Task OnLogOut()
		//{
		//	AppAuthenticationStateProvider authStateProvider = this.AuthenticationStateProvider as AppAuthenticationStateProvider;
		//	authStateProvider.UserName = "";
		//	AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

		//	//await loginNotificator.UpdateLoginState(authState.User.Identity.IsAuthenticated);



		//	//this.AuthenticationStateProvider.NotifyAuthenticationStateChanged();

		//	//this.StateHasChanged(); // re-render the page
		//}
	}
}
