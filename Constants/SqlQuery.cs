using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Constants
{
    public static class SqlQuery
    {
        public const  string EmailCountCheck = "Select COUNT(Email) From tbl_UserDetails Where Email=@Email And Is_Deleted=0";
        public const string TestQuery = "Select Email,UserName From tbl_UserDetails where Status In(@Status) ";
        public const string CheckStatusOfUser = "Select Status From tbl_UserDetails Where Email=@Email And Is_Deleted=0";
        public const string GetUserId = "Select User_Id From tbl_UserDetails Where Email=@Email And Is_Deleted=0";
        public const string UpdateVerficationCode = "Update tbl_UserDetails set verification_code=@VerificationCode Where User_Id=@UserId And Is_Deleted=0";


    }
}
