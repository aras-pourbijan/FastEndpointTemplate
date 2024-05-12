namespace Api.Features.Users.Register.Model;

public class RegistrationModel
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

public class RegistrationModelValidator : Validator<RegistrationModel>
{
    public RegistrationModelValidator()
    {
        RuleFor(x => x).NotEmpty().WithMessage("Richiesta non valida");
        RuleFor(x => x.FirstName).NotEmpty().Length(2, 25).WithMessage("Il nome deve essere tra 2 e 25 caratteti");
        RuleFor(x => x.LastName).NotEmpty().Length(4, 25).WithMessage("Il cognome deve essere tra 2 e 25 caratteti");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email non e valido.");
    }
}
