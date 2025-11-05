
using IdentityHelper.Abstraction;
using IdentityHelper.Models;
using System.Net.Http;
using System.Net.Http.Json;


namespace IdentityHelper.Service
{
    internal class UserManagement : IUserManagement
    {
        private readonly HttpClient _httpClient;

        public UserManagement(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserModel?> GetUserData(Guid id)
        {
            try
            {
                
                var response = await _httpClient.GetAsync($"api/users/{id}");

                if (!response.IsSuccessStatusCode)
                    return null;

                var user = await response.Content.ReadFromJsonAsync<UserModel>();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<List<UserModel>> GetUsersData(List<Guid> ids)
        {
            try
            {
               
                var response = await _httpClient.PostAsJsonAsync("api/users/list", ids);

                if (!response.IsSuccessStatusCode)
                    return new List<UserModel>();

                var users = await response.Content.ReadFromJsonAsync<List<UserModel>>();
                return users ?? new List<UserModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<UserModel>();
            }
        }
    }
}