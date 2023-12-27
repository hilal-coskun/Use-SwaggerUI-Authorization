using UseSwaggerAuthorization.Models;

namespace UseSwaggerAuthorization.Interfaces
{
    public interface IAuthRepository
    {
        Task AddUser(User user);

        Task<bool> IsRegisterCheck(UserDto request);

        Task<User> IsLoginCheck(UserDto request);
    }
}
