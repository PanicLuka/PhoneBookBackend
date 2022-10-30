using PhoneBookBackend.Entities;

namespace PhoneBookBackend.Services
{
    public interface IAuthenticationService
    {
        string GenerateToken(UserLogin user);

        bool VerifiedPassword(UserLogin user);
    }
}
