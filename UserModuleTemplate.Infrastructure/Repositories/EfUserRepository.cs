using Microsoft.EntityFrameworkCore;
using UserModuleTemplate.Domain.Users;
using UserModuleTemplate.Infrastructure.Data;

namespace UserModuleTemplate.Infrastructure.Repositories
{
    public class EfUserRepository : IUserRepository
    {
        private readonly UserModuleTemplateDbContext _context;

        public EfUserRepository(UserModuleTemplateDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        public async Task<User?> GetByIdAsync(Guid id) =>
            await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }
        public async Task<bool> IsEmailConfirmedAsync(string email) =>
            await _context.Users.AnyAsync(x => x.Email == email && x.IsEmailConfirmed);

        public async Task<bool> IsReminderEmailAllowedAsync(Guid userId) =>
            await _context.Users.AnyAsync(x => x.Id == userId && x.IsKvkkAccepted);

        public async Task<User?> AuthenticateAsync(string email, string passwordHash) =>
            await _context.Users.FirstOrDefaultAsync(x =>
                x.Email == email && x.PasswordHash == passwordHash);

        public async Task<User?> GetByNicknameAsync(string nickname)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Nickname == nickname);
        }
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User?> GetByIdentifierAsync(string identifier)
        {
            return await _context.Users.FirstOrDefaultAsync(x =>
                x.Email == identifier || x.Nickname == identifier);
        }

        public async Task<User?> GetByRefreshTokenAsync(string refreshToken) =>
            await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

    }
}
