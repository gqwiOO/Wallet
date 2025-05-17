namespace WalletServicee.Controllers.Requests;

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class RegistrationRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}