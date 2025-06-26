using MediatR;
using Microsoft.Extensions.Logging;
using UserModuleTemplate.Application.Common.Exceptions;
using UserModuleTemplate.Application.Common.Interfaces;
using UserModuleTemplate.Domain.Users;

namespace UserModuleTemplate.Application.Users.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IEMailService _emailService;
    private readonly ILogger<ForgotPasswordCommandHandler> _logger;

    public ForgotPasswordCommandHandler(
        IUserRepository userRepository,
        IEMailService emailService,
        ILogger<ForgotPasswordCommandHandler> logger)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            _logger.LogWarning("ForgotPassword: Email adresi bulunamadı. Email: {Email}", request.Email);
            throw new NotFoundException("Bu email adresine sahip bir kullanıcı bulunamadı.");
        }

        user.PasswordResetCode = new Random().Next(100000, 999999).ToString();
        user.PasswordResetExpiresAt = DateTime.UtcNow.AddMinutes(15);

        await _userRepository.UpdateAsync(user);

        var body = $"Şifre sıfırlama kodunuz: {user.PasswordResetCode}";
        await _emailService.SendEmailAsync(user.Email, "Şifre Sıfırlama", body);

        _logger.LogInformation("ForgotPassword: Şifre sıfırlama kodu gönderildi. UserId: {UserId}, Email: {Email}", user.Id, user.Email);

        return Unit.Value;
    }
}
