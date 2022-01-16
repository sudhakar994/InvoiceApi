using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Constants
{
    
        public enum Status
        {
            InProgress,
            Verified,
        }

    public enum UserRole
    {
        admin=1,
        user=2
    }

    public enum RegistrationStatus
    {
        Verified,
        Failure,
        InProgress
    }

    public enum StatusType
    {
        Success,
        Failure
    }
    
}
