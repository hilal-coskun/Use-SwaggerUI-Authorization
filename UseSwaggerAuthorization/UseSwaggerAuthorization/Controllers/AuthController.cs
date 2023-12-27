using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UseSwaggerAuthorization.Interfaces;
using UseSwaggerAuthorization.Models;
using UseSwaggerAuthorization.Services;

namespace UseSwaggerAuthorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IAuthService _authServices;
        private readonly IAuthRepository _authRepository;

        public AuthController(IConfiguration configuration, IAuthService authServices, IAuthRepository authRepository)
        {
            this.configuration = configuration;
            _authServices = authServices;
            _authRepository = authRepository;
        }

        public static User user = new User();

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var userCheck = await _authRepository.IsRegisterCheck(request);
            if(userCheck == true)
            {
                return BadRequest("User already exists");
            }

            await _authServices.AddUser(request);

            return Ok();
        }


        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var user = await _authRepository.IsLoginCheck(request);

            if (!_authServices.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }

            var token = _authServices.LoginUser(request);
            return Ok(token.Result);
        }
    }
}
