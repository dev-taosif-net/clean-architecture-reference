using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CleanArchitectureReference.Infrastructure.Persistence;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : IdentityDbContext<User>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("auth");
    }
}
