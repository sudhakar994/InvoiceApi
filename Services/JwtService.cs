using InvoiceApi.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace InvoiceApi.Services
{
    public class JwtService : IJwtService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public JwtService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

        }

        //public string GetUserEmailFromJwt()
        //{
        //    string email = string.Empty;
        //    var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split("").Last();
        //    if (!string.IsNullOrWhiteSpace(token))
        //    {
        //        var handler = new JwtSecurityTokenHandler();
        //        var jwtSecurityToken = handler.ReadJwtToken(token);
        //        email = jwtSecurityToken.Claims.First(claim => claim.Type == "Email").Value;
        //    }
        //    return email;
        //}

        public Guid GetUserIdFromJwt()
        {
            Guid userId = Guid.Empty;
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (!string.IsNullOrWhiteSpace(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                userId = Guid.Parse(jwtSecurityToken.Claims.First(claim => claim.Type == "UserId").Value);
            }
            return userId;
        }




    }
}
