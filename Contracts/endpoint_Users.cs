namespace onekarmaapi.Contracts
{
    public interface endpoint_Users
    {
        Task<ApiResponse> AddUser(CreateUserDto User);
        Task<ApiResponse> GetUserById(string id);
        Task<ApiResponse> GetAllUsers(string qurey);
        Task<ApiResponse> UpdateUser(string id, UpdateUserDto User);
        Task<ApiResponse> DeleteUser(string id);
    }
}