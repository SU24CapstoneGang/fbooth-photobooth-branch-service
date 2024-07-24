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
        builder.HasIndex(a => a.PhoneNumber).IsUnique();
        //builder.HasData(
        // );
    }

    //private List<Account> SetAccount()
    //{
    //    var accounts = new List<Account>();
    //    accounts.Add(
    //        new Account
    //        {
    //            Status = AccountStatus.Active,
    //            AccountFBID = "bhVrvKzvLOT4qwEVIf3rnAHh7h53",
    //            AccountID = new Guid("1A39CAC3-04B8-4B27-7672-08DC91ECFAC6"),
    //            Address = "123 Le Loi Street, Ben Thanh Ward, District 1, Ho Chi Minh City, Vietnam",
    //            Email = "usemng1122r@example.com",
    //            FirstName = "Minh Chau",
    //            LastName = "Nguyen Thi",
    //            DateOfBirth = new DateOnly(1990, 5, 15),
    //            Role = AccountRole.Customer,
    //            PhoneNumber = "09015684612"
    //        });
    //    accounts.Add(new Account
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
    //        PhoneNumber = "09015684612"
    //    });
    //    foreach (var acc in accounts)
    //    {
    //        acc.SetPassword("thisispassword", IPasswordHasher passwordHasher);
    //    }
    //    return accounts;
    //}
}
