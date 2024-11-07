namespace UI.Auth.Permissions
{
    public interface IAccessPermissionProvider
    {
        LoggedInUserModel GetLoggedInUser();
        string? GetCustomerId();
        void EnsureUserIsRole(params string[] roleTypes);
    }
}