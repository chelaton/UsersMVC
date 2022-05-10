using System.Text.Json;
using UsersMVC.DTOs;

namespace UsersMVC.Services
{
    public class UserService : IUserService
    {
        public async Task<List<User>> GetUsersFromApiAsync()
        {
            var client = new HttpClient();
            var users = new List<User>();
            HttpResponseMessage response = await client.GetAsync("https://jsonplaceholder.typicode.com/users");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                users = JsonSerializer.Deserialize<List<User>>(json);
            }
            return users;
        }
    }
}
