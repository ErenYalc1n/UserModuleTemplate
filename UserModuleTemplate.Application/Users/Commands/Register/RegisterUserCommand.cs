using MediatR;
using UserModuleTemplate.Application.Users.DTOs;

namespace UserModuleTemplate.Application.Users.Commands.Register
{
    public class RegisterUserCommand : IRequest<AuthResultDto>
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Nickname { get; set; } = default!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsKvkkAccepted { get; set; }
    }

}
