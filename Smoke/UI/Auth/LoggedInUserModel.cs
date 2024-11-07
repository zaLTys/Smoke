using System.Security.Claims;

namespace UI.Auth
{
    public class LoggedInUserModel
    {
        public Guid? Id { get; set; }
        public Guid? CustomerId { get; set; }
        public string Role { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public List<Claim> Claims { get; set; } = default!;
    }
}