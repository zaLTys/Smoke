namespace UI.Contracts
{
    public interface IAuthenticationService
    {
        Task<string> GetAuthenticationUrl();
        Task<bool> AuthenticateViaStrava(string code);
        Task Logout();
    }
}
