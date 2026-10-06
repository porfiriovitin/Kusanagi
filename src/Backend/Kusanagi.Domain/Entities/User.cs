namespace Kusanagi.Domain.Entities;

public class User : EntityBase
{
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public bool IsAdmin { get; private set; }
    public bool IsEmailVerified { get; private set; }
    public bool IsCellphoneVerified { get; private set; }

    public void Deactivate()
    {
        Active = false;
    }

    public void VerifyEmail()
    {
        IsEmailVerified = true;
    }

    public void VerifyCellphone()
    {
        IsCellphoneVerified = true;
    }

    public void UpdatePassword(string newPasswordHash)
    {
        Password = newPasswordHash;
    }

}


