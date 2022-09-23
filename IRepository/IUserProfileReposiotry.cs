using InvoiceApi.Models;
using InvoiceApi.Models.UserProfile;
using System;
using System.Threading.Tasks;

namespace InvoiceApi.IRepository
{
    public interface IUserProfileReposiotry
    {
        Task<ProfileDetails> GetProfileDetail(Guid userId);
        Task<Base> ChangeProfilePassword(UpdatePassword updatePassword);

    }
}
