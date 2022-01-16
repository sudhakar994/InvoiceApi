using InvoiceApi.IServices;
using InvoiceApi.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using InvoiceApi.Constants;
using InvoiceApi.Repository;
using InvoiceApi.IRepository;

namespace InvoiceApi.Services
{
    public class UserService:IUserService
    {
        private readonly IUserReposiotry _userReposiotry;
        public UserService(IUserReposiotry userRepository)
        {
            _userReposiotry = userRepository;
        }

        public string GenerateJwtToken(User user)
        {
            // generate token that is valid for 10 minutes only
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Utility.GetAppSettings(UserConstants.SecretKey));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("UserId", user.UserId.ToString()),
                }),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async  Task<User> Register(User user)
        {
           return await _userReposiotry.Register(user);
           
        }

        public async Task<LoginResponse> ValidateUser(LoginRequest loginRequest)
        {
            return await _userReposiotry.ValidateUser(loginRequest);
        }
    }
}
