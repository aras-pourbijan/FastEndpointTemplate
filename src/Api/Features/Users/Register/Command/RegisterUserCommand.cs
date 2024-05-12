using Api.Commons.Models;
using Api.Entities;
using Api.Infrastructure.Data;
using Api.Services.Authentication;
using Api.Services.Mail;
using FS.Keycloak.RestApiClient.Model;

namespace Api.Features.Users.Register.Command;

public class RegisterUserCommand : ICommand
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

public class RegisterUserCommandHandler(IApplicationDbContext _dbContext, IMailService _mailService, IAuthConfiguration _authConfiguration) : ICommandHandler<RegisterUserCommand>
{
    public async Task ExecuteAsync(RegisterUserCommand command, CancellationToken ct)
    {
        using var usersApi = _authConfiguration.GetUsersApi();

        if (!_authConfiguration.EmailTypeIsValid(command.Email))
            throw new AccessViolationException("Email non e valido");

        // insert user in keycloak
        try
        {
            await usersApi.PostUsersAsync(_authConfiguration.GetRealm(), new UserRepresentation
            {
                Username = command.UserName,
                Email = command.Email,
                FirstName = command.FirstName,
                LastName = command.LastName,
            }, ct);

        }
        catch (Exception ex)
        {
            throw new BadHttpRequestException("problemi con keycloak, " + ex.Message);
        }

        // insert user in our Db

        var users = await usersApi.GetUsersAsync(_authConfiguration.GetRealm(), true, command.Email, cancellationToken: ct);

        var user = users.FirstOrDefault() ?? throw new ArgumentNullException("userId", "Non esiste questo user nel keycloak!");

        _dbContext.Employees.Add(new Employee
        {
            Id = Guid.Parse(user.Id),
            Email = command.Email.Trim(),
            UserName = command.UserName.Trim(),
            FirstName = user.FirstName.Trim(),
            LastName = user.LastName.Trim(),
            CreatedBy = $"{user.FirstName} {user.LastName}"
        });
        await _dbContext.SaveChangesAsync();

        // sending confirmation email

        string massageBody = _mailService.CustomizeEmail(new EmailTemplate
        {
            TemplateName = "email_registration_template.html",
            BaseLinkUrl = Utility.EmailConfirmationLink,
            Id = user.Id
        });

        _mailService.SendEmail(command.Email, "Attiva il tuo account di skill-mapper", massageBody);
    }
}

