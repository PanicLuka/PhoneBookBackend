using PhoneBookBackend.Entities;
using PhoneBookBackend.Helpers;
using System.Threading.Tasks;

namespace PhoneBookBackend.Services
{
    public interface IUserService
    {
        Task CreateUserAsync(User user);

        Task<PagedList<User>> GetAllUsersAsync(UserParameters userParameters);

        Task<User> GetUserByIdAsync(int userId);

        Task<User> UpdateUserAsync(int userId, User user);

        Task DeleteUserAsync(int userId);

        User GetUserByEmail(string email);
    }
}
