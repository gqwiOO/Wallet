using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletServicee.Models.Users;
using WalletServicee.Services;
using WalletServicee.Services.Wallets;

namespace WalletServicee.Controllers.Wallets;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;
    private IUserService _userService;

    public WalletController(IWalletService walletService, IUserService userService)
    {
        _userService = userService;
        _walletService = walletService;
    }

    [HttpGet]
    public async Task<List<Wallet>> GetAllWallets()
    {
        return await _walletService.GetAllWalletsAsync();
    }

    [HttpPost("addCurrency/{amount}/{currency}")]
    public async Task<IActionResult> AddCurrency(decimal amount, string currency)
    {
        int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var wallet = await _walletService.AddCurrencyAsync(id, amount, currency);
        if (wallet == null)
            return NotFound();
        return Ok(wallet);
    }

    [HttpPost("purchase/{price}/{currency}")]
    public async Task<IActionResult> Purchase(decimal price, string currency)
    {
        int id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var result = await _walletService.PurchaseAsync(id, price, currency);
        if (!result.success)
            return BadRequest("Not enough balance for this purchase");
        return Ok(result.wallet);
    }

    [HttpGet("canPurchase/{price}/{currency}")]
    public async Task<bool> CanPurchase(decimal price, string currency)
    {
        return await _walletService.CanPurchaseAsync(price, currency);
    }
}