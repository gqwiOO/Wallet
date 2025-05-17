using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WalletServicee.Database;
using WalletServicee.Models.Users;

namespace WalletServicee.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;
    
    public AccountController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("accounts")]
    public async Task<IActionResult> Get() => Ok(await _context.Users.ToListAsync());
    
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        _context.Users.Add(user);
        Wallet wallet = new Wallet()
        {
            Currency = "USDT",
            Balance = 0,
        };
        wallet.UserId = user.Id;
        wallet.User = user;
        _context.Wallets.Add(wallet);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpGet("{id}")]
    public User GetUserById([FromRoute] int id)
    {
        return _context.Users.FirstOrDefault(item => item.Id == id);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(item => item.Id == id);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return Ok(user);
    }
}