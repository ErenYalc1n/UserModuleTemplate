using MediatR;
using UserModuleTemplate.Application.Users.DTOs;

namespace UserModuleTemplate.Application.Users.Queries.GetCurrentUser;

public class GetCurrentUserQuery : IRequest<CurrentUserDto>
{
}
