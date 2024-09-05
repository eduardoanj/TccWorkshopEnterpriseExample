using System.Text.Json.Serialization;

namespace Registration.UserRegistrationEnterpriseExample.Application.Users.GetUser;

public class GetUserViewModel
{
    [JsonPropertyName("user_type")]
    public string UserType { get; set; }
    
    [JsonPropertyName("document")]
    public string Document { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("workshops_subscribed")]
    public IList<UserWorkshopModel> WorkShopsSubscribed { get; set; } = new List<UserWorkshopModel>();
}

public class UserWorkshopModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
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
    
    [JsonPropertyName("imageCreator")]
    public string ImageCreator { get; set; }
    
    [JsonPropertyName("idCreator")]
    public string IdCreator { get; set; }
}