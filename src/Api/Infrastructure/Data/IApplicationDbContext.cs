using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Data;

public interface IApplicationDbContext
{
    public DbSet<Employee> Employees { get; }
    public Task SaveChangesAsync();
}
