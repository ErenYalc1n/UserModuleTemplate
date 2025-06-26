using MediatR;

namespace UserModuleTemplate.Application.Users.Commands.ResetPassword;

public class ResetPasswordCommand : IRequest<string>
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}

