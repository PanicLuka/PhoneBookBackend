using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PhoneBookBackend.Entities;
using PhoneBookBackend.Helpers;
using PhoneBookBackend.Validators;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using BC = BCrypt.Net.BCrypt;


namespace PhoneBookBackend.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly UserValidator _validator;

        public UserService(DataContext context, UserValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task CreateUserAsync(User user)
        {
            _validator.ValidateAndThrow(user);

            user.Password = BC.HashPassword(user.Password);

            await _context.AddAsync(user);

            await SaveChangesAsync();

        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await GetUserByIdHelperAsync(userId);

            if(user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(user);
            await SaveChangesAsync();
        }

        public async Task<PagedList<User>> GetAllUsersAsync(UserParameters userParameters)
        {
            var users = await _context.Users.ToListAsync();
            if(users == null || users.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            IQueryable<User> queryable = users.AsQueryable();

            return PagedList<User>.ToPagedList(queryable, userParameters.PageNumber, userParameters.PageSize);
        }

        

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.UserId == userId);
            if(user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);


            }

            return user;
        }

        public async Task<User> UpdateUserAsync(int userId, User user)
        {
            var oldUser = await GetUserByIdHelperAsync(userId);
            if (oldUser == null)
            {
                await CreateUserAsync(user);
                return user;
            }
            else
            {
                
                oldUser.FirstName = user.FirstName;
                oldUser.LastName = user.LastName;
                oldUser.Username = user.Username;
                oldUser.Email = user.Email;
                oldUser.Password = user.Password;



                await SaveChangesAsync();

                if (oldUser == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                return oldUser;
                
            }
        }

        public User GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(e => e.Email == email);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            

            return user;
        }

        private async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<User> GetUserByIdHelperAsync(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(e => e.UserId == userId);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return user;
        }
    }
}
