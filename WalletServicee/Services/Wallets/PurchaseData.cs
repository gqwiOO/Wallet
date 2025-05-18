namespace WalletServicee.Services.Wallets;

public class PurchaseData
{
    public int UserId { get; set; }
    public int WalletId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
}