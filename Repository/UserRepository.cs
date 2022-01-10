using InvoiceApi.Constants;
using InvoiceApi.IRepository;
using InvoiceApi.IServices;
using InvoiceApi.Models;
using InvoiceApi.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
namespace InvoiceApi.Repository
{
    public class UserRepository: IUserReposiotry
    {
        private readonly ISqlService _sqlService;

        public UserRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
         
        }
        public async Task<User> Register(User user)
        {
            var response = new User();
            var parameters = new DynamicParameters();
            parameters.Add("@Email", user.Email);
            // check user is already registered
            var emailCount =await _sqlService.GetSingleExecuteQueryasync<int>(SqlQuery.EmailCountCheck, parameters) ;
            if(emailCount == 0)
            {
                //encrypt password
                user.PasswordSalt = Utility.CreateSalt(8);
                user.Password = Utility.EncryptPassword(user.Password, user.PasswordSalt);
                var verficationCode = Utility.GenerateVerificationCode();
                parameters = new DynamicParameters();
                parameters.Add("@UserName", user.UserName);
                parameters.Add("@Email", user.Email);
                parameters.Add("@Password", user.Password);
                parameters.Add("@PasswordSalt", user.PasswordSalt);
                parameters.Add("@Status", Status.InProgress.ToString());
                parameters.Add("@VerificationCode", verficationCode);
                parameters.Add("@RoleId", Convert.ToInt32(UserRole.user));
                var query = @"Insert Into tbl_UserDetails(UserName,Email,Password,Password_Salt,Verification_Code,Status,Role_Id) OUTPUT INSERTED.User_Id values (@UserName,@Email,@Password,@PasswordSalt,@VerificationCode,@Status,@RoleId)";
                Guid userId = await _sqlService.GetSingleExecuteQueryasync<Guid>(query, parameters);
                if(userId != Guid.Empty)
                {
                    response.UserId = userId.ToString();
                    response.VerificationCode = verficationCode;
                }
            }

            return response;
        }
    }
}
