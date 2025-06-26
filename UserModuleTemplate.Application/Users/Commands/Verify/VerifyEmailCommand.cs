using MediatR;
using UserModuleTemplate.Application.Users.DTOs;

namespace UserModuleTemplate.Application.Users.Commands.VerifyEmail;

public class VerifyEmailCommand : IRequest<AuthResultDto>
{
    public string Code { get; set; } = string.Empty;
}
