using MediatR;

namespace UserModuleTemplate.Application.Users.Commands.Logout
{
    public class LogoutUserCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}
