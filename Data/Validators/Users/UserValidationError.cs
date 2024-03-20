namespace Data.Validators.Users;

[Flags]
public enum UserValidationError
{
    None = 0,
    EmailAlreadyExist = 1,
    EmailInvalidFormat = 2,
    PasswordContainsForbiddenSymbols = 4,
}
