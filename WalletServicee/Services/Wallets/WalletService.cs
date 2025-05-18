using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using WalletServicee.Controllers.Operations;
using WalletServicee.Database;
using WalletServicee.Models.Users;

namespace WalletServicee.Services.Wallets;

public class WalletService : IWalletService
{
    private readonly AppDbContext _context;
    private IHttpContextAccessor _accessor;
    private IUserService _userService;
    private IOperationService _operationService;

    public WalletService(AppDbContext context,IHttpContextAccessor accessor, IUserService userService, IOperationService operationService)
    {
        _operationService = operationService;
        _userService = userService;
        _accessor = accessor;
        _context = context;
    }

    public event Action<PurchaseData>? OnPurchased;

    public async Task<List<Wallet>> GetAllWalletsAsync()
    {
        return await _context.Wallets.ToListAsync();
    }

    public async Task<Wallet?> AddCurrencyAsync(int userId, decimal amount, string currency)
    {
        var wallet = await _context.Wallets.FindAsync(userId);
        if (wallet == null || wallet.Currency != currency)
            return null;

        wallet.Balance += amount;
        await _context.SaveChangesAsync();
        return wallet;
    }

    public async Task<(bool success, Wallet? wallet)> PurchaseAsync(int userId, decimal price, string currency)
    {
        var wallet = await _context.Wallets.FindAsync(userId);
        if (wallet == null || wallet.Currency != currency || wallet.Balance < price)
            return (false, null);

        wallet.Balance -= price;
        PurchaseData purchaseData = new PurchaseData()
        {
            Amount = price,
            UserId = userId,
            WalletId = wallet.Id,
            Currency = currency
        };
        OnPurchased?.Invoke(purchaseData);
        
        _operationService.AddOperation(new OperationData(purchaseData));
        
        await _context.SaveChangesAsync();
        return (true, wallet);
    }

    public async Task<bool> CanPurchaseAsync(decimal price, string currency)
    {
        var wallet = GetCurrentWallet();
        return wallet.Balance >= price && wallet.Currency == currency;
    }
    
    public Wallet GetCurrentWallet()
    {
        int userIdClaim = int.Parse(_accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        User user = _userService.GetUserById(userIdClaim);
        return _context.Wallets.Find(user.WalletId);
    }
}