using System;

namespace InvoiceApi.Models.UserProfile
{
    public class ProfileDetails
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
