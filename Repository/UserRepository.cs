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
    public class UserRepository : IUserReposiotry
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
            var emailCount = await _sqlService.GetSingleExecuteQueryasync<int>(SqlQuery.EmailCountCheck, user);
            if (emailCount > 0)
            {
                var status = await _sqlService.GetSingleExecuteQueryasync<string>(SqlQuery.CheckStatusOfUser, user) ?? string.Empty;
                if (!string.IsNullOrEmpty(status) && status == RegistrationStatus.InProgress.ToString())
                {
                    response.VerificationCode = Utility.GenerateVerificationCode();
                    Guid alreadyregisteredUserId = await _sqlService.GetSingleExecuteQueryasync<Guid>(SqlQuery.GetUserId, user);
                    if (alreadyregisteredUserId != Guid.Empty)
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
                Guid userId = await _sqlService.GetSingleExecuteQueryasync<Guid>(SqlQuery.SaveUser, user);
                if (userId != Guid.Empty)
                {
                    await _sqlService.GetSingleExecuteQueryasync<int>(SqlQuery.SaveUserSettings,new { UserId=userId });
                    response.UserId = userId.ToString();
                    response.VerificationCode = user.VerificationCode;
                    response.Status = "Success";
                }
            }

            return response;
        }

        #region User Verification Status Update
        /// <summary>
        /// Update verified User Status
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="verificationCode"></param>
        /// <returns></returns>
        public async Task<string> ValidateVerficationCode(VerificationRequest verificationRequest)
        {

            int affectedRow = _sqlService.Execute_Query(SqlQuery.UpdateVerifiedStatus, verificationRequest);
            var response = affectedRow > 0 ? Messages.Success : Messages.Failed;

            return response;
        }
        #endregion
        public async Task<string> ResendCode(VerificationRequest verificationRequest)
        {
            verificationRequest.VerificationCode = Utility.GenerateVerificationCode();
            int affectedRow = _sqlService.Execute_Query(SqlQuery.ResendVerificationCode, verificationRequest);
            var response = affectedRow > 0 ? Messages.Success : Messages.Failed;

            return response;
        }

        # region Resend Email
        public async Task<ResendEmail> ResendEmail(string userId)
        {
            var resendResponse = new ResendEmail();
            resendResponse = await _sqlService.GetSingleExecuteQueryasync<ResendEmail>(SqlQuery.GetResendEmail, new { UserId = userId });
            return resendResponse;
        }

        #endregion

        public async Task<LoginResponse> ValidateUser(LoginRequest loginRequest)
        {
            var loginResponse = new LoginResponse { Status = StatusType.Failure.ToString() };
            var userDetail = new UserDetail();
            userDetail = await _sqlService.GetSingleExecuteQueryasync<UserDetail>(SqlQuery.GetUserDetailsByEmail, loginRequest);
            if (userDetail != null && !string.IsNullOrEmpty(userDetail.Password) && !string.IsNullOrEmpty(userDetail.PasswordSalt))
            {
                loginRequest.Password = Utility.EncryptPassword(loginRequest.Password, userDetail.PasswordSalt);

                if (userDetail.Password.Equals(loginRequest.Password))
                {
                    loginResponse.Status = StatusType.Success.ToString();
                    loginResponse.UserId = userDetail.UserId.ToString();
                    loginResponse.UserName = userDetail.UserName;
                }
            }
            return loginResponse;
        }


        public async Task<PasswordResetResponse> ResetPassword(ResetPasswordRequest resetPasswordRequest)
        {
            var response = new PasswordResetResponse { Status = StatusType.Failure.ToString() };

            var emailCount = await _sqlService.GetSingleExecuteQueryasync<int>(SqlQuery.CheckPasswordResetEmail, resetPasswordRequest);
            
            if(emailCount > 0)
            {


              Guid userId= await _sqlService.GetSingleExecuteQueryasync<Guid>(SqlQuery.GetUserId, resetPasswordRequest);
                var paaswordAttempCount = await _sqlService.GetSingleExecuteQueryasync<int>(SqlQuery.GetPasswordResetAttemptCount, new { UserId= userId });
                if (userId != Guid.Empty && paaswordAttempCount < 5)
                {
                      await _sqlService.GetSingleExecuteQueryasync<int>(SqlQuery.UpdatePasswordResetCount, new { UserId= userId });
                    await _sqlService.GetSingleExecuteQueryasync<int>(SqlQuery.UpdatePasswordResetStatus, new { UserId = userId });
                    response.UserId = userId.ToString();
                    response.Status = StatusType.Success.ToString();
                }
                else
                {
                    response.Status = "Multiple Attempt";
                    response.Messages = "You tried multiple times please contact support";
                }
            }
            return response;
            
        }

        
        public async  Task<ValidateResetPasswordLinkResponse> ValidateResetPasswordLink(ValidateResetPasswordLinkRequest validateResetPasswordLinkRequest)
        {
            var response = new ValidateResetPasswordLinkResponse { Status = StatusType.Failure.ToString() };
            if (!string.IsNullOrEmpty(validateResetPasswordLinkRequest.UserId))
            {
                var userId = Guid.Parse(validateResetPasswordLinkRequest.UserId);
                var userExistCount = await _sqlService.GetSingleExecuteQueryasync<int>(SqlQuery.GetUserIdCount, new { UserId = userId });
                if (userExistCount > 0)
                {
                    response.Status = StatusType.Success.ToString();
                }
            }

            return response;
        }

        public async Task<Base> UpdatePassword(UpdatePasswordRequest updatePasswordRequest)
        {
            var response = new Base { Status = StatusType.Failure.ToString() };
            var decryptedUrl = Utility.DecryptString(updatePasswordRequest.Url);
            updatePasswordRequest.UserId= decryptedUrl?.Split(new Char[] { ',' })[0];
            updatePasswordRequest.PasswordSalt = Utility.CreateSalt(8);
            updatePasswordRequest.Password = Utility.EncryptPassword(updatePasswordRequest.Password, updatePasswordRequest.PasswordSalt);
            Guid affectedRowId = await _sqlService.GetSingleExecuteQueryasync<Guid>(SqlQuery.UpdatePassword, updatePasswordRequest);
            if(affectedRowId != Guid.Empty)
            {
                await _sqlService.GetSingleExecuteQueryasync<Guid>(SqlQuery.UpdateResetAttempCount, updatePasswordRequest);
                response.Status = StatusType.Success.ToString();
            }
            return response;
        }

       
        
    }
}
