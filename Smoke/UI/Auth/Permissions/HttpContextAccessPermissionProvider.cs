using System.Security.Claims;

namespace UI.Auth.Permissions;


public partial class HttpContextAccessPermissionProvider : IAccessPermissionProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextAccessPermissionProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public LoggedInUserModel GetLoggedInUser()
    {
        try
        {
            var hasCustomerId = Guid.TryParse(GetCustomerId(), out var parsedGuid);
            var loggedInUser = new LoggedInUserModel
            {
                Id = Guid.Parse(GetUserId()),
                CustomerId = hasCustomerId ? parsedGuid : null,
                Role = GetUserRole(),
                UserName = GetUserName(),
                Claims = GetAllClaims()
            };

            return loggedInUser;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public string GetUserRole() => GetNonNullClaimFromContext(UIClaimTypes.Role);
    public string GetUserId() => GetNonNullClaimFromContext(UIClaimTypes.UserId);
    public string GetUserName() => GetClaimFromContext("username");
    public string? GetCustomerId() => GetClaimFromContext(UIClaimTypes.CustomerId);

    private string? GetClaimFromContext(string claim)
    {
        return _httpContextAccessor
            .HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == claim)?.Value;
    }

    private List<Claim> GetAllClaims()
    {
        return _httpContextAccessor.HttpContext?.User.Claims.ToList();
    }

    private string GetNonNullClaimFromContext(string claim)
    {
        return GetClaimFromContext(claim) ?? throw new ArgumentNullException($"Claim {claim} not found");
    }

    private bool UserIsRole(params string[] roleTypes)
    {
        return roleTypes.Any(x => x == GetLoggedInUser().Role);
    }

    public void EnsureUserIsRole(params string[] roleTypes)
    {
        if (!UserIsRole(roleTypes))
        {
            var message = $"User does not have a role that allows this";
            throw new UnauthorizedAccessException(message);
        }
    }
}