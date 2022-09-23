using InvoiceApi.IRepository;
using InvoiceApi.IServices;
using InvoiceApi.Models;
using InvoiceApi.Models.Dashboard;
using InvoiceApi.Models.UserProfile;
using System;
using System.Threading.Tasks;

namespace InvoiceApi.Services
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileReposiotry _userProfileReposiotry;
        public UserProfileService(IUserProfileReposiotry userProfileReposiotry)
        {
            _userProfileReposiotry = userProfileReposiotry;
        }
        public async Task<ProfileDetails> GetProfileDetail(Guid userId)
        {
            return await _userProfileReposiotry.GetProfileDetail(userId);
        }

        public async Task<Base> ChangeProfilePassword(UpdatePassword updatePassword)
        {
            return await _userProfileReposiotry.ChangeProfilePassword(updatePassword);
        }
    }
}
