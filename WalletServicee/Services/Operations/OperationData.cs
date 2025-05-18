using WalletServicee.Services.Wallets;

namespace WalletServicee.Controllers.Operations;

public class OperationData
{
    public int Id { get; set; }
    public int WalletId { get; set; }
    public int UserId { get; set; }
    public string OperationType { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }

    public OperationData() { }
    public OperationData(PurchaseData purchase)
    {
        WalletId = purchase.WalletId;
        UserId = purchase.UserId;
        OperationType = Operations.OperationType.Purchase;
        Amount = purchase.Amount;
        Currency = purchase.Currency;
    }
}