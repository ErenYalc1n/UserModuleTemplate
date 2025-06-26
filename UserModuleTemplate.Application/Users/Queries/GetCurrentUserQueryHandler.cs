using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using UserModuleTemplate.Application.Common.Exceptions;
using UserModuleTemplate.Application.Common.Interfaces;
using UserModuleTemplate.Application.Users.DTOs;
using UserModuleTemplate.Domain.Users;

namespace UserModuleTemplate.Application.Users.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, CurrentUserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<GetCurrentUserQueryHandler> _logger;

    public GetCurrentUserQueryHandler(
        IUserRepository userRepository,
        IHttpContextAccessor httpContextAccessor,
        ILogger<GetCurrentUserQueryHandler> logger)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    public async Task<CurrentUserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userIdStr = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdStr, out var userId))
        {
            _logger.LogWarning("GetCurrentUser: JWT içinden userId alınamadı.");
            throw new UnauthorizedAppException("Kullanıcı oturum bilgisi geçersiz.");
        }

        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null)
        {
            _logger.LogWarning("GetCurrentUser: Kullanıcı bulunamadı. UserId: {UserId}", userId);
            throw new NotFoundException("Kullanıcı bulunamadı.");
        }

        return new CurrentUserDto
        {
            Id = user.Id,
            Email = user.Email,
            Nickname = user.Nickname,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role.ToString()
        };
    }
}
