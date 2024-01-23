namespace WebApplication1.Contracts.User
{
    public class UserUpdateRequest
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Pass { get; set; } = null!;
    }
}
