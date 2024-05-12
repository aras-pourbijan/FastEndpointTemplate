using Api.Features.Users.Register.Command;
using Api.Features.Users.Register.Model;

namespace Api.Features.Users.Register.Endpoint;

public class RegisterUser : Endpoint<RegistrationModel, IResult>
{
    public override void Configure()
    {
        Post("/user/register");
        AllowAnonymous();
    }
    public override async Task<IResult> ExecuteAsync(RegistrationModel request, CancellationToken ct)
    {
        await new RegisterUserCommand
        {
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        }.ExecuteAsync(ct);

        return Results.Created();
    }
}