using Newtonsoft.Json;
using System;
using System.Collections.Generic;
//In .NET Core, a DTO (Data Transfer Object) is a simple class used to transfer data between different layers of an application, like from the database to the presentation layer, allowing developers to control exactly which data is sent and received by selectively exposing properties, thus promoting data decoupling and preventing over-fetching of information from the database

namespace onekarmaapi.Contracts
{
    // DTO for creating a user
    public record CreateUserDto
    {
        [JsonProperty(PropertyName = "id")] // Maps to the "id" field required by Cosmos DB
        public string? Id { get; init; } // Optional, can be generated if not provided

        [JsonProperty(PropertyName = "userId")]
        public required string UserId { get; init; }

        [JsonProperty(PropertyName = "name")]
        public required string Name { get; init; }

        [JsonProperty(PropertyName = "email")]
        public required string Email { get; init; }

        [JsonProperty(PropertyName = "apiKeys")]
        public List<ApiKeyDto>? ApiKeys { get; init; }
    }

    // DTO for updating a user
    public record UpdateUserDto
    {
        [JsonProperty(PropertyName = "name")]
        public required string Name { get; init; }

        [JsonProperty(PropertyName = "email")]
        public required string Email { get; init; }

        [JsonProperty(PropertyName = "apiKeys")]
        public List<ApiKeyDto>? ApiKeys { get; init; } // Optional during update
    }

    // DTO for API keys
    public record ApiKeyDto
    {
        [JsonProperty(PropertyName = "key")]
        public required string Key { get; init; }

        [JsonProperty(PropertyName = "createdAt")]
        public required DateTime CreatedAt { get; init; }

        [JsonProperty(PropertyName = "expiresAt")]
        public DateTime? ExpiresAt { get; init; } // Nullable
    }
}
