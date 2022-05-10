using UsersMVC.DTOs;

namespace UsersMVC.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsersFromApiAsync();
    }
}