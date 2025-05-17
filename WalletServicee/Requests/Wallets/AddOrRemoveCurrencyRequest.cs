namespace WalletServicee.Requests.Wallets;

public class AddOrRemoveCurrencyRequest
{
    public int WalletId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }
}