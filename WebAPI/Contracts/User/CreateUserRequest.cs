using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Contracts.User
{
    public class CreateUserRequest
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Pass { get; set; } = null!;
    }
}
