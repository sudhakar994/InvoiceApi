using InvoiceApi.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApi
{
    public class JwtMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IConfiguration _configuration;
		public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
		{
			_next = next;
			_configuration = configuration;
		}

		public async Task Invoke(HttpContext context)
		{
			//var url = context.Request.Headers["Referer"].ToString();
			bool isValidTaken = false;
			var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
			var path = context.Request.Path.Value != null ? context.Request.Path.Value.ToLower():string.Empty;
			//not validate register and log in method
			if (path.Contains("authenticate") || path.Contains("swagger") || path.Contains("register") || path.Contains("resetpassword") ||path.Contains("updatepassword"))
			{

				await _next(context);
			} 



			else
			{
				if (!string.IsNullOrWhiteSpace(token))
				{
					
					isValidTaken = ValidateCurrentToken(token);
					if (isValidTaken)
					{
						await _next(context);
					}

					else
					{
						context.Response.StatusCode = StatusCodes.Status401Unauthorized;
					}
				}

				else
				{
					//handle preflighted request
					if (context.Request.Method == "OPTIONS")
					{
						await _next.Invoke(context);
					}
					else
					{
						context.Response.StatusCode = StatusCodes.Status401Unauthorized;
					}
					
					
						
				}
			}
			//if token present in header validate token

		}


	
		public bool ValidateCurrentToken(string token)
		{
			var secretKey = Utility.GetAppSettings(UserConstants.SecretKey);
			var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
			var tokenHandler = new JwtSecurityTokenHandler();
			try
			{
				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					ValidateIssuer = false,
					ValidateAudience = false,
					IssuerSigningKey = mySecurityKey,
				     ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);
			}
			catch
			{
				return false;
			}
			return true;
		}
	
}
}

