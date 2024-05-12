using Api.Entities.Common;

namespace Api.Entities;
public class Employee: AuditableBaseEntity<Guid>
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public bool IsEmailConfirmed { get; set; }
}
