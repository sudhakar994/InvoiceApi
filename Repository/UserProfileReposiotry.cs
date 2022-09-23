using InvoiceApi.Constants;
using InvoiceApi.IRepository;
using InvoiceApi.IServices;
using InvoiceApi.Models;
using InvoiceApi.Models.UserProfile;
using System;
using System.Threading.Tasks;

namespace InvoiceApi.Repository
{
    public class UserProfileReposiotry : IUserProfileReposiotry
    {
        private readonly ISqlService _sqlService;

        public UserProfileReposiotry(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }
        public async Task<ProfileDetails> GetProfileDetail(Guid userId)
        {
            var profileDetails = new ProfileDetails();

            if (userId != null && userId != Guid.Empty)
            {
                profileDetails = await _sqlService.GetSingleExecuteQueryasync<ProfileDetails>(SqlQuery.ProfileDetails, new { UserId = userId });
            }

            return profileDetails;
        }

        public async Task<Base> ChangeProfilePassword(UpdatePassword updatePassword)
        {
            var response = new Base { Status = StatusType.Failure.ToString() };
            var userDetail = new UserDetail();
            userDetail = await _sqlService.GetSingleExecuteQueryasync<UserDetail>(SqlQuery.UserProfileDetails, new { UserId = updatePassword.UserId });

            if (userDetail != null)
            {
                if (userDetail != null && !string.IsNullOrEmpty(userDetail.Password) && !string.IsNullOrEmpty(userDetail.PasswordSalt))
                {
                    updatePassword.CurrentPassword = Utility.EncryptPassword(updatePassword.CurrentPassword, userDetail.PasswordSalt);
                    if (userDetail.Password.Equals(updatePassword.CurrentPassword))
                    {
                        updatePassword.PasswordSalt = Utility.CreateSalt(8);
                        updatePassword.NewPassword = Utility.EncryptPassword(updatePassword.NewPassword, updatePassword.PasswordSalt);
                        Guid affectedRowId = await _sqlService.GetSingleExecuteQueryasync<Guid>(SqlQuery.UpdateUserProfilePassword, updatePassword);
                        if (affectedRowId != Guid.Empty)
                        {
                            response.Status = StatusType.Success.ToString();
                        }
                    }
                }
            }
            return response;
        }
    }
}
