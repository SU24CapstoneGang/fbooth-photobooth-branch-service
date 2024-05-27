﻿using PhotoboothBranchService.Domain.Common.Interfaces;
using PhotoboothBranchService.Domain.Enum;

namespace PhotoboothBranchService.Domain.Entities;

public class Account
{
    public Guid AccountID { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public byte[] PasswordHash { get; set; } = default!;
    public byte[] PasswordSalt { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; }
    public string Address { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public AccountStatus Status { get; set; }
    public Guid RoleID { get; set; }
    public virtual Role Role { get; set; }
    public virtual List<PhotoBoothBranch> PhotoBoothBranches { get; set; }
    public virtual List<Session> Sessions { get; set; }

    public void SetPassword(string plainTextPassword, IPasswordHasher passwordHasher)
    {
        PasswordSalt = passwordHasher.GenerateSalt();
        PasswordHash = passwordHasher.HashPassword(plainTextPassword, PasswordSalt);
    }

    public bool VerifyPassword(string plainTextPassword, IPasswordHasher passwordHasher)
    {
        var hash = passwordHasher.HashPassword(plainTextPassword, PasswordSalt);
        return hash.SequenceEqual(PasswordHash);
    }
}
