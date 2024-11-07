namespace UI.ViewModels
{
    public class AuthenticationResponseViewModel
    {
        public long ApiRequestId { get; set; } = default!;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

    }
}
