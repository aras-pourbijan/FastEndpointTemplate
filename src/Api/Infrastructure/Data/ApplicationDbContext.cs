using Api.Entities;

namespace Api.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Employee> Employees => Set<Employee>();

    public async Task SaveChangesAsync()
    {
        await base.SaveChangesAsync();
    }
}