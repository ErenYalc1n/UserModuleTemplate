using UserModuleTemplate.Application.Users.DTOs;
using UserModuleTemplate.Domain.Users;

namespace UserModuleTemplate.Application.Common.Interfaces
{
    public interface ITokenService
    {      
        TokenResultDto CreateToken(User user);
    }

}
