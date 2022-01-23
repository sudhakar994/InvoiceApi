using System;
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
        public const string UpdateVerifiedStatus = "UPDATE tbl_UserDetails SET [Status]='Verified' WHERE User_Id=@UserId AND Verification_Code=@VerificationCode AND [Status]='InProgress' AND Is_Deleted=0";
        public const string ResendVerificationCode = "UPDATE tbl_UserDetails SET Verification_Code=@VerificationCode,[Status]='InProgress' WHERE [User_Id]=@UserId AND Is_Deleted=0";
        public const string CheckPasswordResetEmail = "Select COUNT(Email) From tbl_UserDetails Where Email=@Email And   Status='Verified' And  Is_Deleted=0";
        public const string UpdatePasswordResetStatus = @"UPDATE tbl_UserDetails SET Password_Reset_Status='InProgress' Where User_Id=@UserId and Is_Deleted = 0";
        public const string GetPasswordResetAttemptCount = @"Select Reset_Code_Attempt From tbl_User_Settings 
                   Where User_Id= @UserId  And  Is_Deleted=0";
        public const string UpdatePasswordResetCount = @"Update tbl_User_Settings set Reset_Code_Attempt=(Reset_Code_Attempt + 1)  Where User_Id= @UserId  And  Is_Deleted=0";
        public const string SaveUserSettings = @"Insert into tbl_User_Settings (User_Id,Resent_Code_Attempt,Reset_Code_Attempt) values (@UserId,0,0)";
        public const string SaveUser = @"Insert Into tbl_UserDetails(UserName,Email,Password,Password_Salt,Verification_Code,Status,Role_Id) OUTPUT INSERTED.User_Id values (@UserName,@Email,@Password,@PasswordSalt,@VerificationCode,@Status,@RoleId)";
        public const string GetUserIdCount = @"Select COUNT(User_Id) From tbl_UserDetails Where User_Id=@UserId And Is_Deleted=0";
    }
}
