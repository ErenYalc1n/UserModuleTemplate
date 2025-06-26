using MediatR;
using UserModuleTemplate.Application.Users.DTOs;

namespace UserModuleTemplate.Application.Users.Commands.RefreshToken
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<LoginResultDto>;
}
