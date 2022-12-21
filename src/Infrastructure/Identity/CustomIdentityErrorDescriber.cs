using Jorda.Server.Infrastructure.Common.Resources;
using Microsoft.AspNetCore.Identity;

namespace Jorda.Server.Infrastructure.Identity;

public class CustomIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DuplicateEmail(string email)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateEmail),
            Description = string.Format(IdentityErrorDescriberLocalization.DuplicateEmail, email)
        };
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description = string.Format(IdentityErrorDescriberLocalization.DuplicateUserName, userName)
        };
    }

    public override IdentityError InvalidEmail(string? email)
    {
        return new IdentityError
        {
            Code = nameof(InvalidEmail),
            Description = string.Format(IdentityErrorDescriberLocalization.InvalidEmail, email)
        };
    }

    public override IdentityError DuplicateRoleName(string role)
    {
        return new IdentityError
        {
            Code = nameof(DuplicateRoleName),
            Description = string.Format(IdentityErrorDescriberLocalization.DuplicateRoleName, role)
        };
    }

    public override IdentityError InvalidRoleName(string? role)
    {
        return new IdentityError
        {
            Code = nameof(InvalidRoleName),
            Description = string.Format(IdentityErrorDescriberLocalization.InvalidRoleName, role)
        };
    }

    public override IdentityError InvalidToken()
    {
        return new IdentityError
        {
            Code = nameof(InvalidToken),
            Description = IdentityErrorDescriberLocalization.InvalidToken
        };
    }

    public override IdentityError InvalidUserName(string? userName)
    {
        return new IdentityError
        {
            Code = nameof(InvalidUserName),
            Description = string.Format(IdentityErrorDescriberLocalization.InvalidUserName, userName)
        };
    }

    public override IdentityError LoginAlreadyAssociated()
    {
        return new IdentityError
        {
            Code = nameof(LoginAlreadyAssociated),
            Description = IdentityErrorDescriberLocalization.LoginAlreadyAssociated
        };
    }

    public override IdentityError PasswordMismatch()
    {
        return new IdentityError
        {
            Code = nameof(PasswordMismatch),
            Description = IdentityErrorDescriberLocalization.PasswordMismatch
        };
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description = IdentityErrorDescriberLocalization.PasswordRequiresDigit
        };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresLower),
            Description = IdentityErrorDescriberLocalization.PasswordRequiresLower
        };
    }

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = IdentityErrorDescriberLocalization.PasswordRequiresNonAlphanumeric
        };
    }

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresUniqueChars),
            Description = string.Format(IdentityErrorDescriberLocalization.PasswordRequiresUniqueChars, uniqueChars)
        };
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError
        {
            Code = nameof(PasswordRequiresUpper),
            Description = IdentityErrorDescriberLocalization.PasswordRequiresUpper
        };
    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description = string.Format(IdentityErrorDescriberLocalization.PasswordTooShort, length)
        };
    }

    public override IdentityError UserAlreadyHasPassword()
    {
        return new IdentityError
        {
            Code = nameof(UserAlreadyHasPassword),
            Description = IdentityErrorDescriberLocalization.UserAlreadyHasPassword
        };
    }

    public override IdentityError UserAlreadyInRole(string role)
    {
        return new IdentityError
        {
            Code = nameof(UserAlreadyInRole),
            Description = string.Format(IdentityErrorDescriberLocalization.UserAlreadyInRole, role)
        };
    }

    public override IdentityError UserNotInRole(string role)
    {
        return new IdentityError
        {
            Code = nameof(UserNotInRole),
            Description = string.Format(IdentityErrorDescriberLocalization.UserNotInRole, role)
        };
    }

    public override IdentityError UserLockoutNotEnabled()
    {
        return new IdentityError
        {
            Code = nameof(UserLockoutNotEnabled),
            Description = IdentityErrorDescriberLocalization.UserLockoutNotEnabled
        };
    }

    public override IdentityError RecoveryCodeRedemptionFailed()
    {
        return new IdentityError
        {
            Code = nameof(RecoveryCodeRedemptionFailed),
            Description = IdentityErrorDescriberLocalization.RecoveryCodeRedemptionFailed
        };
    }

    public override IdentityError ConcurrencyFailure()
    {
        return new IdentityError
        {
            Code = nameof(ConcurrencyFailure),
            Description = IdentityErrorDescriberLocalization.ConcurrencyFailure
        };
    }

    public override IdentityError DefaultError()
    {
        return new IdentityError
        {
            Code = nameof(DefaultError),
            Description = IdentityErrorDescriberLocalization.DefaultIdentityError
        };
    }
}
