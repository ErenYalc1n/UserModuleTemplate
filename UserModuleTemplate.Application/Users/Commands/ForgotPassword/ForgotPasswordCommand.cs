using MediatR;

namespace UserModuleTemplate.Application.Users.Commands.ForgotPassword;

public class ForgotPasswordCommand : IRequest
{
    public string Email { get; set; } = string.Empty;
}
