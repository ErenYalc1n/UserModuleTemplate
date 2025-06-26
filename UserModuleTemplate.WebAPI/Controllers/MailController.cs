using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserModuleTemplate.Application.Common.Interfaces;
using UserModuleTemplate.Application.Mail;
using UserModuleTemplate.Application.Users.Commands.ForgotPassword;
using UserModuleTemplate.Application.Users.Commands.VerifyEmail;


namespace UserModuleTemplate.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MailController : ControllerBase
{
    private readonly IEMailService _emailService;  
    private readonly IMediator _mediator;

    public MailController(IEMailService emailService, IMediator mediator)
    {
        _emailService = emailService;
        _mediator = mediator;
    }


    [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
    {
        await _emailService.SendEmailAsync(request.To, request.Subject, request.Body);
        return Ok("Mail gönderildi.");
    }

    [Authorize(Roles = "UnverifiedPlayer")]
    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result); 
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
    {
        await _mediator.Send(command);
        return Ok("Şifre sıfırlama maili gönderildi.");
    }

}
