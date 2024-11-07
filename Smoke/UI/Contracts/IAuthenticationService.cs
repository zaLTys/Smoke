namespace UI.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateDummy(string code);
        Task Logout();
    }
}
