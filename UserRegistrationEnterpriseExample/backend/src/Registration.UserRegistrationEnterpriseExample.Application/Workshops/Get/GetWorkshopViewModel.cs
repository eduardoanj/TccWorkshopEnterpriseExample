using System.Text.Json.Serialization;

namespace Registration.UserRegistrationEnterpriseExample.Application.Workshops.Get;

public class GetWorkshopViewModel
{
    [JsonPropertyName("totalItems")]
    public string TotalItems { get; set; }
    
    [JsonPropertyName("workshops")]
    public IList<WorkShopModel> Workshops { get; set; }
}

public class WorkShopModel
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