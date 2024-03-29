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

        //Invoice Related Query
        public const string GetBusinessDetails = @"SELECT Business_Id as BusinessId,User_Id as UserId,Country_Id as CountryId,Business_Name as BusinessName,Business_Email as BusinessEmail,Business_Phone as BusinessPhone,Phone_Code as PhoneCode,Address, City,Zipcode,State FROM tbl_Business_Details WHERE User_Id=@UserId and IsDeleted=0";
        public const string GetClientDetails = @"SELECT Client_Id as ClientId,User_Id as UserId,Country_Id as CountryId,Client_Name as ClientName,Client_Email as ClientEmail,Client_Phone as ClientPhone,Phone_Code as PhoneCode,Address, City,Zipcode,State FROM tbl_Client_Details WHERE User_Id=@UserId and IsDeleted=0";
        public const string SaveBusiness = @"INSERT INTO tbl_Business_Details (User_Id,Country_Id,Business_Name,Business_Email,Business_Phone,Phone_Code,Address,City,Zipcode,State)  OUTPUT INSERTED.Business_Id VALUES(@UserId,@CountryId,@BusinessName,@BusinessEmail,@BusinessPhone,@PhoneCode,@Address,@City,@Zipcode,@State)";
        public const string UpdateBusinessByBusinessId = @"Update tbl_Business_Details set Country_Id=@CountryId,Business_Name=@BusinessName,Business_Email=@BusinessEmail,Business_Phone=@BusinessPhone,Phone_Code=@PhoneCode,Address=@Address,City=@City,Zipcode=@Zipcode,State=@State  OUTPUT INSERTED.Business_Id where Business_Id=@BusinessId AND User_Id=@UserId";
        public const string SaveClient = @"INSERT INTO tbl_Client_Details (User_Id,Country_Id,Client_Name,Client_Email,Client_Phone,Phone_Code,Address,City,Zipcode,State)  OUTPUT INSERTED.Client_Id VALUES(@UserId,@CountryId,@ClientName,@ClientEmail,@ClientPhone,@PhoneCode,@Address,@City,@Zipcode,@State)";
        public const string UpdateClientsByClientId = @"Update tbl_Client_Details set Country_Id=@CountryId,Client_Name=@ClientName,Client_Email=@ClientEmail,Client_Phone=@ClientPhone,Phone_Code=@PhoneCode,Address=@Address,City=@City,Zipcode=@Zipcode,State=@State where Client_Id=@ClientId AND User_Id=@UserId";
        public const string SaveInvoiceDetails = @"INSERT INTO tbl_Invoice_Details (User_Id, Invoice_No,Invoice_Date,Due_Date,Business_Id,Client_Id,Notes,Terms,Tax,Discount,Total,SubTotal,Taxtype,DiscountType,Template_Name,TaxPercentage,DiscountPercentage,IsCustomDate,IsCustomDueDate,Custom_Date,Custom_DueDate,currency_code,taxlabel,DiscountLabel,TotalLabel,SubTotalLabel,TermsLabel,SignatureLabel) OUTPUT INSERTED.Invoice_Id  VALUES(@UserId,@InvoiceNumber,@InvoiceDate,@InvoiceDueDate,@BusinessId,@ClientId,@Notes,@Terms,@Tax,@Discount,@Total,@SubTotal,@Taxtype,@DiscountType,@TemplateName,@TaxPercentage,@DiscountPercentage,@IsCustomDate,@IsCustomDueDate,@CustomDate,@CustomDueDate,@CurrencyCode,@TaxLabel,@DiscountLabel,@TotalLabel,@SubTotalLabel,@TermsLabel,@SignatureLabel)";
        public const string SaveTransactionDetails = @"INSERT INTO tbl_Transaction_Details (Invoice_Id,Description,Quantity,Rate,Amount,IsConsiderForTax) OUTPUT INSERTED.Transaction_Id  VALUES(@InvoiceId,@ItemDescription,@Quantity,@Rate,@Amount,@IsConsiderForTax)";
        public const string GetInvoiceDetails = @"Select Inv.Invoice_Id as InvoiceId,Inv.Invoice_No as InvoiceNo,Inv.Invoice_date as InvoiceDate,Inv.Due_Date as InvoiceDueDate,Inv.Business_Id as BusinessId,Inv.Client_Id as ClientId,Inv.Total as InvoiceAmount,Custom_Date as CustomDate,IsCustomDate as IsCustomDate,IsCustomDueDate as IsCustomDueDate,Custom_DueDate as CustomDueDate, Currency_code as CurrencyCode,(Select Business_Name from tbl_business_details where Business_Id=Inv.Business_Id and Isdeleted=0) as BusinessName,(Select Client_Name from tbl_client_details where Client_Id=Inv.Client_Id and Isdeleted=0) as ClientName from tbl_invoice_details Inv  where inv.user_Id=@UserId and inv.isdeleted=0";
        public const string GetInvoiceDetailByInvoiceId = @"SELECT Invoice_Id as InvoiceId,Invoice_No as InvoiceNumber,Invoice_Date as  InvoiceDate,Due_Date as InvoiceDueDate,Business_Id as BusinessId,Client_Id as ClientId,Notes,Tax,Discount,Total,Template_Name as TemplateName,Terms,SubTotal,Taxtype,DiscountType,TaxPercentage,DiscountPercentage,IsCustomDate,IsCustomDueDate,Custom_Date as CustomDate,Custom_DueDate as CustomDueDate,Currency_code as CurrencyCode,TaxLabel,DiscountLabel,TotalLabel,SubTotalLabel,SignatureLabel,TermsLabel from tbl_invoice_details WHERE Invoice_Id=@InvoiceId and User_Id=@UserId and IsDeleted=0";
        public const string GetTransactionDetails = @"Select Transaction_Id as TransactionId,Invoice_Id as InvoiceId,Description as ItemDescription,Quantity,Rate,Amount,IsConsiderForTax from tbl_transaction_details where Invoice_Id=@InvoiceId";
        public const string DeleteTransactionDetails = @"Delete from tbl_transaction_details where Invoice_Id=@InvoiceId";
        public const string DeleteLogoDetails = @"Delete From tbl_logo_details where User_Id=@UserId and Invoice_Id=@InvoiceId";
        public const string UpdateInvoiceDetails = @"Update tbl_Invoice_Details set Invoice_No=@InvoiceNumber,Invoice_Date=@InvoiceDate,Due_Date=@InvoiceDueDate,Business_Id=@BusinessId,Client_Id=@ClientId,Notes=@Notes,Tax=@Tax,Discount=@Discount,Total=@Total,Template_Name=@TemplateName,Terms=@Terms,SubTotal=@SubTotal,Taxtype=@Taxtype,DiscountType=@DiscountType,TaxPercentage=@TaxPercentage,DiscountPercentage=@DiscountPercentage,Custom_DueDate=@CustomDueDate,Custom_Date=@CustomDate,IsCustomDate=@IsCustomDate,IsCustomDueDate=@IsCustomDueDate,currency_code=@CurrencyCode,taxlabel=@TaxLabel,DiscountLabel=@DiscountLabel,SubTotalLabel=@SubTotalLabel,TotalLabel=@TotalLabel,TermsLabel=@TermsLabel,SignatureLabel=@SignatureLabel  OUTPUT INSERTED.Invoice_Id where Invoice_Id=@InvoiceId and User_Id=@UserId";
        public const string DeleteInvoiceDetails = @"DELETE FROM tbl_Invoice_Details where user_Id=@UserId and Invoice_Id=@InvoiceId";
        public const string SaveLogoDetails = @"Insert into tbl_logo_details (invoice_id,User_Id,Logo_Name,Image) values (@InvoiceId,@UserId,@LogoName,@ImageBase64String)";
        public const string GetImageSrcByInvoiceId = @"Select Image FROM tbl_logo_details  WHERE Invoice_Id=@InvoiceId and User_Id=@UserId";
        public const string UpdateLogoDetails = @"Update tbl_logo_details set Image=@ImageBase64String,Logo_Name=@LogoName  where User_id=@UserId and Invoice_Id=@InvoiceId and Logo_Id=@LogoId";
        public const string GetBusinessDetailsByBusinessId = @"SELECT Business_Name as BusinessName,Business_Email as BusinessEmail,Business_Phone as BusinessPhone,Phone_Code as PhoneCode,Address ,City,Zipcode,State,Country_Id as CountryId FROM tbl_Business_Details WHERE Business_Id=@BusinessId and User_Id=@UserId";
        public const string GetClientDetailsByClientId = @"SELECT Client_Name as ClientName,Client_Email as ClientEmail,Client_Phone as ClientPhone,Phone_Code as PhoneCode,Address ,City,Zipcode,State,Country_Id as CountryId FROM tbl_Client_Details WHERE Client_Id=@ClientId and User_Id=@UserId";
        public const string SelectLogoId = @"Select Logo_Id LogoId From tbl_logo_details  WHERE Invoice_Id=@InvoiceId and User_Id=@UserId";
        public const string SelectSignatureId = @"Select Signature_Id SignatureId From tbl_signature_details  WHERE Invoice_Id=@InvoiceId and User_Id=@UserId";
        public const string UpdateSignatureDetails = @"Update tbl_signature_details set Image=@SignatureBase64String  where User_id=@UserId and Invoice_Id=@InvoiceId and Signature_Id=@SignatureId";
        public const string SaveSignatureDetails = @"Insert into tbl_signature_details (invoice_id,User_Id,Image) values (@InvoiceId,@UserId,@SignatureBase64String)";
        public const string GetSignatureImageSrcByInvoiceId = @"Select Image FROM tbl_signature_details  WHERE Invoice_Id=@InvoiceId and User_Id=@UserId";
        public const string DeleteSignatureDetails = @"Delete From tbl_signature_details where User_Id=@UserId and Invoice_Id=@InvoiceId";

        //UserProfile Related Query
        public const string ProfileDetails = @"Select [User_Id] AS UserId,UserName,Email FROM tbl_UserDetails WHERE User_Id=@UserId  AND [Status]='Verified' AND Is_Deleted=0";
        public const string UserProfileDetails = @"Select [User_Id] AS UserId,UserName,Email,Password,Password_Salt as PasswordSalt FROM tbl_UserDetails WHERE User_Id=@UserId AND [Status]='Verified' AND Is_Deleted=0";
        public const string UpdateUserProfilePassword = @"UPDATE tbl_UserDetails SET [Password]=@NewPassword,Password_Salt=@PasswordSalt OUTPUT INSERTED.[User_Id] WHERE User_Id=@UserId AND [Status]='Verified' AND Is_Deleted=0";
    }
}
