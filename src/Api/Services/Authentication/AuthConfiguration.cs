using Api.Services.Authentication;
using FS.Keycloak.RestApiClient.Api;
using FS.Keycloak.RestApiClient.Authentication.Client;
using FS.Keycloak.RestApiClient.Authentication.ClientFactory;
using FS.Keycloak.RestApiClient.Authentication.Flow;
using FS.Keycloak.RestApiClient.Client;

namespace Api.Services.Auyhentication;
public class AuthConfiguration(IConfiguration configuration) : IAuthConfiguration
{
    private readonly IConfiguration _configuration = configuration;

    public PasswordGrantFlow GetCredentials()
    {
        var credentials = new PasswordGrantFlow()
        {
            KeycloakUrl = _configuration.GetValue<string>("KeycloakRealm:auth-server-url")!,
            Realm = _configuration.GetValue<string>("KeycloakRealm:realm")!,
            UserName = _configuration.GetValue<string>("KeycloakRealm:UserName")!,
            Password = _configuration.GetValue<string>("KeycloakRealm:Password")!
        };
        return credentials;
    }

    public string GetRealm()
    {
        return _configuration.GetValue<string>("KeycoakClient:realm")!;
    }

    public string GetClientId()
    {
        return _configuration.GetValue<string>("KeycoakClient:resource")!;
    }

    public  AuthenticationHttpClient GetHttpClient()
    {
        return AuthenticationHttpClientFactory.Create(GetCredentials());
    }

    public  UsersApi GetUsersApi()
    {
        return ApiClientFactory.Create<UsersApi>(GetHttpClient());
    }

    public bool EmailTypeIsValid(string email)
    {
        return (
            //email.EndsWith("@contrader.it", StringComparison.OrdinalIgnoreCase) ||
            //email.EndsWith("@contrader.group", StringComparison.OrdinalIgnoreCase) ||
            //email.EndsWith("@contrader.com", StringComparison.OrdinalIgnoreCase)
            true
            );
    }
}
