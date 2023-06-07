using webapi.Models;

namespace webapi.Services.Authorization
{
    public interface IAuthService
    {
        Task<int> AddUser(Users user, string password);
        Task<string> Login(string username, string password);
        Task<bool> AlreadyExists(string username);
    }
}
