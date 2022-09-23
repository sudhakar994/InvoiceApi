using InvoiceApi.Models;
using InvoiceApi.Models.UserProfile;
using System;
using System.Threading.Tasks;

namespace InvoiceApi.IServices
{
    public interface IUserProfileService
    {
        Task<ProfileDetails> GetProfileDetail(Guid userId);
        Task<Base> ChangeProfilePassword(UpdatePassword updatePassword);


    }
}
