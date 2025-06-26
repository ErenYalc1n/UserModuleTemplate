using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserModuleTemplate.Application.Common.Interfaces;
using UserModuleTemplate.Application.Mail;
using UserModuleTemplate.Application.Users.Commands.ChangePassword;
using UserModuleTemplate.Application.Users.Commands.Delete;
using UserModuleTemplate.Application.Users.Commands.Login;
using UserModuleTemplate.Application.Users.Commands.Logout;
using UserModuleTemplate.Application.Users.Commands.RefreshToken;
using UserModuleTemplate.Application.Users.Commands.Register;
using UserModuleTemplate.Application.Users.Commands.ResetPassword;
using UserModuleTemplate.Application.Users.Commands.Update;
using UserModuleTemplate.Application.Users.Queries.GetCurrentUser;
using UserModuleTemplate.Infrastructure.Mail;

namespace UserModuleTemplate.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "UnverifiedPlayer,Player")]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // veya ClaimTypes.Name
            return Ok($"Selam kullanıcı: {userId}");
        }

        [Authorize(Roles = "UnverifiedPlayer,Player")]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "UnverifiedPlayer,Player")]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
                return Unauthorized();

            await _mediator.Send(new LogoutUserCommand { UserId = Guid.Parse(userId) });

            return Ok("Çıkış başarılı.");
        }
        [Authorize(Roles = "UnverifiedPlayer,Player")]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete()
        {
            await _mediator.Send(new DeleteUserCommand());
            return Ok("Hesap başarıyla silindi.");
        }

        [Authorize(Roles = "Player")]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("Profil güncellendi.");
        }

        [Authorize]
        [HttpGet("get-profile")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var result = await _mediator.Send(new GetCurrentUserQuery());
            return Ok(result);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
