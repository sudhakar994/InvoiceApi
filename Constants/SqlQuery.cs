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

        //Email Related Query
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
        public const string GetResendEmail = "Select [User_Id],UserName,Email,Verification_Code AS VerificationCode From tbl_UserDetails WHERE [User_Id]=@UserId And Is_Deleted=0";
        public const string UpdateResetAttempCount = @"Update tbl_User_Settings set Reset_Code_Attempt=0 OUTPUT INSERTED.User_Id WHERE User_Id=@UserId  and Is_Deleted=0";
        public const string UpdatePassword = @"Update tbl_UserDetails set Password_Reset_Status='Verified', Password=@Password,Password_Salt=@PasswordSalt OUTPUT INSERTED.User_Id WHERE User_Id=@UserId and Password_Reset_Status='InProgress' and Is_Deleted=0";
        public const string GetBusinessDetails = @"SELECT Business_Id as BusinessId,User_Id as UserId,Country_Id as CountryId,Business_Name as BusinessName,Business_Email as BusinessEmail,Business_Phone as BusinessPhone,Phone_Code as PhoneCode,Address, City,Zipcode,State FROM tbl_Business_Details WHERE User_Id=@UserId and IsDeleted=0";
        public const string GetClientDetails = @"SELECT Client_Id as ClientId,User_Id as UserId,Country_Id as CountryId,Client_Name as ClientName,Client_Email as ClientEmail,Client_Phone as ClientPhone,Phone_Code as PhoneCode,Address, City,Zipcode,State FROM tbl_Client_Details WHERE User_Id=@UserId and IsDeleted=0";
        public const string SaveBusiness = @"INSERT INTO tbl_Business_Details (User_Id,Country_Id,Business_Name,Business_Email,Business_Phone,Phone_Code,Address,City,Zipcode,State)  OUTPUT INSERTED.Business_Id VALUES(@UserId,@CountryId,@BusinessName,@BusinessEmail,@BusinessPhone,@PhoneCode,@Address,@City,@Zipcode,@State)";
        public const string UpdateBusinessByBusinessId = @"Update tbl_Business_Details set Country_Id=@CountryId,Business_Name=@BusinessName,Business_Email=@BusinessEmail,Business_Phone=@BusinessPhone,Phone_Code=@PhoneCode,Address=@Address,City=@City,Zipcode=@Zipcode,State=@State  OUTPUT INSERTED.Business_Id where Business_Id=@BusinessId AND User_Id=@UserId";
        public const string SaveClient = @"INSERT INTO tbl_Client_Details (User_Id,Country_Id,Client_Name,Client_Email,Client_Phone,Phone_Code,Address,City,Zipcode,State)  OUTPUT INSERTED.Client_Id VALUES(@UserId,@CountryId,@ClientName,@ClientEmail,@ClientPhone,@PhoneCode,@Address,@City,@Zipcode,@State)";
        public const string UpdateClientsByClientId = @"Update tbl_Client_Details set Country_Id=@CountryId,Client_Name=@ClientName,Client_Email=@ClientEmail,Client_Phone=@ClientPhone,Phone_Code=@PhoneCode,Address=@Address,City=@City,Zipcode=@Zipcode,State=@State where Client_Id=@ClientId AND User_Id=@UserId";
        public const string SaveInvoiceDetails = @"INSERT INTO tbl_Invoice_Details (User_Id, Invoice_No,Invoice_Date,Due_Date,Business_Id,Client_Id,Notes,Terms,Tax,Discount,Total,SubTotal,Taxtype,DiscountType,Template_Name) OUTPUT INSERTED.Invoice_Id  VALUES(@UserId,@InvoiceNumber,@InvoiceDate,@InvoiceDueDate,@BusinessId,@ClientId,@Notes,@Terms,@Tax,@Discount,@Total,@SubTotal,@Taxtype,@DiscountType,@TemplateName)";
        public const string SaveTransactionDetails = @"INSERT INTO tbl_Transaction_Details (Invoice_Id,Description,Quantity,Rate,Amount) OUTPUT INSERTED.Transaction_Id  VALUES(@InvoiceId,@ItemDescription,@Quantity,@Rate,@Amount)";
    }
}
