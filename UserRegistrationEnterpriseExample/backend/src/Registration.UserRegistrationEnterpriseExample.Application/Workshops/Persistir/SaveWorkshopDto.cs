using System.Text.Json.Serialization;

namespace Registration.UserRegistrationEnterpriseExample.Application.Workshops.Persistir;

public class SaveWorkshopDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("date")]
    public string Date { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; }
    
    [JsonPropertyName("idCreator")]
    public string IdCreator { get; set; }
}