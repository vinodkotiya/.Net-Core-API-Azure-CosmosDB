using onekarmaapi.Contracts; // DTO namespace
using onekarmaapi.Models;    // karmaUser model namespace
using Microsoft.Azure.Cosmos;
using System.Net;

namespace onekarmaapi.Services
{
    /// <summary>
    /// Service for managing user entities in Azure Cosmos DB.
    /// </summary>
    public class UserService : endpoint_Users
    {
        private readonly Container _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="cosmosClient">The Cosmos client.</param>
        /// <param name="databaseName">The name of the database.</param>
        /// <param name="containerName">The name of the container.</param>
        public UserService(CosmosClient cosmosClient, string databaseName, string containerName)
        {
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        /// <summary>
        /// Adds a new user asynchronously.
        /// </summary>
        /// <param name="user">The user to add.</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the result of the operation.</returns>
        public async Task<ApiResponse> AddUser(CreateUserDto user)
{
    var userEntity = new karmaUser
    {
        Id = string.IsNullOrWhiteSpace(user.Id) ? Guid.NewGuid().ToString() : user.Id,
        userId = user.UserId,
        Name = user.Name,
        Email = user.Email,
        ApiKeys = user.ApiKeys?.ConvertAll(apiKey => new ApiKey
        {
            Key = apiKey.Key,
            CreatedAt = apiKey.CreatedAt,
            ExpiresAt = apiKey.ExpiresAt
        })
    };

    // Log the user entity for debugging
    //Console.WriteLine($"Creating user: {JsonConvert.SerializeObject(userEntity)}");

    try
    {
        // Ensure the partition key matches the container's configuration
        var response = await _container.CreateItemAsync(userEntity, new PartitionKey(userEntity.userId));

        return new ApiResponse
        {
            IsSuccess = response.StatusCode == HttpStatusCode.Created,
            Message = response.StatusCode.ToString(),
            Result = userEntity
        };
    }
    catch (CosmosException ex)
    {
        return new ApiResponse
        {
            IsSuccess = false,
            Message = ex.Message,
            Result = null
        };
    }
}



        /// <summary>
        /// Gets all users asynchronously.
        /// </summary>
        /// <param name="query">The query to execute.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the list of users.</returns>
        public async Task<ApiResponse> GetAllUsers(string query)
        {
            var queryIterator = _container.GetItemQueryIterator<karmaUser>(new QueryDefinition(query));
            var users = new List<karmaUser>();
            while (queryIterator.HasMoreResults)
            {
                var response = await queryIterator.ReadNextAsync();
                users.AddRange(response.Resource);
            }
            return new ApiResponse
            {
                IsSuccess = true,
                Message = "Users fetched successfully",
                Result = users
            };
        }

        /// <summary>
        /// Gets a user by ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>An <see cref="ApiResponse"/> containing the user.</returns>
        public async Task<ApiResponse> GetUserById(string id)
        {
            try
            {
                var response = await _container.ReadItemAsync<karmaUser>(id, new PartitionKey(id));
                return new ApiResponse
                {
                    IsSuccess = true,
                    Message = "karmaUser fetched successfully",
                    Result = response.Resource
                };
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }

        /// <summary>
        /// Updates a user asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="user">The updated user data.</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the result of the operation.</returns>
        public async Task<ApiResponse> UpdateUser(string id, UpdateUserDto user)
        {
            try
            {
                var patchOperations = new List<PatchOperation>();

                if (!string.IsNullOrWhiteSpace(user.Name))
                {
                    patchOperations.Add(PatchOperation.Replace("/Name", user.Name.Trim()));
                }

                if (!string.IsNullOrWhiteSpace(user.Email))
                {
                    patchOperations.Add(PatchOperation.Replace("/Email", user.Email.Trim()));
                }

                if (user.ApiKeys != null && user.ApiKeys.Any())
                {
                    patchOperations.Add(PatchOperation.Replace("/ApiKeys", user.ApiKeys));
                }

                var response = await _container.PatchItemAsync<karmaUser>(id, new PartitionKey(id), patchOperations);

                return new ApiResponse
                {
                    IsSuccess = response.Equals(HttpStatusCode.NoContent),
                    Message = response.StatusCode.ToString(),
                    Result = response.Resource
                };
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }

        /// <summary>
        /// Deletes a user asynchronously.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>An <see cref="ApiResponse"/> indicating the result of the operation.</returns>
        public async Task<ApiResponse> DeleteUser(string id)
        {
            try
            {
                var response = await _container.DeleteItemAsync<karmaUser>(id, new PartitionKey(id));
                return new ApiResponse
                {
                    IsSuccess = response.Equals(HttpStatusCode.NoContent),
                    Message = response.StatusCode.ToString(),
                    Result = null
                };
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = null
                };
            }
        }
    }
}
