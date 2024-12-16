using System;
using System.Collections.Generic;
using Newtonsoft.Json;
namespace onekarmaapi.Models;
public class karmaUser
{
    [JsonProperty(PropertyName = "id")] // Maps to the required "id" field in Cosmos DB
    public string Id { get; set; }

    [JsonProperty(PropertyName = "userId")]
    public string userId { get; set; } // Partition Key

    [JsonProperty(PropertyName = "Name")]
    public string Name { get; set; }

    [JsonProperty(PropertyName = "Email")]
    public string Email { get; set; }

    [JsonProperty(PropertyName = "ApiKeys")]
    public List<ApiKey> ApiKeys { get; set; }
}


public class ApiKey
{
    public string Key { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
}
