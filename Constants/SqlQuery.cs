﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApi.Constants
{
    public static class SqlQuery
    {
        public const string EmailCountCheck = "Select COUNT(Email) From tbl_UserDetails Where Email=@Email And Is_Deleted=0";
        public const string TestQuery = "Select Email,UserName From tbl_UserDetails where Status In(@Status) ";
        public const string CheckStatusOfUser = "Select Status From tbl_UserDetails Where Email=@Email And Is_Deleted=0";
        public const string GetUserId = "Select User_Id From tbl_UserDetails Where Email=@Email And Is_Deleted=0";
        public const string UpdateVerficationCode = "Update tbl_UserDetails set verification_code=@VerificationCode Where User_Id=@UserId And Is_Deleted=0";
        public const string GetUserDetailsByEmail = "SELECT Password,Password_Salt as PasswordSalt,User_Id as UserId,UserName From tbl_UserDetails Where Email=@Email And Status='Verified'  And Is_Deleted=0";

        //Email Related Query
        public const string UpdateVerifiedStatus = "UPDATE tbl_UserDetails SET [Status]='Verified' WHERE User_Id=@UserId AND Verification_Code=@VerificationCode AND [Status]='InProgress' AND Is_Deleted=0";
        public const string ResendVerificationCode = "UPDATE tbl_UserDetails SET Verification_Code=@VerificationCode,[Status]='InProgress' WHERE [User_Id]=@UserId AND Is_Deleted=0";
        public const string GetResendEmail = "Select [User_Id],UserName,Email,Verification_Code AS VerificationCode From tbl_UserDetails WHERE [User_Id]=@UserId And Is_Deleted=0";


    }
}
