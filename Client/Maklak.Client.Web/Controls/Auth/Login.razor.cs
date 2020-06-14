using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;
using Maklak.Client.Web.Models;

using System.Text;
using System.Text.Json;
using Maklak.Client.Web.Services;

using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;


namespace Maklak.Client.Web.Controls.Auth
{
    public partial class Login
    {
        [Inject]
        LoginNotificator loginNotificator { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string UserLogin { get; set; }

        [Parameter]
        public string UserPassword { get; set; }

        string errorMessage;

        public async Task OnLogin()
        {
            AppAuthenticationStateProvider authStateProvider = this.AuthenticationStateProvider as AppAuthenticationStateProvider;
            authStateProvider.UserName = UserLogin;
            authStateProvider.UserPassword = UserPassword;
            authStateProvider.IsRegister = false;

            AuthenticationState authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            if (!authState.User.Identity.IsAuthenticated)
            {
                errorMessage = authStateProvider.ErrorMessage;
            }
            else
            {
                errorMessage = null;

                NavigationManager.NavigateTo("search");
            }

            //await loginNotificator.UpdateLoginState(authState.User.Identity.IsAuthenticated);



            //this.AuthenticationStateProvider.NotifyAuthenticationStateChanged();

            //this.StateHasChanged(); // re-render the page
        }
    }
}
