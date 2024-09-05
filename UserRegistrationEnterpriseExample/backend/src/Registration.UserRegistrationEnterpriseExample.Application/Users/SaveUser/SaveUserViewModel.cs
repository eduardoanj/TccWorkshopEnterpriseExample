using System.Text.Json.Serialization;

namespace Registration.UserRegistrationEnterpriseExample.Application.Users.SaveUser;

public class SaveUserViewModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("usuarioJaCadastrado")]
    public bool UsuarioJaCadastrado { get; set; }
}