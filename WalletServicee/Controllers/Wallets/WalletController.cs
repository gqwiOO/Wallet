using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WalletServicee.Database;
using WalletServicee.Models.Users;

namespace WalletServicee.Controllers.Wallets;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WalletController: ControllerBase
{
    private readonly AppDbContext _context;
    
    public WalletController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public List<Wallet> GetAllWallets()
    {
        return _context.Wallets.ToList();
    }

    [HttpPost("addCurrency/{amount}/{currency}")]
    public async Task<IActionResult> AddCurrency([FromRoute]decimal amount, [FromRoute]string currency)
    {
        int id = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var wallet = await _context.Wallets.FindAsync(id);
    
        if (wallet == null)
            return NotFound();
        
        wallet.Balance += amount;
        await _context.SaveChangesAsync();
        
        return Ok(wallet);
    }


    [HttpPost("purchase/{price}/{currency}")]
    public async Task<IActionResult> Purchase( decimal price, string currency)
    {
        int id = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var wallet = await _context.Wallets.FindAsync(id);
        bool canPurchase = await CanPurchase(price, currency);
        if (!canPurchase)
        {
            return BadRequest("Not enough balance for this purchase"); 
        }
        
        wallet.Balance -= price;
        
        await _context.SaveChangesAsync();
        return Ok(wallet);
    }


    [HttpGet("canPurchase/{price}/{currency}")]
    public async Task<bool> CanPurchase(decimal price, string currency)
    {
        int id = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

        Wallet? wallet = await _context.Wallets.FindAsync(id);

        return wallet != null && wallet.Balance >= price && wallet.Currency == currency;
    }
}