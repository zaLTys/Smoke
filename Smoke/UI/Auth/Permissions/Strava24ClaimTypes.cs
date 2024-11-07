namespace UI.Auth.Permissions
{

    public partial class HttpContextAccessPermissionProvider
    {
        public class UIClaimTypes
        {
            public const string UserId = "sub";
            public const string Role = "role";
            public const string CustomerId = "customer_id";
        }
    }
}