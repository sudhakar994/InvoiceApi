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
            // check user is already registered
            var emailCount =await _sqlService.GetSingleExecuteQueryasync<int>(SqlQuery.EmailCountCheck, user) ;
            if (emailCount > 0)
            {
                 var status = await _sqlService.GetSingleExecuteQueryasync<string>(SqlQuery.CheckStatusOfUser, user) ?? string.Empty;
                if (!string.IsNullOrEmpty(status) && status == RegistrationStatus.InProgress.ToString())
                {
                    response.VerificationCode = Utility.GenerateVerificationCode();
                   Guid alreadyregisteredUserId= await _sqlService.GetSingleExecuteQueryasync<Guid>(SqlQuery.GetUserId, user);
                    if(alreadyregisteredUserId != Guid.Empty)
                    {
                        response.UserId = alreadyregisteredUserId.ToString();
                        await _sqlService.GetSingleExecuteQueryasync<Guid>(SqlQuery.UpdateVerficationCode, response);
                        response.Status = "Success";
                    }
                }
                else
                {
                    response.Status = status;
                }
               

            }
            else 
            {
                //encrypt password
                user.PasswordSalt = Utility.CreateSalt(8);
                user.Password = Utility.EncryptPassword(user.Password, user.PasswordSalt);
                user.VerificationCode = Utility.GenerateVerificationCode();
                user.Status = Status.InProgress.ToString();
                user.RoleId = Convert.ToInt32(UserRole.user);
                var query = @"Insert Into tbl_UserDetails(UserName,Email,Password,Password_Salt,Verification_Code,Status,Role_Id) OUTPUT INSERTED.User_Id values (@UserName,@Email,@Password,@PasswordSalt,@VerificationCode,@Status,@RoleId)";
                Guid userId = await _sqlService.GetSingleExecuteQueryasync<Guid>(query, user);
                if(userId != Guid.Empty)
                {
                    response.UserId = userId.ToString();
                    response.VerificationCode = user.VerificationCode;
                    response.Status ="Success";
                }
            }

            return response;
        }

        public async Task<LoginResponse> ValidateUser(LoginRequest loginRequest)
        {
            var loginResponse = new LoginResponse { Status = StatusType.Failure.ToString() };
            var userDetail = new UserDetail();
            userDetail = await _sqlService.GetSingleExecuteQueryasync<UserDetail>(SqlQuery.GetUserDetailsByEmail, loginRequest);
            if(userDetail != null && !string.IsNullOrEmpty(userDetail.Password) && !string.IsNullOrEmpty(userDetail.PasswordSalt))
            {
                loginRequest.Password= Utility.EncryptPassword(loginRequest.Password, userDetail.PasswordSalt);

                if (userDetail.Password.Equals(loginRequest.Password))
                {
                    loginResponse.Status = StatusType.Success.ToString();
                    loginResponse.UserId = userDetail.UserId.ToString();
                    loginResponse.UserName = userDetail.UserName;
                }
            }
            return loginResponse;
        }
    }
}
