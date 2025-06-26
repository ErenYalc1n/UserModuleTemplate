using MediatR;
using Microsoft.Extensions.Logging;
using UserModuleTemplate.Application.Common.Exceptions;
using UserModuleTemplate.Application.Common.Interfaces;
using UserModuleTemplate.Domain.Users;

namespace UserModuleTemplate.Application.Users.Commands.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<DeleteUserCommandHandler> _logger;

    public DeleteUserCommandHandler(
        IUserRepository userRepository,
        ICurrentUserService currentUserService,
        ILogger<DeleteUserCommandHandler> logger)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        _logger.LogInformation("DeleteUser: Kullanıcı silme işlemi başlatıldı. UserId: {UserId}", userId);

        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            _logger.LogWarning("DeleteUser: Kullanıcı bulunamadı. UserId: {UserId}", userId);
            throw new NotFoundException("Kullanıcı bulunamadı.");
        }

        await _userRepository.DeleteAsync(user);

        _logger.LogInformation("DeleteUser: Kullanıcı başarıyla silindi. UserId: {UserId}", userId);

        return Unit.Value;
    }
}
