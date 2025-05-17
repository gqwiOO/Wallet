namespace WalletServicee.Models.Users;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    
    public int WalletId { get; set; }
}