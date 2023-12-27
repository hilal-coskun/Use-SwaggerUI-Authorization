using UseSwaggerAuthorization.Models;

namespace UseSwaggerAuthorization.Interfaces
{
    public interface IAuthService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

        Task<User> AddUser(UserDto request);

        string CreateToken(string userName);

        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);

        Task<string> LoginUser(UserDto request);
    }
}
