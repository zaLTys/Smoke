using AutoMapper;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using UI.Auth;
using UI.Contracts;
using UI.Services.Base;

namespace UI.Services
{
    public class AuthenticationService : BaseDataService, IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IClient client, ILocalStorageService localStorage, IMapper mapper, AuthenticationStateProvider authenticationStateProvider)
            : base(client, localStorage)
        {
            _mapper = mapper;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateForApi(long athleteId, string password)
        {
            return true;

            //try
            //{
            //    AuthenticationRequest authenticationRequest = new AuthenticationRequest() { AthleteId = athleteId, Password = password };
            //    var authenticationResponse = await _client.GetAuthUrlAsync();

            //    //TODO:AddToken
            //    if (authenticationResponse.AuthenticationUrl != string.Empty)
            //    {
            //        await _localStorage.SetItemAsync("token", authenticationResponse);
            //        ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetUserAuthenticated(12345);
            //        _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authenticationResponse.AuthenticationUrl);

            //        return true;
            //    }
            //    return false;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("token");
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetUserLoggedOut();
            _client.HttpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<string> GetAuthenticationUrl()
        {
            var response = await _client.GetAuthUrlAsync();
            return response.AuthenticationUrl;
        }

        public async Task<bool> AuthenticateViaStrava(string code)
        {
            try
            {
                var authenticationResponse = await _client.AuthenticateAsync(code);
                if (authenticationResponse.AuthenticationResponse.Token != null
                    && authenticationResponse.AuthenticationResponse.AthleteId != null)
                {
                    await _localStorage.SetItemAsync("token", authenticationResponse.AuthenticationResponse.Token);
                    await _localStorage.SetItemAsync("athleteId", authenticationResponse.AuthenticationResponse.AthleteId);
                    ((CustomAuthenticationStateProvider)_authenticationStateProvider).SetUserAuthenticated(authenticationResponse.AuthenticationResponse.AthleteId);
                    _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authenticationResponse.AuthenticationResponse.Token);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                //Todo:log
                return false;
            }


        }
    }
}
