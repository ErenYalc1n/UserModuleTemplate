using MediatR;
using Microsoft.Extensions.Logging;
using UserModuleTemplate.Application.Common.Exceptions;
using UserModuleTemplate.Application.Common.Interfaces;
using UserModuleTemplate.Domain.Users;

namespace UserModuleTemplate.Application.Users.Commands.Logout;

public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<LogoutUserCommandHandler> _logger;

    public LogoutUserCommandHandler(
        IUserRepository userRepository,
        ILogger<LogoutUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null)
        {
            _logger.LogWarning("Logout failed: kullanıcı bulunamadı. UserId: {UserId}", request.UserId);
            throw new UnauthorizedAppException("Kullanıcı bulunamadı.");
        }

        user.RefreshToken = null;
        user.RefreshTokenExpiresAt = null;

        await _userRepository.UpdateAsync(user);

        _logger.LogInformation("Logout success: Kullanıcı çıkış yaptı. UserId: {UserId}", user.Id);

        return Unit.Value;
    }
}
