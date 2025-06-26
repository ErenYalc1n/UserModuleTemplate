using MediatR;
using UserModuleTemplate.Application.Users.DTOs;

namespace UserModuleTemplate.Application.Users.Commands.Login
{
    public class LoginUserCommand : IRequest<LoginResultDto>
    {
        public string Identifier { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
