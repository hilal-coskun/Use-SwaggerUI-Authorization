using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UseSwaggerAuthorization.Context;
using UseSwaggerAuthorization.Interfaces;
using UseSwaggerAuthorization.Models;

namespace UseSwaggerAuthorization.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration configuration;


        public AuthRepository(IConfiguration configuration, UseSwaggerAuthorizationDbContext context)
        {
            this.configuration = configuration;
        }

        public async Task AddUser(User user)
        {
            using(var context = new UseSwaggerAuthorizationDbContext())
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task<User> IsLoginCheck(UserDto request)
        {
            User user;

            using (var context = new UseSwaggerAuthorizationDbContext())
            {
                user = await context.Users
                    .FirstOrDefaultAsync(u => u.UserName == request.UserName);
            }

            return user;
        }

        public async Task<bool> IsRegisterCheck(UserDto request)
        {
            bool userCheck;

            using (var context = new UseSwaggerAuthorizationDbContext())
            {
                userCheck = await context.Users.AnyAsync(u => u.UserName == request.UserName);
            }

            return userCheck;
        }
    }
}
