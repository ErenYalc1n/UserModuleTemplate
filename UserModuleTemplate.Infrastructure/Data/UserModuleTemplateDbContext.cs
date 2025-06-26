using Microsoft.EntityFrameworkCore;
using UserModuleTemplate.Domain.Users;

namespace UserModuleTemplate.Infrastructure.Data
{
    public class UserModuleTemplateDbContext : DbContext
    {
        public UserModuleTemplateDbContext(DbContextOptions<UserModuleTemplateDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserModuleTemplateDbContext).Assembly);
           
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.PasswordHash)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.Nickname)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                      .HasMaxLength(50);

                entity.Property(e => e.LastName)
                      .HasMaxLength(50);

                entity.Property(e => e.IsEmailConfirmed)
                      .IsRequired();

                entity.Property(e => e.IsKvkkAccepted)
                      .IsRequired();

                entity.Property(e => e.Role)
                      .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }


    }

}
