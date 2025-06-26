using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModuleTemplate.Application.Users.DTOs
{
    public class CurrentUserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

}
