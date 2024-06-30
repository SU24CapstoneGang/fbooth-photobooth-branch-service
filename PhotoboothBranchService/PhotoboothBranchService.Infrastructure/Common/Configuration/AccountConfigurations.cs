using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotoboothBranchService.Domain.Entities;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Infrastructure.Common.Configuration;

public class AccountConfigurations : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("Accounts");
        // Primary key
        builder.HasKey(u => u.AccountID);
        builder.Property(u => u.AccountID).HasColumnName("AccountID")
            .ValueGeneratedOnAdd();

        builder.Property(a => a.AccountFBID).IsRequired();
        builder.Property(a => a.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(a => a.LastName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.PhoneNumber).HasMaxLength(11);
        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.PasswordSalt).IsRequired();
        builder.Property(u => u.ResetPasswordToken).IsRequired(false);
        builder.Property(a => a.DateOfBirth).IsRequired();
        builder.Property(a => a.Address).IsRequired().HasMaxLength(100);

        // Account role  enum mapping
        builder.Property(a => a.Role)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (AccountRole)Enum.Parse(typeof(AccountRole), v));

        // Relationship with Sessions
        builder.HasMany(a => a.SessionOrder)
            .WithOne(s => s.Account)
            .HasForeignKey(s => s.AccountID)
            .IsRequired();

        // Account status enum mapping
        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion(
                v => v.ToString(),
                v => (AccountStatus)Enum.Parse(typeof(AccountStatus), v));

        // Indexes
        builder.HasIndex(a => a.Email).IsUnique();

        //builder.HasData(
        //    new Account
        //    {
        //        Status = AccountStatus.Active,
        //        AccountFBID = "bhVrvKzvLOT4qwEVIf3rnAHh7h53",
        //        AccountID = new Guid("1A39CAC3-04B8-4B27-7672-08DC91ECFAC6"),
        //        Address = "123 Le Loi Street, Ben Thanh Ward, District 1, Ho Chi Minh City, Vietnam",
        //        Email = "usemng1122r@example.com",
        //        FirstName = "Minh Chau",
        //        LastName = "Nguyen Thi",
        //        DateOfBirth = new DateOnly(1990, 5, 15),
        //        Role = AccountRole.Customer,
        //        PasswordSalt = ConvertHexStringToByteArray("0xEAFC60690B29B4D7C7F008603173E072"),
        //        PasswordHash = ConvertHexStringToByteArray("0xC2114088053CBC8E5D38C251D3EA601FE77179DB2C26634758AD0B99273079891E94AE57F9F170573E6DB7F27F27A7B1EBBE108DF10B934C63FB521D4AB79653"),
        //        PhoneNumber = "09015684612"
        //    },
        //    new Account
        //    {
        //        Status = AccountStatus.Active,
        //        AccountFBID = "SdipDLaqXJdzYhqMGueKXEjw9mF3",
        //        AccountID = new Guid("68DA2408-90B1-4418-7673-08DC91ECFAC6"),
        //        Address = "654 Tran Hung Dao Street, District 5, Ho Chi Minh City, Vietnam",
        //        Email = "usecuss1122r@example.com",
        //        FirstName = "Thanh Ha",
        //        LastName = "Pham",
        //        DateOfBirth = new DateOnly(1995, 2, 18),
        //        Role = AccountRole.Customer,
        //        PasswordSalt = ConvertHexStringToByteArray("0xF62AF25D3C77EFC72D535046ED826ACA"),
        //        PasswordHash = ConvertHexStringToByteArray("0xE2074982D768A24F65BFB01D72D8272B0ECC3313F2F98CCDB74F65F405F2AC133DB881DB1630911E9124D4DA12B494645AAFCC35C496D7E465976431FD8984FC"),
        //        PhoneNumber = "09015684612"
        //    });
    }
    //private byte[] ConvertHexStringToByteArray(string hexString)
    //{
    //    int length = hexString.Length;
    //    byte[] byteArray = new byte[length / 2];
    //    for (int i = 0; i < length; i += 2)
    //    {
    //        byteArray[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
    //    }
    //    return byteArray;
    //}
}
