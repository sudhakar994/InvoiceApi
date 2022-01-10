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
            var parameters = new DynamicParameters();
            parameters.Add("@Email", user.Email);
            var emailCount =await _sqlService.GetSingleExecuteQueryasync<int>(SqlQuery.EmailCountCheck, parameters) ;
            var query = @"Insert Into tbl_UserDetails(UserName,Email,Password,Password_Salt,Verification_Code,Status) values (@UserName,@Email,@Password,@VerificationCode,@Status)";
            return await _sqlService.GetSingleExecuteQueryasync<User>(query, parameters);
        }
    }
}
