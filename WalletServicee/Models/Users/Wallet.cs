using WalletServicee.Database;

namespace WalletServicee.Models.Users;

public class Wallet
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    
    public string Currency { get; set; }
    public decimal Balance { get; set; }

}