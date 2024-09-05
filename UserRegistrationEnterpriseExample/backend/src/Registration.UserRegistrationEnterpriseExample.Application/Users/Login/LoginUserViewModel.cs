using System.Text.Json.Serialization;

namespace Registration.UserRegistrationEnterpriseExample.Application.Users.Login;

public class LoginUserViewModel
{
    [JsonPropertyName("visualizar")]
    public bool Visualizar { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("id")]
    public string Id { get; set; }
}