using System.Text.Json.Serialization;

namespace Registration.UserRegistrationEnterpriseExample.Application.Users.SaveUser;

public class SaveUserDto
{
    [JsonPropertyName("userType")]
    public string UserType { get; set; }
    
    [JsonPropertyName("document")]
    public string Document { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
}