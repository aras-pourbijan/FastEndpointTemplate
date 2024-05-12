using FS.Keycloak.RestApiClient.Api;
using FS.Keycloak.RestApiClient.Authentication.Client;
using FS.Keycloak.RestApiClient.Authentication.Flow;

namespace Api.Services.Authentication;

public interface IAuthConfiguration
{
    public PasswordGrantFlow GetCredentials();
    public string GetRealm();
    public string GetClientId();
    public AuthenticationHttpClient GetHttpClient();
    public UsersApi GetUsersApi();
    public bool EmailTypeIsValid(string email);
}
