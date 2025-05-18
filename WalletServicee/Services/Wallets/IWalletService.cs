using WalletServicee.Models.Users;

namespace WalletServicee.Services.Wallets;


public interface IWalletService
{
    event Action<PurchaseData> OnPurchased;
    Task<List<Wallet>> GetAllWalletsAsync();
    Task<Wallet?> AddCurrencyAsync(int userId, decimal amount, string currency);
    Task<(bool success, Wallet? wallet)> PurchaseAsync(int userId, decimal price, string currency);
    Task<bool> CanPurchaseAsync(decimal price, string currency);
    
    Wallet GetCurrentWallet();
}